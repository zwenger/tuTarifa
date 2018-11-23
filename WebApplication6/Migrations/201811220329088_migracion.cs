namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Viajes", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Viajes", new[] { "Usuario_Id" });
            AlterColumn("dbo.Viajes", "Usuario_Id", c => c.Int());
            CreateIndex("dbo.Viajes", "Usuario_Id");
            AddForeignKey("dbo.Viajes", "Usuario_Id", "dbo.Usuarios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viajes", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Viajes", new[] { "Usuario_Id" });
            AlterColumn("dbo.Viajes", "Usuario_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Viajes", "Usuario_Id");
            AddForeignKey("dbo.Viajes", "Usuario_Id", "dbo.Usuarios", "Id", cascadeDelete: true);
        }
    }
}
