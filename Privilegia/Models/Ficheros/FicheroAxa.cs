using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace Privilegia.Models.Ficheros
{
    public class FicheroAxa
    {
        [FieldFixedLength(2)]
        public System.String DEVOLUCI;

        [FieldFixedLength(14)]
        public System.String COBRADOR;

        [FieldFixedLength(11)]
        public System.String IDENTCTR;

        [FieldFixedLength(6)]
        public System.String FECHAPROCESO;

        [FieldFixedLength(10)]
        public System.String PRIMANET;

        [FieldFixedLength(2)]
        public System.String TIPRECIB;

        [FieldFixedLength(1)]
        public System.String SITUACIO;

        [FieldFixedLength(8)]
        public System.String FECHASIT;

        [FieldFixedLength(8)]
        public System.String FECHAEMI;

        [FieldFixedLength(10)]
        public System.String PRIMATOT;

        [FieldFixedLength(38)]
        public System.String CONSORCI;

        [FieldFixedLength(1)]
        public System.String MODOPAGO;

        [FieldFixedLength(45)]
        public System.String TOTLIQUI;

        [FieldFixedLength(15)]
        public System.String VENCOASE;

        [FieldFixedLength(15)]
        public System.String NUMEBORD;

        [FieldFixedLength(43)]
        public System.String PROCEDEN;

        [FieldFixedLength(10)]
        public System.String RECAREXT;

        [FieldFixedLength(10)]
        public System.String DGSEGURO;

        [FieldFixedLength(10)]
        public System.String ARBITBOM;

        [FieldFixedLength(10)]
        public System.String RESTOIMP;

        [FieldFixedLength(10)]
        public System.String BONIFICI;

        [FieldFixedLength(10)]
        public System.String RECARGOS;

        [FieldFixedLength(28)]
        public System.String INDESGLO;

        [FieldFixedLength(295)]
        public System.String PRDESGLO;

        [FieldFixedLength(10)]
        public System.String IRPFTOTA;

        [FieldFixedLength(10)]
        public System.String COMBONIF;

        [FieldFixedLength(15)]
        public System.String IRPBONIF;

        [FieldFixedLength(10)]
        public System.String TOTCOMIS;

        [FieldFixedLength(8)]
        public System.String FECHAEFE;

        [FieldFixedLength(15)]
        public System.String NUMPOLIZ;

        [FieldFixedLength(16)]
        public System.String FECHAFIN;

        [FieldFixedLength(23)]
        public System.String NIFTOMAD;

        [FieldFixedLength(7)]
        public System.String CODAGENT;

    }
}