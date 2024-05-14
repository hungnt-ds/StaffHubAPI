using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserObj { get; }
        //IArtworkRepository ArtworkObj { get; }

        void Save();
    }
}
