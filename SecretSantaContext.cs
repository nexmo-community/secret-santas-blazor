using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta
{
    public class SecretSantaContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<SecretSantaParticipant> Participants { get; set; }

        public SecretSantaContext(IConfiguration config) => _config = config;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_config["CONNECTION_STRING"]);
    }


}
