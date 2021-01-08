namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecompras : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compras", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Compras", "IdProduto", "dbo.Produtoes");
            DropIndex("dbo.Compras", new[] { "IdProduto" });
            DropIndex("dbo.Compras", new[] { "Id" });
            DropTable("dbo.Compras");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        IdCompra = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdCompra);
            
            CreateIndex("dbo.Compras", "Id");
            CreateIndex("dbo.Compras", "IdProduto");
            AddForeignKey("dbo.Compras", "IdProduto", "dbo.Produtoes", "IdProduto", cascadeDelete: true);
            AddForeignKey("dbo.Compras", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
