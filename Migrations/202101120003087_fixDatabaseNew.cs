namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDatabaseNew : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Produtoes", newName: "Produtos");
            RenameTable(name: "dbo.Promocaos", newName: "Promocoes");
            DropForeignKey("dbo.Funcionarios", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Funcionarios", "IdEmpresa", "dbo.Empresas");
            DropIndex("dbo.Funcionarios", new[] { "IdEmpresa" });
            DropIndex("dbo.Funcionarios", new[] { "ApplicationUserId" });
            RenameColumn(table: "dbo.Categorias", name: "NomeCategoria", newName: "Nome");
            RenameColumn(table: "dbo.Empresas", name: "NomeEmpresa", newName: "Nome");
            CreateTable(
                "dbo.EmpresaFuncionarios",
                c => new
                    {
                        IdEmpresa = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdEmpresa, t.ApplicationUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Empresas", t => t.IdEmpresa, cascadeDelete: true)
                .Index(t => t.IdEmpresa)
                .Index(t => t.ApplicationUserId);
            
            DropTable("dbo.Funcionarios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        IdFuncionario = c.Int(nullable: false, identity: true),
                        IdEmpresa = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdFuncionario);
            
            DropForeignKey("dbo.EmpresaFuncionarios", "IdEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.EmpresaFuncionarios", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.EmpresaFuncionarios", new[] { "ApplicationUserId" });
            DropIndex("dbo.EmpresaFuncionarios", new[] { "IdEmpresa" });
            DropTable("dbo.EmpresaFuncionarios");
            RenameColumn(table: "dbo.Empresas", name: "Nome", newName: "NomeEmpresa");
            RenameColumn(table: "dbo.Categorias", name: "Nome", newName: "NomeCategoria");
            CreateIndex("dbo.Funcionarios", "ApplicationUserId");
            CreateIndex("dbo.Funcionarios", "IdEmpresa");
            AddForeignKey("dbo.Funcionarios", "IdEmpresa", "dbo.Empresas", "IdEmpresa", cascadeDelete: true);
            AddForeignKey("dbo.Funcionarios", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.Promocoes", newName: "Promocaos");
            RenameTable(name: "dbo.Produtos", newName: "Produtoes");
        }
    }
}
