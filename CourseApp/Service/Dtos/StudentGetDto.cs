﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class StudentGetDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image {  get; set; }
    }
}
