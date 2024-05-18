using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Services.Implementations
{
    public class ActualSalaryService : IActualSalaryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActualSalaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateActualSalary(ActualSalary actualSalary)
        {
            return _unitOfWork.ActualSalaryObj.CreateActualSalary(actualSalary);
        }

        public ActualSalary GetActualSalaryByUserId(int userId)
        {
            return _unitOfWork.ActualSalaryObj.GetActualSalary(userId);
        }
    }
}
