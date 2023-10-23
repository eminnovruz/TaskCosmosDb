namespace TaskApiCosmos.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateSecurityToken(string id, string email);
    }
}
