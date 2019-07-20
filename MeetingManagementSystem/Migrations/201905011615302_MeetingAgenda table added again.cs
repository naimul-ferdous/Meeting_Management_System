namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingAgendatableaddedagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingAgendas",
                c => new
                    {
                        MeetingAgendaId = c.Int(nullable: false, identity: true),
                        MeetingAgendaName = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        MeetingId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MeetingAgendaId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.MeetingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingAgendas", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.MeetingAgendas", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.MeetingAgendas", new[] { "MeetingId" });
            DropIndex("dbo.MeetingAgendas", new[] { "EmployeeId" });
            DropTable("dbo.MeetingAgendas");
        }
    }
}
