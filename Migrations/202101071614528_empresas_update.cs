namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresas_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String());
            DropColumn("dbo.Empresas", "nomeEmpresa");
            DropColumn("dbo.AspNetUsers", "NomeCompleto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NomeCompleto", c => c.String());
            AddColumn("dbo.Empresas", "nomeEmpresa", c => c.String());
            DropColumn("dbo.AspNetUsers", "Nome");
        }
    }
}
