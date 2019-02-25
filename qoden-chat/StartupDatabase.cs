using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using qoden_chat.src.Database;

namespace qoden_chat
{
    public partial class Startup
    {
        public void ConfigureDatabase(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
            Debug.WriteLine(Configuration["ConnectionString"]);
            services.AddDbContext<DatabaseContext>((provider, options) =>
            {
                options.UseNpgsql(Configuration["Database:ConnectionString"]);
            });
        }
    }
}