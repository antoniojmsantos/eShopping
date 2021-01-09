namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promocao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promocaos",
                c => new
                    {
                        IdPromocao = c.Int(nullable: false, identity: true),
                        IdProduto = c.Int(nullable: false),
                        Percentagem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPromocao)
                .ForeignKey("dbo.Produtoes", t => t.IdProduto, cascadeDelete: true)
                .Index(t => t.IdProduto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promocaos", "IdProduto", "dbo.Produtoes");
            DropIndex("dbo.Promocaos", new[] { "IdProduto" });
            DropTable("dbo.Promocaos");
        }
    }
}
