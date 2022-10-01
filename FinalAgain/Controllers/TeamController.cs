using FinalAgain.DAL;
using FinalAgain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalAgain.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TeamController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            ViewBag.MarksheetId = id;
            var data = _context.TeamMakrsheets.Include(x=>x.Marksheet).Include(x=>x.Team).Where(x => x.MarksheetId == id).ToList();
            List<Team> teams = new List<Team>();
            foreach(var i in data)
            {
                if (!teams.Exists(x => x.Id == i.Team.Id)){
                    teams.Add(i.Team);
                }
            }
            return View(teams);
        }
        public IActionResult Teams()
        {
            var data = _context.Teams.ToList();
            return View(data);
        }
        public IActionResult CreateTeam()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges(); 
            return RedirectToAction("Teams");
        }
        public IActionResult AddTeam(int id)
        {
            ViewBag.MarksheetId = id;
            var data = _context.Teams.ToList();
            return View(data);
        }
        public IActionResult AddTeamOnMarksheet(int id , int mrkId)
        {
            TeamMakrsheet TM = new TeamMakrsheet()
            {
                MarksheetId = mrkId,
                TeamId = id
            };
            _context.TeamMakrsheets.Add(TM);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public IActionResult TeamInfo(int id)
        {
            var data=_context.Teams.FirstOrDefault(x=>x.Id==id);
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var data=_context.Teams.FirstOrDefault(x => x.Id == id);
            var marks = _context.Marks.Where(x => x.TeamId == id);
            var teamMarksheet = _context.TeamMakrsheets.Where(x => x.TeamId == id);
            foreach(var i in marks)
            {
                _context.Marks.Remove(i);
            }
            foreach(var i in teamMarksheet)
            {
                _context.TeamMakrsheets.Remove(i);
            }
            _context.Teams.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
