using CIS.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Model.Models
{
    [Table("RequestCategories")]
    public class RequestCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        public virtual IEnumerable<Request> Requests { get; set; }
    }
}