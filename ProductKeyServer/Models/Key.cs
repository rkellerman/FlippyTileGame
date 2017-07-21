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
        [Required(ErrorMessage = "Product Key must be filled in.")]
        [Display(Description = "Product Key")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Product Key must be exactly 10 characters long")]
        public string ProductKey { get; set; }

        public string HardwareId { get; set; }

        [Required(ErrorMessage = "Expiration Date must be filled in.")]
        [Range(typeof(DateTime), "1/1/2000", "12/31/2020", ErrorMessage = "Expiration date must be set properly")]
        public DateTime ExpirationDate { get; set; }

        public DateTime LastChecked { get; set; }

        public bool IsDisabled { get; set; }

    }
}