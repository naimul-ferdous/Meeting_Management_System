namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingAgendatabledeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeetingAgendas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.MeetingAgendas", "MeetingId", "dbo.Meetings");
            DropIndex("dbo.MeetingAgendas", new[] { "EmployeeId" });
            DropIndex("dbo.MeetingAgendas", new[] { "MeetingId" });
            DropTable("dbo.MeetingAgendas");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.MeetingAgendaId);
            
            CreateIndex("dbo.MeetingAgendas", "MeetingId");
            CreateIndex("dbo.MeetingAgendas", "EmployeeId");
            AddForeignKey("dbo.MeetingAgendas", "MeetingId", "dbo.Meetings", "MeetingId", cascadeDelete: true);
            AddForeignKey("dbo.MeetingAgendas", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
