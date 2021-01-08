namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        IdCompra = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdCompra)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Produtoes", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdProduto)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compras", "IdProduto", "dbo.Produtoes");
            DropForeignKey("dbo.Compras", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Compras", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Compras", new[] { "IdProduto" });
            DropTable("dbo.Compras");
        }
    }
}
