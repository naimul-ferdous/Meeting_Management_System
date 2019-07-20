using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.ViewModels;
using System;

namespace MeetingManagementSystem.Repositories
{
    public class OfficeRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        AddressRepository addressRepository = new AddressRepository();
        public int Insert(OfficeVM officeVm)
        {
            Address address = new Address();
            address.HouseNo = officeVm.HouseNo;
            address.RoadNo = officeVm.RoadNo;
            address.Area = officeVm.Area;
            address.Block = officeVm.Block;
            address.PostCode = officeVm.PostCode;
            address.DistrictId = officeVm.DistrictId;
            address.CountryId = officeVm.CountryId;
            addressRepository.Insert(address);
            Office office = new Office();
            office.OfficeName = officeVm.OfficeName;
            office.AddressId = context.Addresses.Max(c => c.AddressId);
            context.Offices.Add(office);
            return context.SaveChanges();
        }
        public int Edit(OfficeVM officeVm)
        {
            Office office = context.Offices.Where(c => c.OfficeId == officeVm.OfficeId).FirstOrDefault();
            Address address = context.Addresses.Where(c => c.AddressId == office.AddressId).FirstOrDefault();
            address.HouseNo = officeVm.HouseNo;
            address.RoadNo = officeVm.RoadNo;
            address.Area = officeVm.Area;
            address.Block = officeVm.Block;
            address.PostCode = officeVm.PostCode;
            address.DistrictId = officeVm.DistrictId;
            address.CountryId = officeVm.CountryId;
            addressRepository.Edit(address);

            office.OfficeName = officeVm.OfficeName;
            context.Entry(office).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            Office office = context.Offices.Where(c => c.OfficeId == id).FirstOrDefault();
            Address address = context.Addresses.Where(c => c.AddressId == office.AddressId).FirstOrDefault();

            addressRepository.Delete(address.AddressId);
            //Venue venue = new Venue() { VenueId = venueId};
            context.Entry(office).State = System.Data.Entity.EntityState.Deleted;
            try
            {
                return context.SaveChanges();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 1;
        }
        public Office Get(int id)
        {
            return context.Offices.SingleOrDefault(o => o.OfficeId == id);
        }
        public List<Office> GetAll()
        {
            return context.Offices.ToList();
        }
    }
}