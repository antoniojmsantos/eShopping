namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCompraUserId2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Compras", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Compras", "ApplicationUserId");
            RenameColumn(table: "dbo.Compras", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Compras", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Compras", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Compras", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Compras", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Compras", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Compras", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Compras", "ApplicationUser_Id");
        }
    }
}
