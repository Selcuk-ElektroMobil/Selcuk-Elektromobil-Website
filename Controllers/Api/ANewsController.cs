using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saklambac.NetCore.Database;
using SelcukElektromobilWebsite.Models;

namespace SelcukElektromobilWebsite.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ANews")]
    public class ANewsController : Controller
    {

        SaklambacDb<News> newsDb = new SaklambacDb<News>();

        public async Task<ActionResult> GetAll()
        {
            return Ok(newsDb.GetAll());
        }

        [HttpGet("{newsId}")]
        public async Task<ActionResult> GetByNewsId(string newsId)
        {
            return Ok(newsDb.GetOneById(newsId));
        }

    }
}