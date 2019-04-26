using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MigrationWithMySql.DAL;
using MigrationWithMySql.Models;

namespace MigrationWithMySql.Controllers
{
    public class CompanyController : Controller
    {
        private MyDbContext _myDbContext;
        public CompanyController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public IActionResult Index()
        {
            return View(_myDbContext.Companies);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {


                _myDbContext.Companies.Add(company);
                _myDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}