namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        IdCompra = c.Int(nullable: false, identity: true),
                        DataCriada = c.DateTime(nullable: false),
                        Estado = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdCompra)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.LinhaCompras",
                c => new
                    {
                        IdLinhaCompra = c.Int(nullable: false, identity: true),
                        Unidades = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdCompra = c.Int(nullable: false),
                        IdProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLinhaCompra)
                .ForeignKey("dbo.Compras", t => t.IdCompra, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdCompra)
                .Index(t => t.IdProduto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinhaCompras", "IdProduto", "dbo.Produtoes");
            DropForeignKey("dbo.LinhaCompras", "IdCompra", "dbo.Compras");
            DropForeignKey("dbo.Compras", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.LinhaCompras", new[] { "IdProduto" });
            DropIndex("dbo.LinhaCompras", new[] { "IdCompra" });
            DropIndex("dbo.Compras", new[] { "ApplicationUserId" });
            DropTable("dbo.LinhaCompras");
            DropTable("dbo.Compras");
        }
    }
}
