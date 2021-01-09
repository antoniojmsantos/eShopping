namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cat_produto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtoes", "IdCategoria", c => c.Int(nullable: false));
            CreateIndex("dbo.Produtoes", "IdCategoria");
            AddForeignKey("dbo.Produtoes", "IdCategoria", "dbo.Categorias", "IdCategoria", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtoes", "IdCategoria", "dbo.Categorias");
            DropIndex("dbo.Produtoes", new[] { "IdCategoria" });
            DropColumn("dbo.Produtoes", "IdCategoria");
        }
    }
}
