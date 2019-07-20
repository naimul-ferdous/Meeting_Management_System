using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class OfficeController : Controller
    {
        OfficeRepository officeRepository = new OfficeRepository();
        Address address = new Address();
        AddressRepository addressRepository = new AddressRepository();
        


        // GET: Office

        public ActionResult Office()
        {
            return View();
        }

        public ActionResult Index()
        {
            var allOffices= officeRepository.GetAll();
            var allAddresses = addressRepository.GetAll();
            var allDistricts= addressRepository.GetAllDistricts();
            var allCountries= addressRepository.GetAllCountries();


            var List = (from office in allOffices
                        join address in allAddresses on office.AddressId equals address.AddressId
                        join dist in allDistricts on address.DistrictId equals dist.DistrictId
                        join cnt in allCountries on dist.CountryId equals cnt.CountryId
                        select new
                        {
                            office.OfficeId,
                            office.OfficeName,
                            address.HouseNo,
                            address.RoadNo,
                            address.Area,
                            address.Block,
                            address.PostCode,
                            address.CountryId,
                            cnt.CountryName,
                            address.DistrictId,
                            dist.DistrictName
                        }).ToList();
            //   var list = officeRepository.GetAll();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(OfficeVM officeVm)
        {
            int row = officeRepository.Insert(officeVm);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(OfficeVM officeVm)
        {
            int row = officeRepository.Edit(officeVm);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int officeId)
        {
            Office office = new Office { OfficeId = officeId };
            int row = officeRepository.Delete(officeId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAddressInfo()
        {
            var allAddresses = addressRepository.GetAll();
            var list = (from address in allAddresses
                        select new
                        {
                            address.AddressId,
                            address.Area
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCountryInfo()
        {
            var allCountries = addressRepository.GetAllCountries();


            var list = (from cntry in allCountries
                        select new
                        {
                            cntry.CountryId,
                            cntry.CountryName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictInfo()
        {
            var allDistricts = addressRepository.GetAllDistricts();
            var list = (from dist in allDistricts
                        select new
                        {
                            dist.DistrictId,
                            dist.DistrictName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictsByCountry(int countryId)
        {
            var datalist = addressRepository.RetrieveDistrictsByCountry(countryId);
            return Json(datalist, JsonRequestBehavior.AllowGet);
        }
    }
}