using InsuranceDay1.Models;
using InsuranceProject.Repository;

namespace InsuranceProject.Services
{
    public class CustomerService:ICustomerService
    {
        private IEntityRepository<Customer> _entityRepository;

        public CustomerService(IEntityRepository<Customer> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public List<Customer> GetAll()
        {
            var customerQuery = _entityRepository.Get();
            var customers = customerQuery.Where(customer => customer.IsActive).ToList();
            return customers;
        }

        public Customer Get(int id)
        {
            var customerQuery = _entityRepository.Get();
            var customer = customerQuery.Where(customer => customer.Id == id).FirstOrDefault();
            return customer;
        }

        public Customer Check(int id)
        {
            return _entityRepository.Get(id);
        }

        public int Add(Customer customer)
        {
            return _entityRepository.Add(customer);
        }

        public Customer Update(Customer customer)
        {
            return _entityRepository.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _entityRepository.Delete(customer);
        }
    }
}
