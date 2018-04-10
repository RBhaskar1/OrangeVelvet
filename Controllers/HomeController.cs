using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCManukauTech.Models.DB;
using MVCManukauTech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace MVCManukauTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = "Server=citizen.manukautech.info,6307;Database=PR03_P01_Team;UID=PR03_P01;Password=fBit$40945;Encrypt=true;TrustServerCertificate=true";

        private readonly PR03_P01_TeamContext _context;

        public HomeController(PR03_P01_TeamContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";
            
            var aboutPage = await _context.PageData.SingleOrDefaultAsync(m => m.PageId == 1);
            return View(aboutPage);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // POST: Home/EditAbout/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(string id, [Bind("PageId,Pagename,Image,PageText,PageText2")] PageData pageData)
        {

            if (ModelState.IsValid)
            {
                if(pageData.PageText != null) { pageData.PageText = Regex.Replace(pageData.PageText, @"<[^>]*>", String.Empty); }
                if (pageData.Pagename != null) { pageData.Pagename = Regex.Replace(pageData.Pagename, @"<[^>]*>", String.Empty); }
                if (pageData.PageText2 != null) { pageData.PageText2 = Regex.Replace(pageData.PageText2, @"<[^>]*>", String.Empty); }
                _context.Update(pageData);
                await _context.SaveChangesAsync();
                return RedirectToAction("About");
            }

            return await About();
        }

        public IActionResult UserRoles()
        {
            string sql =
                @"SELECT u.Id AS UserId,u.Email,r.Name AS Role FROM AspNetUsers u 
                INNER JOIN AspNetUserRoles ur ON ur.UserId = u.Id
                INNER JOIN AspNetRoles r ON r.Id = ur.RoleId";
            List<UserRolesViewModel> report = _context.UserRolesViewModel.FromSql(sql).ToList();
            return View(report);
        }

        // GET: AspNetUserRoles/Edit/5
        public async Task<IActionResult> EditASPRoles(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles.SingleOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRoles.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        public async Task<IActionResult> EditRoles(string id, [Bind("UserId,RoleId,SilverMembership,GoldMembership,NoMembership,User,Role")] AspNetUserRoles role)
        {
            if (id != role.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateConcurrencyException dbx)
                {
                    Console.WriteLine(dbx.InnerException);
                }
            }
            return View(role);
        }
    }
}
