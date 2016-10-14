namespace Test_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "etc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "etc", c => c.String());
        }
    }
}
