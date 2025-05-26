using Microsoft.EntityFrameworkCore;

namespace HIPO.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly HipoDbContext _context;

    public UserRepository(HipoDbContext context)
    {
        _context = context;
    }

    public async Task<HipoUsers> GetUserByAceId(string aceId)
    {
        var user = await _context.HipoUser.FirstOrDefaultAsync(x => x.ACEId == aceId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }

    public async Task<List<HipoUsers>> GetAllUsers()
    {
        var users = await _context.HipoUser.ToListAsync();
        return users;
    }

}