namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        NomeCategoria = c.String(),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        IdCompra = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdCompra)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdProduto)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        IdProduto = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Preco = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduto);
            
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String());
            DropColumn("dbo.AspNetUsers", "NomeCompleto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NomeCompleto", c => c.String());
            DropForeignKey("dbo.Compras", "IdProduto", "dbo.Produtoes");
            DropForeignKey("dbo.Compras", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Compras", new[] { "Id" });
            DropIndex("dbo.Compras", new[] { "IdProduto" });
            DropColumn("dbo.AspNetUsers", "Nome");
            DropTable("dbo.Produtoes");
            DropTable("dbo.Compras");
            DropTable("dbo.Categorias");
        }
    }
}
