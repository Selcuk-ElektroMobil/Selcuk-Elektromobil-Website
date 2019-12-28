using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers
{
    public class YoneticiController : Controller
    {

        SaklambacDb<Contact> contactDb = new SaklambacDb<Contact>();

        /* ANASAYFA */
        public IActionResult Index()
        {
            CreateContactMessage();
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

        /* MESAJLAR */
        public IActionResult Mesajlar()
        {
            return View(contactDb.GetAll());
        }

        /* MESAJ OKU */
        public IActionResult MesajOku(string Id)
        {
            return View(contactDb.GetOneById(Id));
        }

        /* MESAJ SIL */
        [HttpPost]
        public IActionResult MesajSil(string Id)
        {
            contactDb.DeleteWithId(Id);
            TempData["DeleteMessage"] = "Mesaj Başarıyla Silindi.";
            return RedirectToAction("Mesajlar", "Yonetici");
        }

        void CreateContactMessage()
        {
            if (contactDb.GetAll().Count < 1)
            {
                Contact contact = new Contact();
                contact.Name = "Berke";
                contact.Surname = "Kurnaz";
                contact.Mail = "contact@berkekurnaz.com";
                contact.Title = "Mesaj Bölümüne Hoşgeldin.";
                contact.Message = "Merhaba Burası Mesaj Bölümü. Burada Mesajları Okuyabilir Ve Silebilirsin.";
                contact.Date = DateTime.Now.ToShortDateString();
                contactDb.Add(contact);
            }
        }

    }
}