namespace LexiconLMS.App.Client.Old
{
    public class CourseDTO2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsPublished { get; set; }


        //public List<Module> Modules { get; set; } = new List<Module>();
        //public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();


    }
}
