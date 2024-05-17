using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IActualSalaryService
    {
        bool CreateActualSalary(ActualSalary actualSalary);
        ActualSalary GetActualSalaryByUserId(int userId);
    }
}
