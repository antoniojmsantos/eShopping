namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NomeCompleto", c => c.String());
            AddColumn("dbo.Empresas", "NomeEmpresa", c => c.String());
            DropColumn("dbo.AspNetUsers", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String());
            DropColumn("dbo.Empresas", "NomeEmpresa");
            DropColumn("dbo.AspNetUsers", "NomeCompleto");
        }
    }
}
