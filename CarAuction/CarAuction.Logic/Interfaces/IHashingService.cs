namespace CarAuction.Logic.Interfaces
{
    public interface IHashingService : IService
    {
        (string hashed, byte[] salt) EncryptPassword(string password);
        bool VerifyPassword(string enteredPassword, byte[] salt, string storedPassword);
    }
}
