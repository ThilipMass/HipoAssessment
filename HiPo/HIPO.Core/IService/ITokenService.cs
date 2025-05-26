namespace HIPO.Core;

public interface ITokenService
{
    string GenerateToken(string aceid, string email);
}
