﻿using System.Threading.Tasks;
using IServices.Infrastructure;

namespace Services.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDb _dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _dataContext = databaseFactory.Get();
        }

        public int Commit()
        {
            return _dataContext.Commit();
        }

        public Task<int> CommitAsync()
        {
            return _dataContext.CommitAsync();
        }
    }
}