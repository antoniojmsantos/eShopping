namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCompraUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "ApplicationUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "ApplicationUserId");
        }
    }
}
