﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelcukElektromobilWebsite.Models
{
    public class News
    {
        public string Id { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }

        public News()
        {

        }

    }
}
