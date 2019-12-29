using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers
{
    public class YoneticiKullaniciController : Controller
    {

        SaklambacDb<User> userDb = new SaklambacDb<User>();

        /* BUTUN KULLANICILAR */
        public IActionResult Index()
        {
            return View(userDb.GetAll());
        }

        /* TEK KULLANICI */
        public IActionResult Incele(string Id)
        {
            return View(userDb.GetOneById(Id));
        }

        /* KULLANICI EKLEME */
        public IActionResult Ekle()
        {
            return View();
        }

        /* KULLANICI SILME */
        public IActionResult Sil()
        {
            return View(userDb.GetAll());
        }

    }
}