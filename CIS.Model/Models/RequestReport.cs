using CIS.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Model.Models
{
    [Table("RequestReports")]
    public class RequestReport : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int RequestID { get; set; }

        public int SupporterID { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        [ForeignKey("RequestID")]
        public virtual Request Request { get; set; }
    }
}