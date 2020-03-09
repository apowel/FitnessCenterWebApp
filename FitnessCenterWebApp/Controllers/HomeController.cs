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
            currentClub = null;
            return View();
        }
        public IActionResult MemberList()
        {
            var model = FitnessCenterWebApp.Models.MemberList.memberList;
            if (currentClub == null)
            {
                ViewBag.PageTitle = "Member List";
                return View(model);
            }
            else
            {
                List<Member> thisClub = FitnessCenterWebApp.Models.MemberList.GetMembersOf(currentClub.Membership);
                return View(thisClub);
            }
        }
        public IActionResult ClubList()
        {
            var model = FitnessCenterWebApp.Models.ClubList.clubList;
            ViewBag.PageTitle = "Club List";
            return View(model);
        }
        public ViewResult Club(Membership membership)
        {
            HomeController.currentClub = FitnessCenterWebApp.Models.ClubList.clubList.FirstOrDefault(e => e.Membership == membership);
            currentClub.members = FitnessCenterWebApp.Models.MemberList.GetMembersOf(membership);
            return View(HomeController.currentClub);
        }
        public ViewResult Details(int? id)
        {
            HomeController.currentMember = FitnessCenterWebApp.Models.MemberList.memberList.FirstOrDefault(e => e.Id == id);
            FitnessCenterWebApp.Models.MemberList.GetBalance();
            ViewBag.PageTitle = "Employee Details";
            return View(HomeController.currentMember);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
            Random id = new Random();
            member.Id = id.Next(1000, 9999);
            FitnessCenterWebApp.Models.MemberList.memberList.Add(member);
            return RedirectToAction("details", new { Id = member.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult CheckIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckIn(Member member)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("checkedin", 
                    FitnessCenterWebApp.Models.MemberList.memberList
                    .FirstOrDefault(e => e.Name == member.Name));
            }
            return View();
        }
        public ViewResult CheckedIn(int ? id)
        {
            currentMember = FitnessCenterWebApp.Models.MemberList.memberList.FirstOrDefault(e => e.Id == id);
            currentMember.CheckIn(currentClub);
            return View(currentMember);
        }
        public ActionResult Delete(int? id)
        {
            Member current = FitnessCenterWebApp.Models.MemberList.memberList.Where(e => e.Id == id).First();
            FitnessCenterWebApp.Models.MemberList.memberList.Remove(current);
            return RedirectToAction("memberlist");
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
        public ActionResult PayBill(int ? id)
        {
            Member current = FitnessCenterWebApp.Models.MemberList.memberList.Where(e => e.Id == id).First();
            current.Balance = 0;
            current.Begin = DateTime.Today;
            return RedirectToAction("details", new { Id = current.Id });
        }
        
    }
}
