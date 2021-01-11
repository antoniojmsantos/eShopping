namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCompras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "DataConfirmada", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Compras", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Compras", "Total", c => c.Int(nullable: false));
            DropColumn("dbo.Compras", "DataConfirmada");
        }
    }
}
