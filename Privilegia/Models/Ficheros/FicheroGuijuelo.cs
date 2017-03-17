using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FileHelpers;

namespace Privilegia.Models
{
    [DelimitedRecord(";")]
    [IgnoreFirst]
    public class FicheroGuijuelo
    {

        public string NIF_PROV;

        public string NIF_CLIENTE;

        public string PRODUCTO;

        [FieldTrim(TrimMode.Both)]
        public string NOMBRE;

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime FCONTRATAC;

        public int N_MUTUAL;
    
        public int N_PEDIDO;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal IMPORTE_PEDIDO;

        public byte SITUACIONPEDIDO;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal COMISION;

        public int SITUACIONPAGO;


    }
}