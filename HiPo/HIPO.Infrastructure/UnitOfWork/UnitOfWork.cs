namespace HIPO.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly HipoDbContext _context;
    public ILoginRepository LoginRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(HipoDbContext context, ILoginRepository loginRepository, IUserRepository userRepository)
    {
        _context = context;
        LoginRepository = loginRepository;
        UserRepository = userRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}