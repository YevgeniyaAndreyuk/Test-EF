namespace Test_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupPermisions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        GroupPermision_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GroupPermisions", t => t.GroupPermision_ID)
                .Index(t => t.GroupPermision_ID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupPermision_ID = c.Int(),
                        UserPermissions_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GroupPermisions", t => t.GroupPermision_ID)
                .ForeignKey("dbo.UserPermissions", t => t.UserPermissions_ID)
                .Index(t => t.GroupPermision_ID)
                .Index(t => t.UserPermissions_ID);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        refGroup = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        isLocked = c.Boolean(nullable: false),
                        isOnline = c.Boolean(nullable: false),
                        etc = c.String(),
                        GroupUsers_ID = c.Int(),
                        UserPermissions_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GroupUsers", t => t.GroupUsers_ID)
                .ForeignKey("dbo.UserPermissions", t => t.UserPermissions_ID)
                .Index(t => t.GroupUsers_ID)
                .Index(t => t.UserPermissions_ID);
            
            CreateTable(
                "dbo.UserPermissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserPermissions_ID", "dbo.UserPermissions");
            DropForeignKey("dbo.Permissions", "UserPermissions_ID", "dbo.UserPermissions");
            DropForeignKey("dbo.Users", "GroupUsers_ID", "dbo.GroupUsers");
            DropForeignKey("dbo.Permissions", "GroupPermision_ID", "dbo.GroupPermisions");
            DropForeignKey("dbo.Groups", "GroupPermision_ID", "dbo.GroupPermisions");
            DropIndex("dbo.Users", new[] { "UserPermissions_ID" });
            DropIndex("dbo.Users", new[] { "GroupUsers_ID" });
            DropIndex("dbo.Permissions", new[] { "UserPermissions_ID" });
            DropIndex("dbo.Permissions", new[] { "GroupPermision_ID" });
            DropIndex("dbo.Groups", new[] { "GroupPermision_ID" });
            DropTable("dbo.UserPermissions");
            DropTable("dbo.Users");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.Permissions");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupPermisions");
        }
    }
}
