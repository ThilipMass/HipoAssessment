using HIPO.Infrastructure;

namespace HIPO.Core;

public interface IUserService
{
    Task<HipoUsers> GetUserByAceId(string aceId);
    Task<List<HipoUsers>> GetAllUsers();
}
