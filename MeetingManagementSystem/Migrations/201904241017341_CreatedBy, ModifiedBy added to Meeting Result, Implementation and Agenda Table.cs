namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedByModifiedByaddedtoMeetingResultImplementationandAgendaTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Implementations", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Implementations", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Implementations", "ModifiedById", c => c.Int());
            AddColumn("dbo.Implementations", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.MeetingResults", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.MeetingResults", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MeetingResults", "ModifiedById", c => c.Int());
            AddColumn("dbo.MeetingResults", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.MeetingAgendas", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.MeetingAgendas", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MeetingAgendas", "ModifiedById", c => c.Int());
            AddColumn("dbo.MeetingAgendas", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingAgendas", "ModifiedDate");
            DropColumn("dbo.MeetingAgendas", "ModifiedById");
            DropColumn("dbo.MeetingAgendas", "CreatedDate");
            DropColumn("dbo.MeetingAgendas", "CreatedById");
            DropColumn("dbo.MeetingResults", "ModifiedDate");
            DropColumn("dbo.MeetingResults", "ModifiedById");
            DropColumn("dbo.MeetingResults", "CreatedDate");
            DropColumn("dbo.MeetingResults", "CreatedById");
            DropColumn("dbo.Implementations", "ModifiedDate");
            DropColumn("dbo.Implementations", "ModifiedById");
            DropColumn("dbo.Implementations", "CreatedDate");
            DropColumn("dbo.Implementations", "CreatedById");
        }
    }
}
