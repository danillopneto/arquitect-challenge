﻿using ArquitectChallenge.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace ArquitectChallenge.Services.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
                : base(options)
        {
        }

        public DbSet<EventData> Events { get; set; }
    }
}