using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitnessCenterWepApp.Models;
using FitnessCenterWebApp.Models;

namespace FitnessCenterWepApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static Club currentClub;
        public static Member currentMember;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = MemberList.memberList;
            ViewBag.PageTitle = "Member List";
            return View(model);
        }
        public ViewResult Details(int? id)
        {
            HomeController.currentMember = MemberList.memberList.FirstOrDefault(e => e.Id == id);
            ViewBag.PageTitle = "Employee Details";
            return View(HomeController.currentMember);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(Member member)
        {
            Random id = new Random();
            member.Id = id.Next(1000, 9999);
            MemberList.memberList.Add(member);
            return RedirectToAction("details", new { Id = member.Id });
        }
        public ActionResult Delete(int? id)
        {
            Member current = MemberList.memberList.Where(e => e.Id == id).First();
            MemberList.memberList.Remove(current);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
