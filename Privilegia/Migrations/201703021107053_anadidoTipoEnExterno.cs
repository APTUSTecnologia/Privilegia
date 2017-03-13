namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoTipoEnExterno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartnerModels", "Tipo_Id", c => c.Guid());
            CreateIndex("dbo.PartnerModels", "Tipo_Id");
            AddForeignKey("dbo.PartnerModels", "Tipo_Id", "dbo.TipoModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartnerModels", "Tipo_Id", "dbo.TipoModels");
            DropIndex("dbo.PartnerModels", new[] { "Tipo_Id" });
            DropColumn("dbo.PartnerModels", "Tipo_Id");
        }
    }
}
