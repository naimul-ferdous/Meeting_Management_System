namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingAgenda_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingAgendas",
                c => new
                    {
                        MeetingAgendaId = c.Int(nullable: false, identity: true),
                        MeetingAgendaName = c.String(),
                        Presenter = c.String(),
                        MeetingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingAgendaId)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: true)
                .Index(t => t.MeetingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingAgendas", "MeetingId", "dbo.Meetings");
            DropIndex("dbo.MeetingAgendas", new[] { "MeetingId" });
            DropTable("dbo.MeetingAgendas");
        }
    }
}
