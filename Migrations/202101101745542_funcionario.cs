namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class funcionario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        IdFuncionario = c.Int(nullable: false, identity: true),
                        IdEmpresa = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        Empresa_IdEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.IdFuncionario)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Empresas", t => t.Empresa_IdEmpresa)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Empresa_IdEmpresa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionarios", "Empresa_IdEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.Funcionarios", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Funcionarios", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Funcionarios", new[] { "ApplicationUserId" });
            DropTable("dbo.Funcionarios");
        }
    }
}
