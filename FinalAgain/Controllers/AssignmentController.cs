using Microsoft.AspNetCore.Mvc;
using FinalAgain.DAL;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using FinalAgain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalAgain.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssignmentController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AssignmentController( AppDBContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var data = _context.Assignments.Include(x=>x.Admin).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Assignment assignment)
        {
            var Admin = _userManager.FindByNameAsync(User.Identity.Name).Result;
            assignment.Admin = Admin;
            assignment.AdminId = Admin.Id;
            _context.Assignments.Add(assignment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Info(int id)
        {
            var data = _context.Assignments.Include(x => x.Admin).FirstOrDefault(x => x.Id == id);
            ViewBag.Marksheets = _context.Marksheets.Where(x => x.AssignmentId == id).ToList();
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var assignment = _context.Assignments.FirstOrDefault(x => x.Id == id);
            var marksheets = _context.Marksheets.Where(x => x.AssignmentId == id).ToList();
            foreach(var mrk in marksheets)
            {
                var TM = _context.TeamMakrsheets.Where(x => x.MarksheetId == mrk.Id).ToList();
                foreach (var i in TM)
                {
                    _context.TeamMakrsheets.Remove(i);
                }
                var UM = _context.UserMarksheets.Where(x => x.MarksheetId == mrk.Id).ToList();
                foreach (var i in UM)
                {
                    _context.UserMarksheets.Remove(i);
                }
                var questions = _context.Questions.Where(x => x.MarksheetId == mrk.Id).ToList();
                foreach (var i in questions)
                {
                    var marks = _context.Marks.Where(x => x.QuestionId == i.Id);
                    foreach (var j in marks)
                    {
                        _context.Marks.Remove(j);
                    }
                    _context.Questions.Remove(i);
                }
                _context.Marksheets.Remove(mrk);
            }
            _context.Assignments.Remove(assignment);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}
