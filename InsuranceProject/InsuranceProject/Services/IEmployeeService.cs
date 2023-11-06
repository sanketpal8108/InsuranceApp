using InsuranceDay1.Models;

namespace InsuranceProject.Services
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();
        public Employee Get(int id);

        public Employee Check(int id);

        public int Add(Employee employee);

        public Employee Update(Employee employee);

        public void Delete(Employee employee);

    }
}
