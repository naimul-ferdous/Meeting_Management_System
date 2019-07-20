namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        HouseNo = c.String(),
                        RoadNo = c.String(),
                        Block = c.String(),
                        Area = c.String(),
                        PostCode = c.String(),
                        CountryId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        VenueId = c.Int(nullable: false, identity: true),
                        VenueName = c.String(),
                        Capacity = c.String(),
                        VenueType = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VenueId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Logistics",
                c => new
                    {
                        LogisticId = c.Int(nullable: false, identity: true),
                        LogisticName = c.String(),
                        Availbale = c.String(),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LogisticId)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        MeetingId = c.Int(nullable: false, identity: true),
                        MeetingName = c.String(),
                        BeginningTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        MeetingDescription = c.String(),
                        Status = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                        Approval = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Password = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Offices", t => t.OfficeId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.OfficeId)
                .Index(t => t.DesignationId)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        EmployeeTypeId = c.Int(nullable: false, identity: true),
                        EmployeeTypeName = c.String(),
                        EmployeeTypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.Implementations",
                c => new
                    {
                        ImplementationId = c.Int(nullable: false, identity: true),
                        ImplementationDescription = c.String(),
                        MeetingId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImplementationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        OfficeId = c.Int(nullable: false, identity: true),
                        OfficeName = c.String(),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OfficeId);
            
            CreateTable(
                "dbo.ResultAndDiscussions",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        Announcement = c.String(),
                        Result = c.String(),
                        Status = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        MeetingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Entertainments",
                c => new
                    {
                        EntertainmentId = c.Int(nullable: false, identity: true),
                        EntertainmentItem = c.String(),
                        Quantity = c.Int(nullable: false),
                        MeetingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntertainmentId)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: true)
                .Index(t => t.MeetingId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.ExternalMembers",
                c => new
                    {
                        ExternalMemberId = c.Int(nullable: false, identity: true),
                        ExternalMemberName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Profession = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ExternalMemberId);
            
            CreateTable(
                "dbo.MeetingExternalMembers",
                c => new
                    {
                        MeetingExternalMemberId = c.Int(nullable: false, identity: true),
                        MeetingId = c.Int(nullable: false),
                        ExternalMemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingExternalMemberId);
            
            CreateTable(
                "dbo.MeetingLoggers",
                c => new
                    {
                        MeetingLoggerId = c.Int(nullable: false, identity: true),
                        MeetingId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                        BeginningTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.MeetingLoggerId)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: false)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: false)
                .Index(t => t.MeetingId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.MeetingMembers",
                c => new
                    {
                        MeetingMemberId = c.Int(nullable: false, identity: true),
                        MeetingId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Attendance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingMemberId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        ResourceName = c.String(),
                        ResourcePath = c.String(),
                    })
                .PrimaryKey(t => t.ResourceId);
            
            CreateTable(
                "dbo.RoleEmployees",
                c => new
                    {
                        RoleEmployeeId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleEmployeeId);
            
            CreateTable(
                "dbo.RoleResourcePermissions",
                c => new
                    {
                        RoleResourcePermissionId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleResourcePermissionId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingLoggers", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.MeetingLoggers", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.Districts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Meetings", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Entertainments", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.ResultAndDiscussions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "OfficeId", "dbo.Offices");
            DropForeignKey("dbo.Meetings", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Implementations", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Logistics", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Venues", "AddressId", "dbo.Addresses");
            DropIndex("dbo.MeetingLoggers", new[] { "VenueId" });
            DropIndex("dbo.MeetingLoggers", new[] { "MeetingId" });
            DropIndex("dbo.Districts", new[] { "CountryId" });
            DropIndex("dbo.Entertainments", new[] { "MeetingId" });
            DropIndex("dbo.ResultAndDiscussions", new[] { "EmployeeId" });
            DropIndex("dbo.Implementations", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.Employees", new[] { "OfficeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Meetings", new[] { "VenueId" });
            DropIndex("dbo.Meetings", new[] { "EmployeeId" });
            DropIndex("dbo.Logistics", new[] { "VenueId" });
            DropIndex("dbo.Venues", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "DistrictId" });
            DropTable("dbo.Roles");
            DropTable("dbo.RoleResourcePermissions");
            DropTable("dbo.RoleEmployees");
            DropTable("dbo.Resources");
            DropTable("dbo.MeetingMembers");
            DropTable("dbo.MeetingLoggers");
            DropTable("dbo.MeetingExternalMembers");
            DropTable("dbo.ExternalMembers");
            DropTable("dbo.Districts");
            DropTable("dbo.Countries");
            DropTable("dbo.Entertainments");
            DropTable("dbo.ResultAndDiscussions");
            DropTable("dbo.Offices");
            DropTable("dbo.Implementations");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Designations");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Meetings");
            DropTable("dbo.Logistics");
            DropTable("dbo.Venues");
            DropTable("dbo.Addresses");
        }
    }
}
