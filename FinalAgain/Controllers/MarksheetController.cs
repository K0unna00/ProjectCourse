using FinalAgain.DAL;
using FinalAgain.Helpers;
using FinalAgain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalAgain.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MarksheetController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MarksheetController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var data = _context.Marksheets.Include(x => x.Admin).Include(x => x.Assignment).ToList();
            var check = _context.Assignments.ToList();
            if (check.Count > 0)
            {
                ViewBag.Check = true;
            }
            else
            {
                ViewBag.Check = false;
            }
            return View(data);
        }
        public IActionResult CreateMarksheet()
        {
            ViewBag.Assingments = _context.Assignments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateMarksheet(Marksheet mrk)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            mrk.CreatedAt = DateTime.UtcNow;
            mrk.Admin = user;
            mrk.AdminId = user.Id;
            _context.Marksheets.Add(mrk);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Info(int id)
        {
            var data = _context.Marksheets.Include(x => x.TeamMakrsheets).ThenInclude(x => x.Team)
                                        .Include(x => x.Admin)
                                            .Include(x => x.Assignment)
                                                .Include(x => x.TeamMakrsheets)
                                                    .FirstOrDefault(x => x.Id == id);

            var data2 = _context.TeamMakrsheets.Include(x => x.Marksheet).Include(x => x.Team).Where(x => x.MarksheetId == id).ToList();
            List<Team> teams = new List<Team>();
            foreach (var i in data2)
            {
                if (!teams.Exists(x => x.Id == i.Team.Id))
                {
                    teams.Add(i.Team);
                }
            }
            ViewBag.Teams = teams;
            return View(data);
        }
        public IActionResult Comments(int id)
        {
            var marksheet = _context.Marksheets.FirstOrDefault(x => x.Id == id);
            var questions=_context.Questions.Where(x => x.MarksheetId == id).ToList();
            List<Mark> marks= new List<Mark>();
            foreach(var i in questions)
            {
                var data=_context.Marks.Include(x=>x.Marker).Include(x=>x.Team).Where(x => x.QuestionId == i.Id).ToList();
                foreach(var j in data)
                {
                    marks.Add(j);
                }
            }
            return View(marks);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.Marksheets.FirstOrDefault(x => x.Id == id);
            var questions = _context.Questions.Where(x => x.MarksheetId == id).ToList();
            var TM = _context.TeamMakrsheets.Where(x => x.MarksheetId == id).ToList();
            var UM = _context.UserMarksheets.Where(x => x.MarksheetId == id).ToList();
            foreach(var i in TM)
            {
                _context.TeamMakrsheets.Remove(i);
            }
            foreach (var i in questions)
            {
                var marks = _context.Marks.Where(x => x.QuestionId == i.Id);
                foreach (var j in marks)
                {
                    _context.Marks.Remove(j);
                }
                _context.Questions.Remove(i);
            }
            foreach (var i in UM)
            {
                _context.UserMarksheets.Remove(i);
            }
            _context.Marksheets.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
