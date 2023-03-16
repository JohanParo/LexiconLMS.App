using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LexiconLMS.App.Server.Data;
using LexiconLMS.App.Server.Models;
using LexiconLMS.App.Client.DTOs;
using System.Net;
using System.Reflection.Metadata;
using LexiconLMS.App.Client;

namespace LexiconLMS.App.Server.Controllers
{
    [ApiController]
    [Route("api/Documents")]
    public class DocumentsController : ControllerBase
    {
        private String[] allowedExtensions = { ".doc", ".docx", ".jpg", ".gif", ".png", ".bmp", ".txt", ".pdf" };

        private readonly IWebHostEnvironment env;
        private readonly ILogger<DocumentsController> logger;
        private readonly ApplicationDbContext _context;

        public DocumentsController(IWebHostEnvironment env,
        ILogger<DocumentsController> logger, ApplicationDbContext context)
        {
            this.env = env;
            this.logger = logger;
            this._context = context;
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<Document>> PostDocument(Document document)
        public async Task<ActionResult<IList<DocumentDto>>> PostDocument(
           [FromBody] IEnumerable<IFormFile> files)
        {
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<DocumentDto> uploadResults = new();

            foreach (var file in files)
            {
                var uploadResult = new DocumentDto();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);
                var dbDocument = new Models.Document();

                if (filesProcessed < SharedVariable.maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        logger.LogInformation("{FileName} length is 0 (Err: 1)",
                            trustedFileNameForDisplay);
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > SharedVariable.maxFileSize)
                    {
                        logger.LogInformation("{FileName} of {Length} bytes is " +
                            "larger than the limit of {Limit} bytes (Err: 2)",
                            trustedFileNameForDisplay, file.Length, SharedVariable.maxFileSize);
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                            if (!allowedExtensions.Contains(extension))
                            {
                                logger.LogInformation("{FileName} has an invalid extension (Err: 5)",
                                    trustedFileNameForDisplay);
                                uploadResult.ErrorCode = 5;
                            }
                            trustedFileNameForFileStorage = Path.GetRandomFileName();
                            var path = Path.Combine(env.ContentRootPath,
                                env.EnvironmentName, "ModulesDoc",
                                trustedFileNameForFileStorage);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            logger.LogInformation("{FileName} saved at {Path}",
                                trustedFileNameForDisplay, path);
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                            uploadResult.FileType = file.ContentType;

                            dbDocument.StoredFileName = uploadResult.StoredFileName;
                            dbDocument.FileName = uploadResult.FileName;
                            dbDocument.Uploaded = uploadResult.Uploaded;
                            dbDocument.ErrorCode = uploadResult.ErrorCode;
                            dbDocument.FileType = uploadResult.FileType;
                            dbDocument.IsHidden = uploadResult.IsHidden;
                            dbDocument.ModuleId = uploadResult.ModuleId;
                            dbDocument.CourseId = 0;
                            dbDocument.ModuleId = 0;
                            dbDocument.ActivityId = 0;

                            _context.Document.Add(dbDocument);
                            await _context.SaveChangesAsync();
                        }
                        catch (IOException ex)
                        {
                            logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                                trustedFileNameForDisplay, ex.Message);
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    logger.LogInformation("{FileName} not uploaded because the " +
                        "request exceeded the allowed {Count} of files (Err: 4)",
                        trustedFileNameForDisplay, SharedVariable.maxAllowedFiles);
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }

            return new CreatedResult(resourcePath, uploadResults);
        }
    }

        //// GET: api/Documents
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Document>>> GetDocument()
        //{
        //  if (_context.Document == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Document.ToListAsync();
        //}

    //    // GET: api/Documents/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Document>> GetDocument(int id)
    //    {
    //      if (_context.Document == null)
    //      {
    //          return NotFound();
    //      }
    //        var document = await _context.Document.FindAsync(id);

    //        if (document == null)
    //        {
    //            return NotFound();
    //        }

    //        return document;
    //    }

    //    // PUT: api/Documents/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutDocument(int id, Document document)
    //    {
    //        if (id != document.Id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(document).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!DocumentExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

        

    //    // DELETE: api/Documents/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteDocument(int id)
    //    {
    //        if (_context.Document == null)
    //        {
    //            return NotFound();
    //        }
    //        var document = await _context.Document.FindAsync(id);
    //        if (document == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Document.Remove(document);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool DocumentExists(int id)
    //    {
    //        return (_context.Document?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    //}
}
