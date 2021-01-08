namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userfix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Empresas", name: "Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Empresas", name: "IX_Id", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Empresas", name: "IX_ApplicationUser_Id", newName: "IX_Id");
            RenameColumn(table: "dbo.Empresas", name: "ApplicationUser_Id", newName: "Id");
        }
    }
}
