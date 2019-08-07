namespace WowGuildManager.Models.ApiModels.Logs
{
    using System;

    public class ExceptionApiViewModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
