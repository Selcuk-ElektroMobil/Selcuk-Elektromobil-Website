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
    public class YoneticiHaberController : Controller
    {

        SaklambacDb<News> newsDb = new SaklambacDb<News>();

        /* BUTUN HABERLER */
        public IActionResult Index()
        {
            return View(newsDb.GetAll());
        }

        /* TEK HABER */
        public IActionResult Incele(string Id)
        {
            return View(newsDb.GetOneById(Id));
        }

        /* HABER EKLEME */
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(News news, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                news.Photo = "defaultnews.png";
            }
            else
            {
                string newImage = Guid.NewGuid().ToString() + Image.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Haberler", newImage);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                news.Photo = newImage;
            }
            var cont = news.Content.Replace("\r", "");
            news.Content = cont.Replace("\n", " ");
            newsDb.Add(news);
            TempData["AddMessage"] = "Yeni Haber Başarıyla Eklendi.";
            return RedirectToAction("Index", "YoneticiHaber");
        }

        /* HABER GUNCELLEME */
        public IActionResult Guncelle(string Id)
        {
            return View(newsDb.GetOneById(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Guncelle(string Id, News newNews, IFormFile Image)
        {
            News news = newsDb.GetOneById(Id);
            News updateNews = newsDb.GetOneById(Id);
            if (Image != null)
            {
                if (news.Photo != "defaultnews.png")
                {
                    if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\Haberler\\" + news.Photo))
                    {
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\Haberler\\" + news.Photo);
                    }
                }
                string newImage = Guid.NewGuid().ToString() + Image.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Haberler", newImage);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                news.Photo = newImage;
            }

            news.Title = newNews.Title;
            news.Date = newNews.Date;
            news.Author = newNews.Author;
            news.Content = newNews.Content;


            var cont = newNews.Content.Replace("\r", "");
            news.Content = cont.Replace("\n", " ");
            newsDb.Update(updateNews, news);

            TempData["UpdateMessage"] = "Haber Başarıyla Güncellendi.";
            return RedirectToAction("Index", "YoneticiHaber");
        }

        /* HABER SIL */
        [HttpPost]
        public IActionResult Sil(string Id)
        {
            var news = newsDb.GetOneById(Id);
            if (news.Photo != "defaultnews.png")
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\Haberler\\" + news.Photo))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\Haberler\\" + news.Photo);
                }
            }
            newsDb.Delete(news);
            TempData["DeleteMessage"] = "Haber Başarıyla Silindi.";
            return RedirectToAction("Index", "YoneticiHaber");
        }

    }
}