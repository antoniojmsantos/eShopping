namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresas_extension : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        idEmpresa = c.Int(nullable: false, identity: true),
                        nomeEmpresa = c.String(),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.idEmpresa)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empresas", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Empresas", new[] { "Id" });
            DropTable("dbo.Empresas");
        }
    }
}
