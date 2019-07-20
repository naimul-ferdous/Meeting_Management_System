using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Controllers
{
    public class AddressController : Controller
    {
        AddressRepository repository = new AddressRepository();
        public ActionResult Address()
        {
            return View();
        }

        public ActionResult Create(Address address)
        {
            ActionResult rtn = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                {
                    int rowAff = repository.Insert(address);

                    return Json(rowAff, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                rtn = Json(-100, JsonRequestBehavior.DenyGet);
            }

            return rtn;
        }

        public ActionResult Edit(Address address)
        {
            int rowAff = repository.Edit(address);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int addressId)
        {
            ActionResult rtn = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                {
                    int row = repository.Delete(addressId);
                    return Json(row, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                rtn = Json(0, JsonRequestBehavior.DenyGet);
            }
            return rtn;


        }

        public ActionResult Index()
        {
            var list = repository.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult CountryCity()
        //{
        //    CountryRepository countryRepository = new CountryRepository();
        //    return Json(countryRepository.GetAll(), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult Country()
        //{

        //    repository.country();
        //    return View();
        //}
    }
}