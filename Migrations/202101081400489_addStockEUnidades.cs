namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStockEUnidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Unidades", c => c.Int(nullable: false));
            AddColumn("dbo.Produtoes", "UnidadesEmStock", c => c.Int(nullable: false));
            AddColumn("dbo.Produtoes", "EmStock", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtoes", "EmStock");
            DropColumn("dbo.Produtoes", "UnidadesEmStock");
            DropColumn("dbo.Compras", "Unidades");
        }
    }
}
