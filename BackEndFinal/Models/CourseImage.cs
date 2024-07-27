﻿using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class CourseImage:BaseEntity
    {
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int CourseId {  get; set; }
        public Course Course { get; set; }
    }
}
