
using Microsoft.EntityFrameworkCore;

namespace HIPO.Infrastructure;

public class LoginRepository : ILoginRepository
{
    private readonly HipoDbContext _context;
    public LoginRepository(HipoDbContext context)
    {
        _context = context;
    }
    public async Task<HipoUsers> GetUserByAceId(string aceid)
    {
        HipoUsers? user = await _context.HipoUser.FirstOrDefaultAsync(u => u.ACEId == aceid);
        if (user == null)
        {
            throw new Exception(Constants.UserNotFound);
        }
        return user;
    }
}
