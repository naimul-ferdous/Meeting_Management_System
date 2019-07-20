using System.Data.Entity;

namespace MeetingManagementSystem.Models
{
   public class MeetingManagementDbContext:DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Entertainment> Entertainments { get; set; }
        public DbSet<ExternalMember> ExternalMembers { get; set; }
        public DbSet<Implementation> Implementations { get; set; }
        public DbSet<Logistic> Logistics { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<MeetingExternalMember> MeetingExternalMembers { get; set; }
        public DbSet<MeetingMember> MeetingMembers { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<SecResource> SecResources { get; set; }
        public DbSet<MeetingResult> MeetingResults { get; set; }

        public DbSet<SecRole> SecRoles { get; set; }
        //public DbSet<RoleEmployee> RoleEmployees { get; set; }
       // public DbSet<RoleResourcePermission> RoleResourcePermissions { get; set; }

       // public DbSet<SecRole> Roles { get; set; }
       // public DbSet<RoleEmployee> RoleEmployees { get; set; }

        public DbSet<SecResourcePermission> SecResourcePermissions { get; set; }

        //public DbSet<SecResourcePermission> SecResourcePermissions { get; set; }



        public DbSet<Venue> Venues { get; set; }
        //public DbSet<CountryName> CountryNames { get; set; }
        public DbSet<Country> Countries { get; set; }
        //public DbSet<City> Cities { get; set; }public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<MeetingLogger> MeetingLoggers { get; set; }
        public DbSet<MeetingAgenda> MeetingAgendas { get; set; }
        public DbSet<MeetingFileUpload> MeetingFileUploads { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<SecUser> SecUsers { get; set; }

        public DbSet<SecRolePermission> SecRolePermissions { get; set; }

        public DbSet<SecUserRole> SecUserRoles { get; set; }
    }
}
