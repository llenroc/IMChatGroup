using System;

namespace Base.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
