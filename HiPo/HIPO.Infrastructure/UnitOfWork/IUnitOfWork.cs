namespace HIPO.Infrastructure;

public interface IUnitOfWork
{
    ILoginRepository LoginRepository { get; }
    IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync();
}
