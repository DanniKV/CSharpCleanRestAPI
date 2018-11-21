using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CustomerApp.Infrastructure.Data;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CustomerAppContext _ctx;

        public UserRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }

        public User ReadUser(string username)
        {
            return _ctx.Users.FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));
        }
    }
}