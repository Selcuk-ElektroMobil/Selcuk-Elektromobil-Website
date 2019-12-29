using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers
{
    public class YoneticiGaleriController : Controller
    {

        SaklambacDb<Gallery> galleryDb = new SaklambacDb<Gallery>();
        
        /* BUTUN FOTOGRAFLAR */
        public IActionResult Index()
        {
            return View(galleryDb.GetAll());
        }

        /* TEK FOTOGRAF */
        public IActionResult Incele(string Id)
        {
            return View(galleryDb.GetOneById(Id));
        }

        /* FOTOGRAF EKLEME */
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(Gallery gallery, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                gallery.Photo = "defaultgallery.png";
            }
            else
            {
                string newImage = Guid.NewGuid().ToString() + Image.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Galeri", newImage);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                gallery.Photo = newImage;
            }

            galleryDb.Add(gallery);
            TempData["AddMessage"] = "Yeni Fotoğraf Başarıyla Eklendi.";
            return RedirectToAction("Index");
        }

        /* FOTOGRAF GUNCELLEME */
        public IActionResult Guncelle(string Id)
        {
            return View(galleryDb.GetOneById(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Guncelle(string Id, Gallery newGallery, IFormFile Image)
        {
            Gallery gallery = galleryDb.GetOneById(Id);
            Gallery updateGallery = galleryDb.GetOneById(Id);
            if (Image != null)
            {
                if (gallery.Photo != "defaultgallery.png")
                {
                    if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\Galeri\\" + gallery.Photo))
                    {
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\Galeri\\" + gallery.Photo);
                    }
                }
                string newImage = Guid.NewGuid().ToString() + Image.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Galeri", newImage);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                gallery.Photo = newImage;
            }
            galleryDb.Update(updateGallery, gallery);
            TempData["UpdateMessage"] = "Fotoğraf Başarıyla Güncellendi.";
            return RedirectToAction("Index");
        }

        /* MESAJ SIL */
        [HttpPost]
        public IActionResult Sil(string Id)
        {
            var gallery = galleryDb.GetOneById(Id);
            if (gallery.Photo != "defaultgallery.png")
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\Galeri\\" + gallery.Photo))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\Galeri\\" + gallery.Photo);
                }
            }
            galleryDb.Delete(gallery);
            TempData["DeleteMessage"] = "Fotoğraf Başarıyla Silindi.";
            return RedirectToAction("Index", "YoneticiGaleri");
        }

    }
}