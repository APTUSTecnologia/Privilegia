using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Privilegia.Models;
using Privilegia.Models.Partner;

namespace PartnersTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PartnerRepository repoPartner = new PartnerRepository();

            var orders = repoPartner.ObtenerTodos(new List<Expression<Func<PartnerModel, object>>>()
            {
                model => model.Nombre
            });
            //
            // creo un partner Externo
            //
            
            Console.Write("hola");

        }

        
    }
}
