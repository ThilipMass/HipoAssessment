using HIPO.Infrastructure;

namespace HIPO.Core;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<HipoUsers> GetUserByAceId(string aceId)
    {
        return await _unitOfWork.UserRepository.GetUserByAceId(aceId);
    }

    public async Task<List<HipoUsers>> GetAllUsers()
    {
        return await _unitOfWork.UserRepository.GetAllUsers();
    }
}
