using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.FacturacionPartners
{
    public class FacturacionPremiosRepository : BaseRepository<FacturacionPremiosModel> , IFacturacionPremiosRepository
    {
        public List<FacturacionPremiosModel> ObtenerFacturacionPremios()
        {
            throw new NotImplementedException();
        }

        public List<FacturacionPremiosModel> ObtenerFacturasPremiosPorNifPartner(string nifPartner)
        {
            using (Contexto context = new Contexto())
            {
                var lista = context.FacturacionPremios.OfType<FacturacionPremiosModel>().Where(d => d.NifProveed == nifPartner).ToList();
                
                return lista;
            }
        }

        public FacturacionPremiosModel ObtenerFacturaPremiosPorIdFactura(string idFactura)
        {
            throw new NotImplementedException();
        }

        public FacturacionPremiosModel ObtenerFacturasPremiosPorMutualista(string idMutualista)
        {
            throw new NotImplementedException();
        }

        public List<FacturacionPremiosModel> ObtenerFacturacionPorMes(string mes)
        {
            throw new NotImplementedException();
        }



        public bool InsertarGuijuelo(List<FicheroGuijuelo> ficheroGuijuelo)
        {
            if (ficheroGuijuelo != null)
            {
                foreach (var elemento in ficheroGuijuelo)
                {
                    var ficheroFacturacion = new FacturacionPremiosModel()
                    {
                        Id = Guid.NewGuid(),
                        NifProveed = elemento.NIF_PROV,
                        CodigoPcto = elemento.PRODUCTO,
                        CodigoCliente = elemento.NIF_CLIENTE,
                        NumeroPedido = elemento.N_PEDIDO.ToString(),
                        FechaContratacion = elemento.FCONTRATAC.ToLongDateString(),
                        Situacion = elemento.SITUACIONPEDIDO.ToString(),
                        ImportePago = elemento.IMPORTE_PEDIDO.ToString("##.##"),
                        Descripcion = elemento.NOMBRE,
                        ComisionPago = elemento.COMISION.ToString("##.##"),
                        SituacionPago = elemento.SITUACIONPAGO.ToString(),
                        NumeroMutualista = elemento.N_MUTUAL.ToString(),
                        FechaPago = DateTime.Today.ToLongDateString(),
                        FechaDeCreacion = DateTime.Today.ToLongDateString()
                    };

                    if (!ExisteRegistro(ficheroFacturacion))
                    {
                        Insertar(ficheroFacturacion);
                    }

                    
                }
                return true;
            }
            return false;
        }

        public bool ExisteRegistro(FacturacionPremiosModel registro)
        {
            if (registro != null)
            {
                var registros = ObtenerFacturasPremiosPorNifPartner(registro.NifProveed);

                if (registros.Any(m => m.NifProveed == registro.NifProveed 
                                    && m.CodigoPcto == registro.CodigoPcto 
                                    && m.CodigoCliente == registro.CodigoCliente 
                                    && m.FechaPago == registro.FechaPago))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}