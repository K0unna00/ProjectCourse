using FinalAgain.DAL;
using FinalAgain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace FinalAgain.Services
{
    public class LayoutService
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LayoutService(AppDBContext context, UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public List<AppUser> GetUsers()
        {
            return _context.Users.ToList();
        }
        public string GetRole(AppUser user)
        {
            return (_userManager.GetRolesAsync(user).Result)[0];
        }
    }
}