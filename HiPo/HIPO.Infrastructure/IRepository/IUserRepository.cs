namespace HIPO.Infrastructure;

public interface IUserRepository
{
    Task<HipoUsers> GetUserByAceId(string aceId);
    Task<List<HipoUsers>> GetAllUsers();
}
