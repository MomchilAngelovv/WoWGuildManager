using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ApiModels.Logs
{
    public class ExceptionApiViewModel
    {
        public string Id { get; set; }

        public string ExceptionMessage { get; set; }

        public string Username { get; set; }

        public DateTime ExceptionTime { get; set; }
    }
}
