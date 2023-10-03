using System;
using System.Collections.Generic;

namespace BookingManagementApp.Contracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetByGuid(Guid guid);
        T? Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }



}
