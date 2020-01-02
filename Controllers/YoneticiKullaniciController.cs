using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.AuthFilter;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers
{
    [YoneticiFilter]
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
        [HttpPost]
        public IActionResult Ekle(User user)
        {
            userDb.Add(user);
            TempData["AddMessage"] = "Kullanıcı Başarıyla Eklendi.";
            return RedirectToAction("Index");
        }

        /* KULLANICI GUNCELLEME */
        public IActionResult Guncelle(string Id)
        {
            return View(userDb.GetOneById(Id));
        }
        [HttpPost]
        public IActionResult Guncelle(string Id, User user)
        {
            var userfind = userDb.GetOneById(Id);
            userDb.Update(userfind, user);
            TempData["UpdateMessage"] = "Kullanıcı Başarıyla Güncellendi.";
            return RedirectToAction("Index");
        }

        /* KULLANICI SILME */
        [HttpPost]
        public IActionResult Sil(string Id)
        {
            userDb.DeleteWithId(Id);
            TempData["DeleteMessage"] = "Kullanıcı Başarıyla Silindi.";
            return RedirectToAction("Index");
        }

    }
}