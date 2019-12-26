using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SelcukElektromobilWebsite.Controllers
{
    public class AnasayfaController : Controller
    {

        /* ANASAYFA */
        public IActionResult Index()
        {
            return View();
        }



        /* HAKKIMIZDA SAYFALARI */
        public IActionResult BizKimiz()
        {
            return View();
        }

        public IActionResult Misyonumuz()
        {
            return View();
        }

        public IActionResult Vizyonumuz()
        {
            return View();
        }

        public IActionResult SSS()
        {
            return View();
        }



        /* EKİBİMİZ SAYFALARI */
        public IActionResult Akademisyenler()
        {
            return View();
        }

        public IActionResult ElektronikTakimi()
        {
            return View();
        }

        public IActionResult MekanikTakimi()
        {
            return View();
        }

        public IActionResult YazilimTakimi()
        {
            return View();
        }

    }
}
