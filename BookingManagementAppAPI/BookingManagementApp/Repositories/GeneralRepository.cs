﻿using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Utilities.Handlers;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using BookingManagementApp.Data;

namespace BookingManagementApp.Repositories
{
    public class GeneralRepository<TEntity> : IRepository<TEntity> where
    TEntity : class
    {
        protected readonly BookingManagementDbContext _context;

        protected GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public TEntity? Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
