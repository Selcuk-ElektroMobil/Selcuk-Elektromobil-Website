﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.AuthFilter;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers
{
    public class YoneticiController : Controller
    {

        SaklambacDb<Contact> contactDb = new SaklambacDb<Contact>();
        SaklambacDb<User> userDb = new SaklambacDb<User>();
        SaklambacDb<Team> teamDb = new SaklambacDb<Team>();
        SaklambacDb<News> newsDb = new SaklambacDb<News>();
        SaklambacDb<Gallery> galleryDb = new SaklambacDb<Gallery>();

        /* ANASAYFA */
        [YoneticiFilter]
        public IActionResult Index()
        {
            CreateContactMessage();
            ViewBag.YoneticiMesajSayi = contactDb.GetAll().Count.ToString();
            ViewBag.YoneticiEkipSayi = teamDb.GetAll().Count.ToString();
            ViewBag.YoneticiHaberSayi = newsDb.GetAll().Count.ToString();
            ViewBag.YoneticiGaleriSayi = galleryDb.GetAll().Count.ToString();
            return View();
        }

        /* GIRIS YAP */
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Giris(User user)
        {
            var users = userDb.GetAll().Where(x => x.Username == user.Username && x.Password == user.Password);
            if (users.Count() > 0)
            {
                HttpContext.Session.SetString("SessionUserId", user.Username);
                return RedirectToAction("Index");   
            }
            return View();
        }

        /* BILGILER */
        [YoneticiFilter]
        public IActionResult Bilgiler()
        {
            return View();
        }

        /* MESAJLAR */
        [YoneticiFilter]
        public IActionResult Mesajlar()
        {
            return View(contactDb.GetAll());
        }

        /* MESAJ OKU */
        [YoneticiFilter]
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