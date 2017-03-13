using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Privilegia.Models.Partner;

namespace Privilegia.Models
{
    public class Logo
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        public string IdPartner { get; set; }

    }
    public enum FileType
    {
        Logo = 1
    }
}