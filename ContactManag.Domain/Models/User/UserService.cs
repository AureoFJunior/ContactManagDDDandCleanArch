using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Interfaces.Services;

namespace ContactManag.Domain.Models
{
    public class UserService : IUserService<User>
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user)
        {
            var existentUser = _userRepository.GetById(user.Id);
            User includedUser = new User();

            if (existentUser == null)
            {
                includedUser = await _userRepository.CreateAsync(user);
                return includedUser;
            }
            throw new ConflictDatabaseException();
        }

        public User Update(User user)
        {
            User existentUser = _userRepository.GetById(user.Id);
            User updatedUser = new User();

            if (existentUser != null)
            {
                updatedUser = _userRepository.Update(user);
                return updatedUser;
            }

            throw new NotFoundDatabaseException();
        }

        public User Delete(int id)
        {

            User user = _userRepository.GetById(id);
            User deletedUser = new User();

            if (user != null)
            {
                deletedUser = _userRepository.Delete(user);
                return deletedUser;
            }
            throw new NotFoundDatabaseException();
        }
    }
}