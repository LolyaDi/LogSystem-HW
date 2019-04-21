using LogSystem.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace LogSystem.Models
{
    public class Visitor: Entity
    {
        [Required]
        [MaxLength(150)]
        public string Fullname { get; set; }
        [Required]
        public long CertificateNumber { get; set; }
        [Required]
        public DateTime EnterTime { get; set; }
        [Required]
        public DateTime QuitTime { get; set; }
        [Required]
        [MaxLength(100)]
        public string EnterPurpose { get; set; }
    }
}
