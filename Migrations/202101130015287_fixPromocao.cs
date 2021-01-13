namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixPromocao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promocoes", "PrecoNovo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promocoes", "Ativa", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promocoes", "Ativa");
            DropColumn("dbo.Promocoes", "PrecoNovo");
        }
    }
}
