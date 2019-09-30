﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(250)]
        public string Phone { set; get; }

        [StringLength(500)]
        public string Message { set; get; }

        [Required]        
        public string Unit { get; set; }

        public DateTime CreatedDate { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}