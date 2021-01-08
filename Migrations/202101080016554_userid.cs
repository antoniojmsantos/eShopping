namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Empresas", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Empresas", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Empresas", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Empresas", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
