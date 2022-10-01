using FinalAgain.DAL;
using FinalAgain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FinalAgain.Controllers
{
    public class QuestionController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public QuestionController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int teamId, int id)
        {
            ViewBag.MarksheetId = id;
            ViewBag.TeamId = teamId;
            var data = _context.Questions.Include(x => x.Admin).Include(x => x.Marksheet).Where(x => x.MarksheetId == id).ToList();
            return View(data);
        }
        public IActionResult CreateQuestion(int id)
        {
            ViewBag.MarksheetId = id;
            return View();
        }
        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            AppUser Admin = _userManager.FindByNameAsync(User.Identity.Name).Result;
            question.Id = 0; //The Get "CreateQuestion" method have "int Id" on its constractor and its making some problems .Thats why i added this line 
            question.Admin = Admin;
            question.AdminId = Admin.Id;
            question.Marksheet = _context.Marksheets.FirstOrDefault(x => x.Id == question.MarksheetId);
            _context.Questions.Add(question);
            _context.SaveChanges();
            return RedirectToAction("Index", "Marksheet");
        }
        public IActionResult AddMark(int teamId, int id)
        {
            ViewBag.Question = _context.Questions.FirstOrDefault(x => x.Id == id);
            ViewBag.TeamId = teamId;
            return View();
        }
        [HttpPost]
        public IActionResult AddMark(int qId, int tId, Mark mark)
        {
            mark.Id = 0;//The Get "AddMark" method have "int Id" on its constractor and its making some problems .Thats why i added this line 
            mark.MarkerId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            mark.CreatedAt = DateTime.UtcNow;
            var question = _context.Questions.FirstOrDefault(x => x.Id == mark.QuestionId);
            var maksheet = _context.Marksheets.FirstOrDefault(x => x.Id == question.MarksheetId);
            if (mark.Grade > question.GradeLimit)
            {
                ModelState.AddModelError("TeamId", "Your grade must be lower than limit!");
                ViewBag.Question = _context.Questions.FirstOrDefault(x => x.Id == qId);
                ViewBag.TeamId = tId;
                return View();
            }
            var Team = _context.Teams.FirstOrDefault(x => x.Id == tId);
            Team.TotalGrade += mark.Grade;
            _context.Marks.Add(mark);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Delete(int id)
        {
            var data = _context.Questions.FirstOrDefault(x => x.Id == id);
            var marks = _context.Marks.Where(x => x.QuestionId == id);
            foreach (var i in marks)
            {
                _context.Marks.Remove(i);
            }
            _context.Questions.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}
