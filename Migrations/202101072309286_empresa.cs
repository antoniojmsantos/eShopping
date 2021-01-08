namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Companies", new[] { "Id" });
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        IdEmpresa = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdEmpresa)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Produtoes", "IdEmpresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Produtoes", "IdEmpresa");
            AddForeignKey("dbo.Produtoes", "IdEmpresa", "dbo.Empresas", "IdEmpresa", cascadeDelete: true);
            DropTable("dbo.Companies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        IdCompany = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdCompany);
            
            DropForeignKey("dbo.Produtoes", "IdEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.Empresas", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Empresas", new[] { "Id" });
            DropIndex("dbo.Produtoes", new[] { "IdEmpresa" });
            DropColumn("dbo.Produtoes", "IdEmpresa");
            DropTable("dbo.Empresas");
            CreateIndex("dbo.Companies", "Id");
            AddForeignKey("dbo.Companies", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
