namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameandRoleNameUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SecRoles", "RoleName", unique: true);
            CreateIndex("dbo.SecUsers", "LoginName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SecUsers", new[] { "LoginName" });
            DropIndex("dbo.SecRoles", new[] { "RoleName" });
        }
    }
}
