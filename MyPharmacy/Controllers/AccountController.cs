using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyPharmacy.Data;
using MyPharmacy.Data.Static;
using MyPharmacy.Data.ViewModels;
using MyPharmacy.Models;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace MyPharmacy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "Name"); 
            return View(users);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FullName,SelectedRole,Email,FirstPassword,ConfirmPassword")] ApplicationUser appUser)
        {
            appUser.PasswordHash = HttpContext.Request.Form["FirstPassword"];
            if (id != HttpContext.Request.Form["Id"])
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ApplicationUser user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Users));
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "Name"); 
            return View(appUser);
        }

        public IActionResult Password() => View(new ResetPasswordVM());

        [HttpPost]
        public async Task<IActionResult> Password([Bind("CurrentPassword,FirstPassword, ConfirmPassword")] ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid) return View(resetPasswordVM);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, resetPasswordVM.CurrentPassword);
                if (passwordCheck)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPassResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.FirstPassword);
                    if (!resetPassResult.Succeeded)
                    {
                        TempData["error"] = "Something went wrong!";

                        foreach (var error in resetPassResult.Errors)
                        {
                            ModelState.TryAddModelError(error.Code, error.Description);
                        }
                        return View(resetPasswordVM);
                    }

                    TempData["success"] = "Your password has been successfully changed!";
                    return RedirectToAction("Profile", "Account");
                    
                }
                ModelState.AddModelError("CurrentPassword", "Your password is wrong!");
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(resetPasswordVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(resetPasswordVM);

        }


        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

            return View(user); 
        }

        public IActionResult UpdateProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

            //var user = _userManager.FindByIdAsync(userId);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string id, [Bind("Id,FullName,SelectedRole,FirstPassword,ConfirmPassword,UserName,Email")] ApplicationUser applicationUser)
        {
            if (id != HttpContext.Request.Form["Id"])
            {
                return NotFound();
            }

            // Get the existing user from the db
            //var user = await _userManager.FindByIdAsync(id);
            ApplicationUser user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            bool IsEmailExist = _context.Users.Any
                (x => x.Email == applicationUser.Email && x.Id != applicationUser.Id);
            if (IsEmailExist == true)
            {
                ModelState.AddModelError("Email", "This email already exists in our system");
            }

            if (ModelState.IsValid)
            {
                // Update it with the values from the view model
                user.FullName = applicationUser.FullName;
                user.UserName = applicationUser.UserName;
                user.Email = applicationUser.Email;
                
                // Apply the changes if any to the db
                
                try
                {
                    //_userManager.UpdateAsync(user);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["success"] = "Your profile data has been successfully updated!";
                return RedirectToAction("Profile", "Account");
            }

            return View(applicationUser);
        }

        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,SelectedRole,Email,FirstPassword,ConfirmPassword")] ApplicationUser appUser)
        {
            if (ModelState.IsValid)
            {
                appUser.PasswordHash = HttpContext.Request.Form["FirstPassword"];

                var appUser1 = await _userManager.FindByEmailAsync(appUser.Email);
                if (appUser1 == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = appUser.FullName,
                        UserName = appUser.Email,
                        Email = appUser.Email,
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(newAppUser, appUser.PasswordHash);
                    await _userManager.AddToRoleAsync(newAppUser, appUser.SelectedRole);

                    return RedirectToAction(nameof(Users));
                } else
                {
                    TempData["Error"] = "This email is already exists in our system!";
                    ModelState.AddModelError("Email", "This email is already exists in our system!");
                }

            }
            ViewData["Roles"] = new SelectList(_context.Roles, "Name");
            return View(appUser);
        }

        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.PasswordHash);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.PasswordHash, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM); 
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "User has been successfully deleted!";

            return RedirectToAction(nameof(Users));
        }

    }
}
