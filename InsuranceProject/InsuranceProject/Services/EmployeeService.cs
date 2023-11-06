using InsuranceDay1.Models;
using InsuranceProject.Repository;

namespace InsuranceProject.Services
{
    public class EmployeeService:IEmployeeService
    {
        private IEntityRepository<Employee> _entityRepository;

        public EmployeeService(IEntityRepository<Employee> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public List<Employee> GetAll()
        {
            var empolyeeQuery = _entityRepository.Get();
            var employees = empolyeeQuery.Where(employee => employee.IsActive).ToList();
            return employees;
        }

        public Employee Get(int id)
        {
            var employeeQuery = _entityRepository.Get();
            var employee = employeeQuery.Where(employee => employee.Id == id).FirstOrDefault();
            return employee;
        }

        public Employee Check(int id)
        {
            return _entityRepository.Get(id);
        }

        public int Add(Employee employee)
        {
            return _entityRepository.Add(employee);
        }

        public Employee Update(Employee employee)
        {
            return _entityRepository.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _entityRepository.Delete(employee);
        }
    }
}
