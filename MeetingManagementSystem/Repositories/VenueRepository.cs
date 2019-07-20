using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Repositories
{
    public class VenueRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        AddressRepository repository = new AddressRepository();
        public int Insert(VenueVM venueVm)
        {
            Address address = new Address();
            address.HouseNo = venueVm.HouseNo;
            address.RoadNo = venueVm.RoadNo;
            address.Area = venueVm.Area;
            address.Block = venueVm.Block;
            address.PostCode = venueVm.PostCode;
            address.DistrictId = venueVm.DistrictId;
            address.CountryId = venueVm.CountryId;
            repository.Insert(address);
            Venue venue = new Venue();
            venue.VenueName = venueVm.VenueName;
            venue.Capacity = venueVm.Capacity;
            venue.VenueType = venueVm.VenueType;
            venue.AddressId = context.Addresses.Max(c => c.AddressId);
            context.Venues.Add(venue);
            return context.SaveChanges();
        }
        public int Edit(VenueVM venueVm)
        {
            Venue venue = context.Venues.Where(c => c.VenueId == venueVm.VenueId).FirstOrDefault();
            Address address = context.Addresses.Where(c => c.AddressId == venue.AddressId).FirstOrDefault();
            venue.AddressId = context.Addresses.Max(c => c.AddressId);
            address.HouseNo = venueVm.HouseNo;
            address.RoadNo = venueVm.RoadNo;
            address.Area = venueVm.Area;
            address.Block = venueVm.Block;
            address.PostCode = venueVm.PostCode;
            address.DistrictId = venueVm.DistrictId;
            address.CountryId = venueVm.CountryId;
            repository.Edit(address);

            venue.VenueName = venueVm.VenueName;
            venue.Capacity = venueVm.Capacity;
            venue.VenueType = venueVm.VenueType;
            
            context.Entry(venue).State = EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int venueId)
        {
            Venue venue = context.Venues.Where(c => c.VenueId == venueId).FirstOrDefault();
            Address address = context.Addresses.Where(c => c.AddressId == venue.AddressId).FirstOrDefault();
            
            repository.Delete(address.AddressId);
            //Venue venue = new Venue() { VenueId = venueId};
            context.Entry(venue).State = EntityState.Deleted;
            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
            }

            return 1;


        }
        public Venue Get(int id)
        {
            return context.Venues.SingleOrDefault(v => v.VenueId == id);
        }
        public List<Venue> GetAll()
        {
            return context.Venues.ToList();
        }

        
    }
}