using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class VenueController : Controller
    {
        VenueRepository venueRepository = new VenueRepository();
        AddressRepository addressRepository=    new AddressRepository();
        public ActionResult Venue()
        {
            return View();
        }

        public ActionResult Create(VenueVM venueVm)
        {
            int rowAff = venueRepository.Insert(venueVm);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(VenueVM venueVm)
        {
            int rowAff = venueRepository.Edit(venueVm);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int venueId)
        {
            int row = venueRepository.Delete(venueId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVenueWithAddressInfo()
        {
            var allVenues= venueRepository.GetAll();
            var allAddresses= addressRepository.GetAll();
            var allDistricts = addressRepository.GetAllDistricts();
            var allCountries = addressRepository.GetAllCountries();

            var dataList = (from venue in allVenues
                                //.AsEnumerable()
                            join address in allAddresses on venue.AddressId equals address.AddressId
                            join dist in allDistricts on address.DistrictId equals dist.DistrictId
                            join cnt in allCountries on dist.CountryId equals cnt.CountryId
                            select new
                            {
                                venue.VenueId,
                                venue.VenueName,
                                venue.Capacity,
                                venue.VenueType,
                                //venueTypeName = venue.VenueType==1?"Internal":"External",
                                address.HouseNo,
                                address.RoadNo,
                                address.Block,
                                address.Area,
                                address.PostCode,
                                address.CountryId,
                                cnt.CountryName,
                                address.DistrictId,
                                dist.DistrictName

                                
                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVenueTypeList()
        {
            var dataList = new List<SelectListItem>(){
                new SelectListItem{Value = "1",Text = "Internal"},
                new SelectListItem{Value = "2",Text = "External"}
            };
            return Json(dataList, JsonRequestBehavior.AllowGet);
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
            return Json(list,JsonRequestBehavior.AllowGet);
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