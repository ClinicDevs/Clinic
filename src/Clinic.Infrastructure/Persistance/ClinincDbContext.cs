﻿using Clinic.Application.Abstractions;
using Clinic.Domain.Entities.Auth;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Persistance
{
    public class ClinincDbContext : DbContext, IClinincDbContext
    {
        public ClinincDbContext(DbContextOptions<ClinincDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Diplom> Diploms { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Specialist> Specialists { get; set; }

        async ValueTask<int> IClinincDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}