namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataEntrega : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LinhaCompras", "DataEntrega", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LinhaCompras", "DataEntrega");
        }
    }
}
