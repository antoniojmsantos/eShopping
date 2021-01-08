namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresasUsers1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Empresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Empresa", c => c.String());
        }
    }
}
