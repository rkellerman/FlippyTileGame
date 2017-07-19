using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductKeyServer.Models
{
    public class Key
    {
        [Key]
        public string ProductKey { get; set; }
        public string HardwareId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime LastChecked { get; set; }
        public bool IsDisabled { get; set; }
    }
}