namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedaddedtotableMeetingFileUpload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingFileUploads", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingFileUploads", "IsDeleted");
        }
    }
}
