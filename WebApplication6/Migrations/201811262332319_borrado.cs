namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class borrado : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Viajes", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Viajes", new[] { "Usuario_Id" });
            DropColumn("dbo.Viajes", "Usuario_Id");
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Contraseña = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Viajes", "Usuario_Id", c => c.Int());
            CreateIndex("dbo.Viajes", "Usuario_Id");
            AddForeignKey("dbo.Viajes", "Usuario_Id", "dbo.Usuarios", "Id");
        }
    }
}
