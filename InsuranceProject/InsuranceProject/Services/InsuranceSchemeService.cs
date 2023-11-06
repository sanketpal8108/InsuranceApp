
using InsuranceDay1.Models;
using InsuranceProject.Repository;

namespace InsuranceProject.Service
{
    public class InsuranceSchemeService:IInsuranceSchemeService
    {
        private IEntityRepository<InsuranceScheme> _entityRepository;
        public InsuranceSchemeService(IEntityRepository<InsuranceScheme> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public List<InsuranceScheme> GetAll()
        {
            var insuranceSchemeQuery = _entityRepository.Get();
            return insuranceSchemeQuery.Where(insuranceScheme => insuranceScheme.IsActive).ToList();
        }
        public InsuranceScheme Get(int id)
        {
            var insuranceSchemeQuery = _entityRepository.Get();
            return insuranceSchemeQuery.Where(insuranceScheme => insuranceScheme.IsActive && insuranceScheme.Id == id).FirstOrDefault();
        }
        public int Add(InsuranceScheme insuranceScheme)
        {
            return _entityRepository.Add(insuranceScheme);
        }
        public InsuranceScheme Check(int id)
        {
            return _entityRepository.Get(id);
        }
        public InsuranceScheme Update(InsuranceScheme insuranceScheme)
        {
            return _entityRepository.Update(insuranceScheme);
        }
        public void Delete(InsuranceScheme insuranceScheme)
        {
            _entityRepository.Delete(insuranceScheme);
        }
    }
}
