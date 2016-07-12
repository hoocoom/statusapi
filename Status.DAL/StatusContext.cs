using System.Data.Common;
using System.Data.Entity;
using Status.DAL.Entities;

namespace Status.DAL
{
    public sealed class StatusContext : DbContext
    {
        public StatusContext()
        {
        }

        public StatusContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public StatusContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<StateLog> StateLogs { get; set; }
    }
}
