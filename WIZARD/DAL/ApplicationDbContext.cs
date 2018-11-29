using System;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        internal DbSet<ApiKey> ApiKeys { get; set; }
    }

    internal class ApiKey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}
