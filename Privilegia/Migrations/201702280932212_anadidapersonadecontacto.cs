namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidapersonadecontacto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonaContactoModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellidos = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        Cargo = c.String(nullable: false),
                        Principal = c.Boolean(nullable: false),
                        PartnerId = c.String(),
                        PartnerInterno_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.PartnerInterno_Id)
                .Index(t => t.PartnerInterno_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonaContactoModels", "PartnerInterno_Id", "dbo.PartnerModels");
            DropIndex("dbo.PersonaContactoModels", new[] { "PartnerInterno_Id" });
            DropTable("dbo.PersonaContactoModels");
        }
    }
}
