using TestAAI.Models;

namespace TestAAI.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserById(int id);
    }
}
