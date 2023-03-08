namespace LexiconLMS.App.Client.DTOs
{
    public interface IEntityDto
    {
        int Id { get; set; }
        string Title { get; set; } 
        string Description { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }
}