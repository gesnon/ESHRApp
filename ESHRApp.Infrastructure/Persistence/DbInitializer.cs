using Microsoft.EntityFrameworkCore;

namespace ESHRApp.Infrastructure.Persistence
{
    public class DbInitializer
    {
        private readonly ESHRAppContext _context;

        public DbInitializer(ESHRAppContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
           await _context.Database.MigrateAsync();
        }
    }
}
