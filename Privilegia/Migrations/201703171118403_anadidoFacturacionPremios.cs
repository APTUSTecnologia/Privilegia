namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoFacturacionPremios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacturacionPremiosModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NifProveed = c.String(nullable: false, maxLength: 128),
                        CodigoPcto = c.String(nullable: false, maxLength: 128),
                        CodigoCliente = c.String(nullable: false, maxLength: 128),
                        FechaPago = c.String(nullable: false, maxLength: 128),
                        NumeroPedido = c.String(),
                        Descripcion = c.String(),
                        FechaContratacion = c.String(),
                        FechaBaja = c.String(),
                        FechaDeAnulacion = c.String(),
                        Situacion = c.String(),
                        Observaciones = c.String(),
                        ImportePago = c.String(),
                        SituacionPago = c.String(),
                        ComisionPago = c.String(),
                        NumeroMutualista = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.NifProveed, t.CodigoPcto, t.CodigoCliente, t.FechaPago });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FacturacionPremiosModels");
        }
    }
}
