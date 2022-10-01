using FinalAgain.DAL;
using FinalAgain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalAgain.Controllers
{
    public class MarkerController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MarkerController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            var data = _context.UserMarksheets.Include(x=>x.Marker).Where(x => x.MarksheetId == id).ToList();
            ViewBag.MarksheetId = id;
            return View(data);
        }
        public IActionResult AddMarker(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == "Marker");
            var data = _context.UserRoles.Where(x => x.RoleId == role.Id).ToList();
            var allocatedMarkers= _context.UserMarksheets.Where(x=>x.MarksheetId==id).ToList();
            List<AppUser> UserList = new List<AppUser>();
            foreach(var i in data)
            {
                var check = true;
                foreach(var j in allocatedMarkers)
                {
                    if (j.MarkerId == i.UserId)
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    var user = _userManager.FindByIdAsync(i.UserId).Result;
                    UserList.Add(user);
                }
            }
            ViewBag.MarksheetId = id;
            return View(UserList);
        }
        public IActionResult AddMarkerOnMarksheet(string id, int marksheetId)
        {
            UserMarksheet UM = new UserMarksheet()
            {
                MarkerId = id,
                MarksheetId = marksheetId
            };
            _context.UserMarksheets.Add(UM);
            _context.SaveChanges();
            return RedirectToAction("Index","Marksheet");
        }
        public IActionResult RemoveFromMarksheet(int id , string userId)
        {
            var data = _context.UserMarksheets.FirstOrDefault(x => (x.MarksheetId == id)&&(x.MarkerId==userId));
            _context.UserMarksheets.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index","Marksheet");
        }
    }
}
