﻿using BackEndFinal.Models.Common;

namespace BackEndFinal.Models
{
    public class SliderContent:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsMain  { get; set; }
    }
}
