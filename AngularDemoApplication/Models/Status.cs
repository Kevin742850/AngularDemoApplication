﻿using System.ComponentModel.DataAnnotations;

namespace AngularDemoApplication.Models
{
    public class Status
    {
        public int Id { get; set; }

        [StringLength(50)]

        public string Option { get; set; } = string.Empty;
    }
}
