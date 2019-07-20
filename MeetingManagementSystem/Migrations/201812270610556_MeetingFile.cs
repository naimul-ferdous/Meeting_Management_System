namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingFileUploads",
                c => new
                    {
                        MeetingFileUploadId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        MeetingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingFileUploadId)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: true)
                .Index(t => t.MeetingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingFileUploads", "MeetingId", "dbo.Meetings");
            DropIndex("dbo.MeetingFileUploads", new[] { "MeetingId" });
            DropTable("dbo.MeetingFileUploads");
        }
    }
}
