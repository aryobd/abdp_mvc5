using abdp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        public List<string> GetAllEmployee()
        {
            //throw new NotImplementedException();

            List<string> lstEmployeeName = new List<string>();
            lstEmployeeName.Add("abdp 1");
            lstEmployeeName.Add("abdp 2");
            lstEmployeeName.Add("abdp 3");
            lstEmployeeName.Add("abdp 4");
            lstEmployeeName.Add("abdp 5");
            lstEmployeeName.Add("abdp 6");
            lstEmployeeName.Add("abdp 7");

            return lstEmployeeName;
        }

        public string GetEmployeeName(int id)
        {
            //throw new NotImplementedException();

            return "abdp " + id;
        }
    }
}
