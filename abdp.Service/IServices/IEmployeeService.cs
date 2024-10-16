using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service
{
    public interface IEmployeeService
    {
        string GetEmployeeName(int id);
        List<string> GetAllEmployee();
    }
}
