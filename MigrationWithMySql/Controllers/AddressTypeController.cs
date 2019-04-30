using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MigrationWithMySql.DAL;
using MigrationWithMySql.DAL.UnitOfWork;
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
            List<AddressType> list = new List<AddressType>();
            using (UnitOfWork uow = new UnitOfWork ())
            {
               list = uow.GetRepository<AddressType>().GetAll().ToList();
            }
            return View(list);
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
                using (UnitOfWork uow =new UnitOfWork ())
                {
                    uow.GetRepository<AddressType>().Add(addressType);
                    uow.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        public IActionResult Delete(long id)
        {
            using (UnitOfWork uow =new UnitOfWork())
            {
                var addressType = uow.GetRepository<AddressType>().Get(x => x.Id == id);
                return View(addressType);
            }            
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            using (UnitOfWork uow = new UnitOfWork ())
            {
                AddressType address = uow.GetRepository<AddressType>().Get(x => x.Id == id);
                uow.GetRepository<AddressType>().Delete(address);
                uow.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(long id)
        {
            using (UnitOfWork uow = new UnitOfWork ())
            {
                AddressType address = uow.GetRepository<AddressType>().Get(x => x.Id == id);
                return View(address);
            }
        }
        [HttpPost]
        public IActionResult Edit(long id ,AddressType addressType)
        {
            using (UnitOfWork uow =new UnitOfWork ())
            {
                uow.GetRepository<AddressType>().Update(addressType);
                uow.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}