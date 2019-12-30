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
    public class YoneticiEkipController : Controller
    {

        SaklambacDb<Team> teamDb = new SaklambacDb<Team>();

        /* TAKIM ISIMLERI */
        public IActionResult Index()
        {
            return View();
        }

        /* BELIRLI TAKIMA AIT TUM UYELER */
        public IActionResult TakimUyeleri(string team)
        {
            return View(teamDb.GetAll().Where(x => x.TeamName == team));
        }

        /* TEK UYE */
        public IActionResult Incele(string Id)
        {
            return View(teamDb.GetOneById(Id));
        }

        /* HABER EKLEME */
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(Team team, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                team.Photo = "defaultteam.png";
            }
            else
            {
                string newImage = Guid.NewGuid().ToString() + Image.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Ekip", newImage);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                team.Photo = newImage;
            }
            teamDb.Add(team);
            TempData["AddMessage"] = "Yeni Üye Başarıyla Eklendi.";
            return RedirectToAction("Index");
        }

        /* UYE GUNCELLEME */
        public IActionResult Guncelle(string Id)
        {
            return View(teamDb.GetOneById(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Guncelle(string Id, Team newTeam, IFormFile Image)
        {
            Team team = teamDb.GetOneById(Id);
            Team updateTeam = teamDb.GetOneById(Id);
            if (Image != null)
            {
                if (team.Photo != "defaultteam.png")
                {
                    if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\Ekip\\" + team.Photo))
                    {
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\Ekip\\" + team.Photo);
                    }
                }
                string newImage = Guid.NewGuid().ToString() + Image.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Ekip", newImage);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                team.Photo = newImage;
            }

            team.NameSurname = newTeam.NameSurname;
            team.Github = newTeam.Github;
            team.Twitter = newTeam.Twitter;
            team.Linkedin = newTeam.Linkedin;
            team.Instagram = newTeam.Instagram;
            team.TeamName = newTeam.TeamName;

            teamDb.Update(updateTeam, team);

            TempData["UpdateMessage"] = "Üye Başarıyla Güncellendi.";
            return RedirectToAction("Index");
        }

        /* UYE SIL */
        [HttpPost]
        public IActionResult Sil(string Id)
        {
            var team = teamDb.GetOneById(Id);
            if (team.Photo != "defaultteam.png")
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\Ekip\\" + team.Photo))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\wwwroot\\Ekip\\" + team.Photo);
                }
            }
            teamDb.Delete(team);
            TempData["DeleteMessage"] = "Üye Başarıyla Silindi.";
            return RedirectToAction("Index");
        }

    }
}