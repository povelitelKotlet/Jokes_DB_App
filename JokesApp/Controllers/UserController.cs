using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using JokesApp.Data;
using JokesApp.Models;
using JokesApp.Controllers;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace JokesApp.Controllers;

public class User : Controller
{
    private readonly DatabaseContext _context = new DatabaseContext(); 
        
    // GET
    public IActionResult Register(int id)
    {
        return View();
    }

    public IActionResult Login(int id)
    {
        return View("Login", id);
    }

    public IActionResult LoginUser(String login, String password)
    {
        var user = _context.User.Where(j => j.Login.Equals(login)).ToList()[0];

        if (user.Password == password)
        {
            return View("~/Views/Home/Index.cshtml", user.UserId);
        }

        return View("Login");
    }
    
    
    public IActionResult RegisterNewUser(String login, String password, int id)
    {
        var user_id = _context.User.Count() + 1;
        if (!_context.User.Any(j => j.Login.Equals(login)))
        {
            _context.User.Add(new JokesApp.Models.User
            {
                UserId = user_id,
                Login = login,
                Password = password
            });
            _context.SaveChanges();
            return View("~/Views/Home/Index.cshtml", user_id);
        }
        return View("Register", id);
    }
    
    public IActionResult Exit(int id)
    {
        return View("~/Views/Home/Index.cshtml", 0);
    }
    
}