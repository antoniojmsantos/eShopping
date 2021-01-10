namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class funcionariofix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Funcionarios", "Empresa_IdEmpresa", "dbo.Empresas");
            DropIndex("dbo.Funcionarios", new[] { "Empresa_IdEmpresa" });
            DropColumn("dbo.Funcionarios", "IdEmpresa");
            RenameColumn(table: "dbo.Funcionarios", name: "Empresa_IdEmpresa", newName: "IdEmpresa");
            AlterColumn("dbo.Funcionarios", "IdEmpresa", c => c.Int(nullable: false));
            AlterColumn("dbo.Funcionarios", "IdEmpresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Funcionarios", "IdEmpresa");
            AddForeignKey("dbo.Funcionarios", "IdEmpresa", "dbo.Empresas", "IdEmpresa", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionarios", "IdEmpresa", "dbo.Empresas");
            DropIndex("dbo.Funcionarios", new[] { "IdEmpresa" });
            AlterColumn("dbo.Funcionarios", "IdEmpresa", c => c.Int());
            AlterColumn("dbo.Funcionarios", "IdEmpresa", c => c.String());
            RenameColumn(table: "dbo.Funcionarios", name: "IdEmpresa", newName: "Empresa_IdEmpresa");
            AddColumn("dbo.Funcionarios", "IdEmpresa", c => c.String());
            CreateIndex("dbo.Funcionarios", "Empresa_IdEmpresa");
            AddForeignKey("dbo.Funcionarios", "Empresa_IdEmpresa", "dbo.Empresas", "IdEmpresa");
        }
    }
}
