namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtoApagadoNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtos", "Apagado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtos", "Apagado");
        }
    }
}
