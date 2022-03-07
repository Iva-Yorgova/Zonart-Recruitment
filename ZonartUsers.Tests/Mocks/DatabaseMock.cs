using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZonartUsers.Data;

namespace ZonartUsers.Tests.Mocks
{
    public class DatabaseMock
    {
        public static ZonartUsersDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ZonartUsersDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ZonartUsersDbContext(dbContextOptions);
            }
        }
    }
}
