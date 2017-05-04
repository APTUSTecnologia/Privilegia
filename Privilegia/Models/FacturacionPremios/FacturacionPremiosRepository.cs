using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;
using Privilegia.Models.FacturacionPartners;
using Privilegia.Models.FacturasPremios;
using Privilegia.Models.Ficheros;
using Privilegia.Models.Incidencias;

namespace Privilegia.Models.FacturacionPartners
{
    public class FacturacionPremiosRepository : BaseRepository<FacturacionPremiosModel> , IFacturacionPremiosRepository
    {
        public List<FacturacionPremiosModel> ObtenerFacturacionPremios()
        {
            using (Contexto context = new Contexto())
            {
                var lista = context.FacturacionPremios.OfType<FacturacionPremiosModel>().ToList();

                return lista;
            }
        }

        public List<FacturacionPremiosModel> ObtenerFacturasPremiosPorNifPartner(string nifPartner)
        {
            using (Contexto context = new Contexto())
            {
                var lista = context.FacturacionPremios.OfType<FacturacionPremiosModel>().Where(d => d.NifProveed == nifPartner).ToList();
                
                return lista;
            }
        }

        public List<FacturacionPremiosModel> ObtenerFacturaPremiosPorIdFactura(string idFactura)
        {
            using (Contexto context = new Contexto())
            {
                var lista = context.FacturacionPremios.OfType<FacturacionPremiosModel>().Where(d => d.IdFactura == idFactura).ToList();

                return lista;
            }
        }

        
        public List<FacturacionPremiosModel> ObtenerFacturasPremiosPorMutualista()
        {
            using (Contexto context = new Contexto())
            {
                var lista = context.FacturacionPremios.OfType<FacturacionPremiosModel>().ToList();

                return lista;
            }
        }

        public List<FacturacionPremiosModel> ObtenerFacturacionPorMes(string mes)
        {
            throw new NotImplementedException();
        }


        public List<FacturacionPremiosModel> MapearLaCaixa(Carga ficheroLaCaixa)
        {
            if (ficheroLaCaixa != null && ficheroLaCaixa.NumRegsProces > 0)
            {
                var lista = new List<FacturacionPremiosModel>();

                foreach (var pedido in ficheroLaCaixa.Pedidos)
                {
                    
                    if (pedido.Pagos != null && pedido.Pagos.Pago != null)
                    {
                        var importe = Double.Parse(pedido.Pagos.Pago.Importe);
                        lista.Add(new FacturacionPremiosModel()
                        {
                            Id = Guid.NewGuid(),
                            ImportePago = pedido.Pagos.Pago.Importe,
                            CodigoPcto = pedido.CodigoProducto,
                            CodigoCliente = pedido.Cliente.Nif,
                            ComisionPago = pedido.Pagos.Pago.Importe,
                            Descripcion = pedido.Descripcion,
                            FechaContratacion = pedido.FechaContratacion,
                            Observaciones = pedido.Observaciones,
                            FechaPago = DateTime.Today.ToShortDateString(),
                            Temp = 1,
                            Situacion = pedido.Situacion.ToString(),
                            SituacionPago = pedido.Pagos.Pago.Situacion.ToString(),
                            NumeroPedido = pedido.NumPedido,
                            Valor = pedido.Valor.ToString(),
                            FechaDeCreacion = DateTime.Today.ToShortDateString()
                        });
                          

                    }
                }
                return lista;
            }
            return null;
        }

        public List<FacturacionPremiosModel> MapearErrores(List<ErrorInfo> errores)
        {
            if (errores != null && errores.Count > 0)
            {
                var lista = new List<FacturacionPremiosModel>();

                foreach (var error in errores)
                {
                    var linea = error.RecordString.Split(';');

                    var registroFacturacion = new FacturacionPremiosModel()
                    {
                        Id = Guid.NewGuid(),
                        NifProveed = linea[0],
                        CodigoPcto = linea[1],
                        CodigoCliente = linea[2],
                        NumeroPedido = linea[3],
                        Descripcion = linea[4],
                        Valor = linea[10],
                        FechaContratacion = linea[5],
                        Observaciones = linea[6],
                        NumeroMutualista = linea[7],
                        Situacion = linea[8],
                        FechaPago = linea[9],
                        ImportePago = linea[10],
                        ComisionPago = linea[11],
                        SituacionPago = linea[12],
                        FechaDeCreacion = DateTime.Today.ToShortDateString(),
                        Temp = 1
                        
                    };

                    if (!ExisteRegistro(registroFacturacion))
                    {
                        //Insertar(registroFacturacion);
                        lista.Add(registroFacturacion);
                    }
                }
                return lista;
            }
            return null;
        }

        public List<FacturacionPremiosModel> MapearStandar(List<FicheroYoRespondo> ficheroYoRespondo)
        {
            if (ficheroYoRespondo != null)
            {

                List<FacturacionPremiosModel> lista = new List<FacturacionPremiosModel>();
                foreach (var elemento in ficheroYoRespondo)
                {
                    var ficheroFacturacion = new FacturacionPremiosModel()
                    {
                        Id = Guid.NewGuid(),
                        NifProveed = elemento.NIF_PROVEED,
                        CodigoPcto = elemento.COD_PCTO,
                        CodigoCliente = elemento.COD_CLIENTE,
                        NumeroPedido = elemento.Nº_PEDIDO.ToString(),
                        FechaContratacion = elemento.FECHA_CONTRATACION.ToShortDateString(),
                        Situacion = elemento.SITUACION.ToString(),
                        ImportePago = elemento.IMPORTE_PAGO.ToString("##.##"),
                        Descripcion = elemento.DESCRIPCION,
                        ComisionPago = elemento.VALOR.ToString("##.##"),
                        SituacionPago = elemento.SITUACION_PAGO.ToString(),
                        NumeroMutualista = elemento.Nº_MUTUALISTA.ToString(),
                        FechaPago = elemento.FECHA_PAGO.ToShortDateString(),
                        FechaDeCreacion = DateTime.Today.ToShortDateString(),
                        Valor = elemento.VALOR.ToString("##.##"),
                        Temp = 1
                    };

                    if (!ExisteRegistro(ficheroFacturacion))
                    {
                        Insertar(ficheroFacturacion);
                        lista.Add(ficheroFacturacion);
                    }
                    //Si el registro existiera se podria actualizar aqui 

                }
                return lista;
            }
            return new List<FacturacionPremiosModel>();
        }

        public bool ExisteRegistro(FacturacionPremiosModel registro)
        {
            if (registro != null)
            {
                var registros = ObtenerFacturasPremiosPorNifPartner(registro.NifProveed);

                if (registros.Any(m => m.NifProveed == registro.NifProveed 
                                    && m.CodigoPcto == registro.CodigoPcto 
                                    && m.CodigoCliente == registro.CodigoCliente 
                                    && m.NumeroPedido == registro.NumeroPedido
                                    && m.NumeroMutualista == registro.NumeroMutualista
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