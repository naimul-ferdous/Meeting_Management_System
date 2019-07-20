namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedByandCreatedDatenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecUsers", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecUsers", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.Int(nullable: false));
        }
    }
}
