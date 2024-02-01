using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using WhatsGoodApi.Models;
using WhatsGoodApi.Repository.IRepository;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private WhatsGoodDbContext _db;
        private readonly IConnectionMultiplexer _redis;
        public UserRepository(WhatsGoodDbContext db, IConnectionMultiplexer redis) : base(db)
        {
            _db = db;
            _redis = redis;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await this._db.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await this._db.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IQueryable<User>> GetUsersByUsername(string username, string ownerUsername)
        {
            var users = this._db.Users.Where(x => x.Username.Contains(username));

            users = users.Where(u => u.Username != ownerUsername);

            return users;
        }

        public async Task<User> UpdateUser(User user)
        {
            var redis = _redis.GetDatabase();
            string hashKey = $"user:{user.ID}";
            await redis.KeyDeleteAsync(hashKey);

            _db.Users.Update(user);
            await _db.SaveChangesAsync();


            var userProperties = new HashEntry[]
            {
                new HashEntry("id", user.ID),
                new HashEntry("username", user.Username),
                new HashEntry("name", user.Name),
                new HashEntry("lastName", user.LastName),
                new HashEntry("email", user.Email)
            };

            await redis.HashSetAsync(hashKey, userProperties);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {

            var redis = _redis.GetDatabase();
            string hashKey = $"user:{id}";

            var redisValue = await redis.HashGetAllAsync(hashKey);
            if (redisValue.Length > 0)
            {
                var userFromRedis = new User
                {
                    ID = (int)redisValue.FirstOrDefault(x => x.Name == "id").Value,
                    Username = redisValue.FirstOrDefault(x => x.Name == "username").Value,
                    Name = redisValue.FirstOrDefault(x => x.Name == "name").Value,
                    LastName = redisValue.FirstOrDefault(x => x.Name == "lastName").Value,
                    Email = redisValue.FirstOrDefault(x => x.Name == "email").Value
                };

                return userFromRedis;
            }

            var userFromDb = await this._db.Users.Where(x => x.ID == id).FirstOrDefaultAsync();

            if (userFromDb != null)
            {
                var userProperties = new HashEntry[]
                {
                    new HashEntry("id", userFromDb.ID),
                    new HashEntry("username", userFromDb.Username),
                    new HashEntry("name", userFromDb.Name),
                    new HashEntry("lastName", userFromDb.LastName),
                    new HashEntry("email", userFromDb.Email)
                };

                await redis.HashSetAsync(hashKey, userProperties);
            }

            return userFromDb;
        }

        public async Task<User> Create(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var redis = _redis.GetDatabase();
            string hashKey = $"user:{user.ID}";

            var userProperties = new HashEntry[]
            {
                new HashEntry("id", user.ID),
                new HashEntry("username", user.Username),
                new HashEntry("name", user.Name),
                new HashEntry("lastName", user.LastName),
                new HashEntry("email", user.Email)
            };

            await redis.HashSetAsync(hashKey, userProperties);

            return user;
        }
    }
}
