using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Model.Models
{
    [Table("Jobs")]
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]        
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }

        public virtual IEnumerable<Applicant> Applicants { set; get; }
    }
}
