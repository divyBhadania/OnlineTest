﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Model
{
    public class Technology
    {
        public int Id { get; set; }
        public string TechName { get; set; }
        [ForeignKey("UserCreatedBy")]
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("UserModifiedBy")]
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public User UserCreatedBy { get; set; }
        public User UserModifiedBy { get; set; }
    }
}