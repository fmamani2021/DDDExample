﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using VeterinaryClinic.ManagementModule.Infrastructure.Data;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.ManagementModule.Infrastructure.Data
{
    // We are using the EfRepository from Ardalis.Specification
    // https://github.com/ardalis/Specification/blob/v5/ArdalisSpecificationEF/src/Ardalis.Specification.EF/RepositoryBaseOfT.cs
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
