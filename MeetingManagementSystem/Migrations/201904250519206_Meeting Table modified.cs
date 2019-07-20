namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingTablemodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Meetings", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Meetings", "ModifiedById", c => c.Int());
            AddColumn("dbo.Meetings", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Meetings", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings", "IsDeleted");
            DropColumn("dbo.Meetings", "ModifiedDate");
            DropColumn("dbo.Meetings", "ModifiedById");
            DropColumn("dbo.Meetings", "CreatedDate");
            DropColumn("dbo.Meetings", "CreatedById");
        }
    }
}
