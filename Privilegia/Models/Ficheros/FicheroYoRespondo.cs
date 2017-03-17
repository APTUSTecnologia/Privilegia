using System;
using FileHelpers;

namespace Privilegia.Models
{
    [IgnoreFirst(1)]
    [DelimitedRecord(";")]
    public class FicheroYoRespondo
    {
        public String NIF_PROVEED;

        public String COD_PCTO;

        public String COD_CLIENTE;

        public String Nº_PEDIDO;

        public String DESCRIPCION;

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime FECHA_CONTRATACION;

        public String OBSERVACIONES;

        public String Nº_MUTUALISTA;

        public String SITUACION;

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime FECHA_PAGO;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VALOR;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal IMPORTE_PAGO;

        public String SITUACION_PAGO;



    }
}