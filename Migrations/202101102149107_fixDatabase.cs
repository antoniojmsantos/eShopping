namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compras", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Compras", "IdProduto", "dbo.Produtoes");
            DropIndex("dbo.Compras", new[] { "IdProduto" });
            DropIndex("dbo.Compras", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        IdFuncionario = c.Int(nullable: false, identity: true),
                        IdEmpresa = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdFuncionario)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Empresas", t => t.IdEmpresa, cascadeDelete: true)
                .Index(t => t.IdEmpresa)
                .Index(t => t.ApplicationUserId);
            
            DropTable("dbo.Compras");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        IdCompra = c.Int(nullable: false, identity: true),
                        Unidades = c.Int(nullable: false),
                        DataPedido = c.DateTime(nullable: false),
                        DataConfirmacao = c.DateTime(),
                        DataEntrega = c.DateTime(),
                        Estado = c.Int(nullable: false),
                        IdProduto = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdCompra);
            
            DropForeignKey("dbo.Funcionarios", "IdEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.Funcionarios", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Funcionarios", new[] { "ApplicationUserId" });
            DropIndex("dbo.Funcionarios", new[] { "IdEmpresa" });
            DropTable("dbo.Funcionarios");
            CreateIndex("dbo.Compras", "ApplicationUserId");
            CreateIndex("dbo.Compras", "IdProduto");
            AddForeignKey("dbo.Compras", "IdProduto", "dbo.Produtoes", "IdProduto", cascadeDelete: true);
            AddForeignKey("dbo.Compras", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
