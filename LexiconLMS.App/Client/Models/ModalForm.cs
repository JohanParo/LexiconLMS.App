namespace LexiconLMS.App.Client.Models
{
    public class ModalForm
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Update this one...";
        public ModalType Type { get; set; }
    }

    public enum ModalType
    {
        None,
        Course,
        Module,
        Activity
    }

    public enum CastType
    {
        None,
        CourseDto,
        ModuleDto,
        ActivityDto
    }


}
