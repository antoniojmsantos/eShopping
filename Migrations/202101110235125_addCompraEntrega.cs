namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompraEntrega : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Entrega", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "Entrega");
        }
    }
}
