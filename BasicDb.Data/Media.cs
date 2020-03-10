﻿    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDb.Data
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string MediaType { get; set; }

        [Required]
        public string Description { get; set; }

        public string AddedBy { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
