namespace HIPO.Infrastructure;

public interface ILoginRepository
{
    Task<HipoUsers> GetUserByAceId(string username);
}
