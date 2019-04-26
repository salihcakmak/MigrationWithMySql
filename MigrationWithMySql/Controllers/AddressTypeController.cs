using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MigrationWithMySql.DAL;
using MigrationWithMySql.Models;

namespace MigrationWithMySql.Controllers
{
    public class AddressTypeController : Controller
    {
        private MyDbContext _myDbContext;
        public AddressTypeController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public IActionResult Index()
        {
            return View(_myDbContext.AddressTypes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                _myDbContext.AddressTypes.Add(addressType);
                _myDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        public IActionResult Delete(long id)
        {
            var addressType = _myDbContext.AddressTypes.FirstOrDefault(m=>m.Id==id);
            return View(addressType);
            
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var addressType = _myDbContext.AddressTypes.Find(id);
            _myDbContext.AddressTypes.Remove(addressType);
            _myDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(long id)
        {
            AddressType addressType = _myDbContext.AddressTypes.Find(id);
            return View(addressType);
        }
        [HttpPost]
        public IActionResult Edit(long id ,AddressType addressType)
        {
            _myDbContext.Update(addressType);
            _myDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}