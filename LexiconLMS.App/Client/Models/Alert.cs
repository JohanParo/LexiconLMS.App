namespace LexiconLMS.App.Client.Models
{
    public class Alert : IAlert
    {
        public Alert() { }
        public Alert(string message, Alerts alert)
        {
            this.message = message;
            this.alert = alert;
        }

        public string message { get; set; }
        public Alerts alert { get; set; }
    }
}
