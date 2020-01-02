using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers
{
    public class AnasayfaController : Controller
    {

        SaklambacDb<Contact> contactDb = new SaklambacDb<Contact>();
        SaklambacDb<Team> teamDb = new SaklambacDb<Team>();
        SaklambacDb<Gallery> galleryDb = new SaklambacDb<Gallery>();
        SaklambacDb<News> newsDb = new SaklambacDb<News>();

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
            return View(teamDb.GetAll().Where(x => x.TeamName == "Akademisyen"));
        }

        public IActionResult ElektronikTakimi()
        {
            return View(teamDb.GetAll().Where(x => x.TeamName == "Elektronik"));
        }

        public IActionResult MekanikTakimi()
        {
            return View(teamDb.GetAll().Where(x => x.TeamName == "Mekanik"));
        }

        public IActionResult YazilimTakimi()
        {
            return View(teamDb.GetAll().Where(x => x.TeamName == "Yazılım"));
        }



        /* GALERI */
        public IActionResult Galeri()
        {
            return View(galleryDb.GetAll());
        }



        /* SPONSORLAR */
        public IActionResult Sponsorlar()
        {
            return View();
        }



        /* HABERLER */
        public IActionResult Haberler()
        {
            return View(newsDb.GetAll());
        }

        public IActionResult Haber(string Id)
        {
            var item = newsDb.GetOneById(Id);
            if (item == null)
            {
                return RedirectToAction("Haberler");
            }
            return View(item);
        }


        /* ILETISIM */
        public IActionResult Iletisim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Iletisim(Contact contact)
        {
            if (contact.Title.Length > 1 && contact.Message.Length > 1)
            {
                contact.Date = DateTime.Now.ToShortDateString();
                contactDb.Add(contact);
                TempData["SendMessageSuccess"] = "Mesaj Başarıyla Gönderildi.";
                return RedirectToAction("Iletisim");
            }
            return View();
        }

    }
}
