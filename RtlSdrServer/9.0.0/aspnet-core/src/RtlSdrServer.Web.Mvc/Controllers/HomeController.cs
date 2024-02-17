﻿using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using RtlSdrServer.Controllers;

namespace RtlSdrServer.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : RtlSdrServerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}