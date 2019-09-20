using CIS.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Model.Models
{
    [Table("Credentials")]
    public class Credential : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(Order = 1)]
        public int UserGroupID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(Order = 2)]
        public string RoleID { get; set; }

        [ForeignKey("UserGroupID")]
        public virtual UserGroup UserGroup { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}