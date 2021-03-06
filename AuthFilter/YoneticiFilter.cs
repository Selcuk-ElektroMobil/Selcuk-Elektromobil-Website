﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelcukElektromobilWebsite.AuthFilter
{
    public class YoneticiFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("SessionUserId") == null)
            {
                context.Result = new RedirectResult("/Anasayfa/Index/");
            }
        }
    }
}
