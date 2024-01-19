using WhatsGoodApi.DTOs;
using WhatsGoodApi.Helpers;
using WhatsGoodApi.Models;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.Unit;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Services
{
    public class UserService : IUserService
    {
        private readonly WhatsGoodDbContext _db;
        public UnitOfWork _unitOfWork { get; set; }
        private JwtService jwtService { get; set; }

        public UserService(WhatsGoodDbContext db)
        {
            this._db = db;
            this._unitOfWork = new UnitOfWork(db);
            jwtService = new JwtService();
        }

        public async Task<User> Register(UserRegisterDTO user)
        {
            if (user != null)
            {
                var userFound = await this._unitOfWork.User.GetUserByEmail(user.Email);
                if (userFound != null)
                {
                    throw new Exception("User with this email already exists.");
                }

                userFound = await this._unitOfWork.User.GetUserByUsername(user.Username);
                if (userFound != null)
                {
                    throw new Exception("User with this username already exists.");
                }

                var userCreated = new User
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                };

                return await this._unitOfWork.User.Create(userCreated);
            }
            else
            {
                return null;
            }
        }

        public async Task<string> Login(string email, string password)
        {
            var userFound = await this._unitOfWork.User.GetUserByEmail(email);

            if (userFound == null) throw new Exception("Invalid credential");

            if (!BCrypt.Net.BCrypt.Verify(password, userFound.Password)) throw new Exception("Invalid credential");

            var jwt = jwtService.Generate(userFound.ID);

            return jwt;
        }
        public async Task UpdateProfile(UserUpdateDTO user)
        {
            if (user != null)
            {
                var userFound = await this._unitOfWork.User.GetUserById(user.Id);
                userFound.Name = user.Name;
                userFound.LastName = user.LastName;
                userFound.Username = user.Username;
                userFound.Email = user.Email;
                userFound.Password = user.Password;
                this._unitOfWork.User.Update(userFound);
                await this._unitOfWork.Save();
            }
        }
        public async Task<User> GetUser(string jwt)
        {
            var token = jwtService.Verify(jwt);

            int userId = int.Parse(token.Issuer);

            var user = await this._unitOfWork.User.GetUserById(userId);

            return user;
        }
        public async Task<IQueryable<User>> Search(string username, string ownerUsername)
        {
            if (username == null)
            {
                throw new Exception("Search is null!");
            }

            if (ownerUsername == null)
            {
                throw new Exception("Missing username who's searching!");
            }

            var users = await this._unitOfWork.User.GetUsersByUsername(username, ownerUsername);
            return users;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            if (username != null)
            {
                var user = await this._unitOfWork.User.GetUserByUsername(username);
                return user;
            }
            throw new ArgumentNullException(nameof(username), "Username cannot be null.");
        }

        public async Task<User> GetUserByUserId(int userId)
        {
            var user = await this._unitOfWork.User.GetUserById(userId);
            return user;
        }

    }
}
