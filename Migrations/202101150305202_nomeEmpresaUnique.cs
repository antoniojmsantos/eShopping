namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomeEmpresaUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empresas", "Nome", c => c.String(maxLength: 50));
            CreateIndex("dbo.Empresas", "Nome", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Empresas", new[] { "Nome" });
            AlterColumn("dbo.Empresas", "Nome", c => c.String());
        }
    }
}
