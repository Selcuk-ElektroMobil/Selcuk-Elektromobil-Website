﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SelcukElektromobilWebsite.Controllers
{
    public class YoneticiController : Controller
    {
        
        /* ANASAYFA */
        public IActionResult Index()
        {
            return View();
        }

        /* GIRIS YAP */
        public IActionResult Giris()
        {
            return View();
        }

        /* BILGILER */
        public IActionResult Bilgiler()
        {
            return View();
        }



    }
}