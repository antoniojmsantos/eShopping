namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDataToLinhaCOmpra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LinhaCompras", "Estado", c => c.Int(nullable: false));
            AddColumn("dbo.LinhaCompras", "DataCriada", c => c.DateTime(nullable: false));
            AddColumn("dbo.LinhaCompras", "DataConfirmada", c => c.DateTime());
            DropColumn("dbo.Compras", "DataCriada");
            DropColumn("dbo.Compras", "DataConfirmada");
            DropColumn("dbo.Compras", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compras", "Estado", c => c.Int(nullable: false));
            AddColumn("dbo.Compras", "DataConfirmada", c => c.DateTime());
            AddColumn("dbo.Compras", "DataCriada", c => c.DateTime(nullable: false));
            DropColumn("dbo.LinhaCompras", "DataConfirmada");
            DropColumn("dbo.LinhaCompras", "DataCriada");
            DropColumn("dbo.LinhaCompras", "Estado");
        }
    }
}
