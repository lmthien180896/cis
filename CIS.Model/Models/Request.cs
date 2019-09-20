using CIS.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Model.Models
{
    [Table("Requests")]
    public class Request : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(256)]
        public string SenderName { get; set; }

        [Required]
        [DataType("varchar")]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(500)]
        public string Detail { get; set; }

        [Required]
        [MaxLength(256)]
        public string Place { get; set; }

        [MaxLength(256)]
        public string Files { get; set; }

        [DataType("varchar")]
        [MaxLength(6)]
        public string Code { get; set; }

        public DateTime? ClosedDate { get; set; }

        [ForeignKey("CategoryID")]
        public virtual RequestCategory RequestCategory { get; set; }

        public virtual IEnumerable<RequestReport> RequestReports { get; set; }
    }
}