using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MeetingManagementSystem.Models;
using System.IO;

namespace MeetingManagementSystem.Repositories
{
    public class AddressRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public void country()
        {
            MeetingManagementDbContext context = new MeetingManagementDbContext();
            //List<CountryName> co = context.CountryNames.ToList();
            //foreach (var c in co)
            //{
            //    Country country= context.Countries.SingleOrDefault(cc => cc.CountryName == c.Country);
            //    if (country == null)
            //    {
            //        Country count = new Country();
            //        count.CountryName = c.Country;
            //        context.Countries.Add(count);
            //        context.SaveChanges();
            //    }
            // }
            //foreach(var c in co)
            //{
            //    Country country = context.Countries.SingleOrDefault(cc => cc.CountryName == c.Country);
            //    City city = new City();
            //    city.CityName = c.City;
            //    city.CountryId = country.CountryId;
            //    context.Cities.Add(city);
            //    context.SaveChanges();
            //}
            //var reader = new StreamReader("G:/code/ASP.NET/Systech/v1/meetingmanagement/MeetingManagementSystem/worldcities.csv");
            //CountryName countryName = new CountryName();
            //List<string> listA = new List<string>();
            //List<string> listB = new List<string>();
            //while (!reader.EndOfStream)
            //{
            //    var line = reader.ReadLine();
            //    var values = line.Split(',');
            //    countryName.City = values[0];
            //    countryName.Country = values[1];
            //    context.CountryNames.Add(countryName);
            //    context.SaveChanges();
            //    listA.Add(values[0]);
            //    listB.Add(values[1]);
            //}

        }
        public int Insert (Address address)
        {
            context.Addresses.Add(address);
            return context.SaveChanges();
        }
        public int Edit(Address address)
        {
            Address newAddress = context.Addresses.FirstOrDefault(c => c.AddressId == address.AddressId);
            if (newAddress != null)
            {
                newAddress.DistrictId = address.DistrictId;
                newAddress.CountryId = address.CountryId;
                newAddress.PostCode = address.PostCode;
                context.Addresses.Attach(newAddress);
            }

            
            //context.Entry(address).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int addressId)
        {
            Address address = new Address() { AddressId = addressId };
            context.Entry(address).State = EntityState.Deleted;
            int rowAff =  context.SaveChanges();
            return rowAff;
        }
        public Address Get(int id)
        {
            return context.Addresses.SingleOrDefault(a => a.AddressId == id);
        }
        public List<Address> GetAll()
        {
            return context.Addresses.ToList();
        }

        public List<District> GetAllDistricts()
        {
            return context.Districts.ToList();
        }

        public List<Country> GetAllCountries()
        {
            return context.Countries.ToList();
        }

        public List<District> RetrieveDistrictsByCountry(int countryId)
        {
            return context.Districts.Where(c => c.CountryId == countryId).ToList();
        }
    }
}