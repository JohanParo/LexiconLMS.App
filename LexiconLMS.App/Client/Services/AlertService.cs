namespace LexiconLMS.App.Client.Services
{
    using LexiconLMS.App.Client.Models;
    using System;
    using System.Collections.Generic;


    //https://dev.to/supachris28/building-an-alert-service-in-blazor-4poo
    public class AlertService
    {
        public List<IAlert> messages { get; private set; }
        public event Action RefreshRequested;

        public AlertService()
        {
            this.messages = new List<IAlert>();
        }

        public void addMessage(Alert alert)
        {
            this.messages.Add(alert);
            System.Console.WriteLine("Message count: {0}", this.messages.Count);
            RefreshRequested?.Invoke();

            // pop message off after a delay
            new System.Threading.Timer((_) => {
                this.messages.RemoveAt(0);
                RefreshRequested?.Invoke();
            }, null, 8000, System.Threading.Timeout.Infinite);
        }
    }
}
