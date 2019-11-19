using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
using ChinookSystem.Data.POCOs;

namespace ChinookSystem.BLL
{
    public class CustomerController
    {
        public Customer Customer_Get(int customerid)
        {
            using (var context = new ChinookContext())
            {
                return context.Customers.Find(customerid);
            }
        }


    }
}
