using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Model.Models
{
    [Table("Applicants")]
    public class Applicant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Fullname { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(256)]
        public string Resume { get; set; }

        public int JobID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [ForeignKey("JobID")]
        public virtual Job Job { set; get; }
    }
}
