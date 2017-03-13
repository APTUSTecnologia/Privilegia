using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Publicidad
{
    public class TimeTableModel
    {
        public String id { get; set; }

        public String title { get; set; }

        public String start { get; set; }

        public String end { get; set; }

        public String color { get; set; }

        public String descripcion { get; set; }

        public TimeTableModel(String I, String t, String ds, String de , String col , String des)
        {
            this.id = I;
            this.title = t;
            this.start = ds;
            this.end = de;
            this.color = col;
            this.descripcion = des;
        }
    }
}