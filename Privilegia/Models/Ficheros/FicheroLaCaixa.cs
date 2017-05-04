using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using FileHelpers;

namespace Privilegia.Models.Ficheros
{

    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Carga
    {

        private CargaPedido[] pedidosField;

        private ushort identificadorField;

        private string origenField;

        private string destinatarioField;

        private string fechaGeneracionField;

        private uint numSequenciaField;

        private byte numRegsFileField;

        private ushort numRegsProcesField;

        private ushort numRegsOkField;

        private byte numRegsSkipedField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Pedido", IsNullable = false)]
        public CargaPedido[] Pedidos
        {
            get
            {
                return this.pedidosField;
            }
            set
            {
                this.pedidosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Identificador
        {
            get
            {
                return this.identificadorField;
            }
            set
            {
                this.identificadorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Origen
        {
            get
            {
                return this.origenField;
            }
            set
            {
                this.origenField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Destinatario
        {
            get
            {
                return this.destinatarioField;
            }
            set
            {
                this.destinatarioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FechaGeneracion
        {
            get
            {
                return this.fechaGeneracionField;
            }
            set
            {
                this.fechaGeneracionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint NumSequencia
        {
            get
            {
                return this.numSequenciaField;
            }
            set
            {
                this.numSequenciaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumRegsFile
        {
            get
            {
                return this.numRegsFileField;
            }
            set
            {
                this.numRegsFileField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort NumRegsProces
        {
            get
            {
                return this.numRegsProcesField;
            }
            set
            {
                this.numRegsProcesField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort NumRegsOk
        {
            get
            {
                return this.numRegsOkField;
            }
            set
            {
                this.numRegsOkField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumRegsSkiped
        {
            get
            {
                return this.numRegsSkipedField;
            }
            set
            {
                this.numRegsSkipedField = value;
            }
        }
    }

    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CargaPedido
    {

        private string aliasProveedorField;

        private string codigoProductoField;

        private CargaPedidoCliente clienteField;

        private string descripcionField;

        private string numPedidoField;

        private byte valorField;

        private byte situacionField;

        private string fechaContratacionField;

        private string observacionesField;

        private CargaPedidoPagos pagosField;

        /// <comentarios/>
        public string AliasProveedor
        {
            get
            {
                return this.aliasProveedorField;
            }
            set
            {
                this.aliasProveedorField = value;
            }
        }

        /// <comentarios/>
        public string CodigoProducto
        {
            get
            {
                return this.codigoProductoField;
            }
            set
            {
                this.codigoProductoField = value;
            }
        }

        /// <comentarios/>
        public CargaPedidoCliente Cliente
        {
            get
            {
                return this.clienteField;
            }
            set
            {
                this.clienteField = value;
            }
        }

        /// <comentarios/>
        public string Descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <comentarios/>
        public string NumPedido
        {
            get
            {
                return this.numPedidoField;
            }
            set
            {
                this.numPedidoField = value;
            }
        }

        /// <comentarios/>
        public byte Valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }

        /// <comentarios/>
        public byte Situacion
        {
            get
            {
                return this.situacionField;
            }
            set
            {
                this.situacionField = value;
            }
        }

        /// <comentarios/>
        public string FechaContratacion
        {
            get
            {
                return this.fechaContratacionField;
            }
            set
            {
                this.fechaContratacionField = value;
            }
        }

        /// <comentarios/>
        public string Observaciones
        {
            get
            {
                return this.observacionesField;
            }
            set
            {
                this.observacionesField = value;
            }
        }

        /// <comentarios/>
        public CargaPedidoPagos Pagos
        {
            get
            {
                return this.pagosField;
            }
            set
            {
                this.pagosField = value;
            }
        }
    }

    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CargaPedidoCliente
    {

        private byte entidadField;

        private string nifField;

        /// <comentarios/>
        public byte Entidad
        {
            get
            {
                return this.entidadField;
            }
            set
            {
                this.entidadField = value;
            }
        }

        /// <comentarios/>
        public string Nif
        {
            get
            {
                return this.nifField;
            }
            set
            {
                this.nifField = value;
            }
        }
    }

    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CargaPedidoPagos
    {

        private CargaPedidoPagosPago pagoField;

        /// <comentarios/>
        public CargaPedidoPagosPago Pago
        {
            get
            {
                return this.pagoField;
            }
            set
            {
                this.pagoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CargaPedidoPagosPago
    {

        private string importeField;

        private byte situacionField;

        private object fechaPagoField;

        /// <comentarios/>
        public string Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }

        /// <comentarios/>
        public byte Situacion
        {
            get
            {
                return this.situacionField;
            }
            set
            {
                this.situacionField = value;
            }
        }

        /// <comentarios/>
        public object FechaPago
        {
            get
            {
                return this.fechaPagoField;
            }
            set
            {
                this.fechaPagoField = value;
            }
        }
    }





}