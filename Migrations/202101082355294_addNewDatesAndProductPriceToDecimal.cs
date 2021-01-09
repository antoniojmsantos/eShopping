namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewDatesAndProductPriceToDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "DataPedido", c => c.DateTime(nullable: false));
            AddColumn("dbo.Compras", "DataConfirmacao", c => c.DateTime());
            AddColumn("dbo.Compras", "DataEntrega", c => c.DateTime());
            AlterColumn("dbo.Produtoes", "Preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Compras", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compras", "Data", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Produtoes", "Preco", c => c.Single(nullable: false));
            DropColumn("dbo.Compras", "DataEntrega");
            DropColumn("dbo.Compras", "DataConfirmacao");
            DropColumn("dbo.Compras", "DataPedido");
        }
    }
}
