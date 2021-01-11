namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCompras2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Compras", "DataConfirmada", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Compras", "DataConfirmada", c => c.DateTime(nullable: false));
        }
    }
}
