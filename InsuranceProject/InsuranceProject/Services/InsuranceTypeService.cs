
using InsuranceDay1.Models;
using InsuranceProject.Repository;

namespace InsuranceProject.Service
{
    public class InsuranceTypeService:IInsuranceTypeService
    {
        private IEntityRepository<InsuranceType> _entityRepository;
        public InsuranceTypeService(IEntityRepository<InsuranceType> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public List<InsuranceType> GetAll()
        {
            var insuranceTypeQuery = _entityRepository.Get();
            return insuranceTypeQuery.Where(insuranceType => insuranceType.IsActive).ToList();
        }
        public InsuranceType Get(int id)
        {
            var insuranceTypeQuery = _entityRepository.Get();
            return insuranceTypeQuery.Where(insuranceType => insuranceType.IsActive && insuranceType.Id == id).FirstOrDefault();
        }
        public int Add(InsuranceType insuranceType)
        {
            return _entityRepository.Add(insuranceType);
        }
        public InsuranceType Check(int id)
        {
            return _entityRepository.Get(id);
        }
        public InsuranceType Update(InsuranceType insuranceType)
        {
            return _entityRepository.Update(insuranceType);
        }
        public void Delete(InsuranceType insuranceType)
        {
            _entityRepository.Delete(insuranceType);
        }
    }
}
