using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
using ChinookSystem.Data.DTOs;

namespace ChinookSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        public List<string> Employees_GetTitles()
        {
            using (var context = new ChinookContext())
            {
                var results = (from x in context.Employees
                               select x.Title).Distinct();
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]

        public List<SelectionList> Employee_ListNames()
        {
            using (var context = new  ChinookContext())
            {
                var employeeList = from x in context.Employees
                                   orderby x.LastName, x.FirstName
                                   select new SelectionList
                                   {
                                       IDValueField = x.EmployeeId,
                                       DisplayText = x.FirstName + " " +x.LastName
                                       
                                   };
                return employeeList.ToList();
            }
        }
    }
}
