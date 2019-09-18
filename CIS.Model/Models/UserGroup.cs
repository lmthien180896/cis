using CIS.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Model.Models
{
    [Table("UserGroups")]
    public class UserGroup : Auditable
    {
        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        public virtual IEnumerable<Credential> Credentials { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}