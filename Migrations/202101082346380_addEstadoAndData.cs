namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEstadoAndData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.Compras", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "Estado");
            DropColumn("dbo.Compras", "Data");
        }
    }
}
