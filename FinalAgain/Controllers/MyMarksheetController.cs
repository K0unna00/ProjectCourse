using FinalAgain.DAL;
using FinalAgain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalAgain.Controllers
{
    [Authorize(Roles = "Marker")]

    public class MyMarksheetController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        public MyMarksheetController(AppDBContext context
                             , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(string userId)
        {

            var data = _context.UserMarksheets.Include(x=>x.Marksheet).Include(x=>x.Marksheet.Admin).Where(x => x.MarkerId == userId).ToList();
            List<Marksheet> MarksheetList = new List<Marksheet>();
            foreach(var i in data)
            {
                MarksheetList.Add(i.Marksheet);
            }
            ViewBag.TeamMarksheets = _context.TeamMakrsheets.Include(x=>x.Marksheet).Include(x => x.Team).ToList();
            return View(MarksheetList);
        }
    }
}
