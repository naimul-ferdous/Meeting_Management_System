namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableMeetingMembermodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingMembers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.MeetingMembers", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.MeetingMembers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MeetingMembers", "ModifiedById", c => c.Int());
            AddColumn("dbo.MeetingMembers", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingMembers", "ModifiedDate");
            DropColumn("dbo.MeetingMembers", "ModifiedById");
            DropColumn("dbo.MeetingMembers", "CreatedDate");
            DropColumn("dbo.MeetingMembers", "CreatedById");
            DropColumn("dbo.MeetingMembers", "IsDeleted");
        }
    }
}
