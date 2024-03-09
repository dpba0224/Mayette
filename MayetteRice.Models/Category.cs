﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MayetteRice.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        
        [DisplayName("Display Order")]
        [Range(1, 30, ErrorMessage = "Display Order must be between 1-30.")]
        public int DisplayOrder { get; set; }
    }
}
