namespace LexiconLMS.App.Client.Models
{
    public interface IAlert
    {
        string message { get; set; }
        Alerts alert { get; set; }
    }
}