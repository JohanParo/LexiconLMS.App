namespace LexiconLMS.App.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using LexiconLMS.App.Server.Data;
    using LexiconLMS.App.Shared;
    using System.Reflection.Metadata;

    [ApiController]
    [Route("api/Filesave")]
    public class FilesaveController : ControllerBase
    {
        private string[] allowedExtensions = { ".doc", ".docx", ".jpg", ".gif", ".png", ".bmp", ".txt", ".pdf" };

        private readonly IWebHostEnvironment env;
        private readonly ILogger<FilesaveController> logger;
        private readonly ApplicationDbContext dbContext;

        public FilesaveController(IWebHostEnvironment env,
        ILogger<FilesaveController> logger,
        ApplicationDbContext dbContext)
        {
            this.env = env;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<IList<UploadResult>>> PostFile(
            [FromForm] IEnumerable<IFormFile> files)
        {
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<UploadResult> uploadResults = new();

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);

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
                                "Documents", "Modules",
                                trustedFileNameForFileStorage);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            logger.LogInformation("{FileName} saved at {Path}",
                                trustedFileNameForDisplay, path);
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                            uploadResult.FileType = file.ContentType;
                            dbContext.UploadResults.Add(uploadResult);
                            await dbContext.SaveChangesAsync();
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
}
