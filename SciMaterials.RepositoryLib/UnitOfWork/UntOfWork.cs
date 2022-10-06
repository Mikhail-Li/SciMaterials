﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using SciMaterials.DAL.Models;
using SciMaterials.DAL.Repositories.CategorysRepositories;
using SciMaterials.DAL.Repositories.CommentsRepositories;
using SciMaterials.DAL.Repositories.ContentTypesRepositories;
using SciMaterials.DAL.Repositories.FilesRepositories;
using SciMaterials.DAL.UnitOfWork;
using SciMaterials.Data.Repositories;
using SciMaterials.Data.Repositories.UserRepositories;
using System;
using File = SciMaterials.DAL.Models.File;

namespace SciMaterials.Data.UnitOfWork;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly ILogger _logger;
    private readonly TContext _context;

    private bool disposed;
    private Dictionary<Type, object>? _repositories = new Dictionary<Type, object>();

    /// <summary> ctor. </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <exception cref="ArgumentException"></exception>
    public UnitOfWork(
        ILogger logger,
        TContext context)
    {
        _logger = logger;
        _logger.Debug($"Логгер встроен в {nameof(UnitOfWork)}.");

        _context = context ?? throw new ArgumentException(nameof(context));
    }

    ///
    /// <inheritdoc cref="IUnitOfWork{T}.GetRepository{TEntity}"/>
    public IRepository<T> GetRepository<T>() where T : class
    {
        _logger.Debug($"{nameof(UnitOfWork)} >>> {nameof(GetRepository)}.");

        if (_repositories == null)
            _repositories = new Dictionary<Type, object>();

        var type = typeof(T);

        return (IRepository<T>)_repositories[type];
    }

    ///
    /// <inheritdoc cref="IUnitOfWork{T}.SaveContext()"/>
    public int SaveContext()
    {
        _logger.Info($"{nameof(UnitOfWork)} >>> {nameof(SaveContext)}.");
        try
        {
            return _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.Error($"{nameof(UnitOfWork)} >>> {nameof(SaveContext)}. Ошибка при попытке сохранений изменений контекста. >>> {ex.Message}");
            return 0;
        }
    }

    ///
    /// <inheritdoc cref="IUnitOfWork{T}.SaveContextAsync()"/>
    public Task<int> SaveContextAsync()
    {
        throw new NotImplementedException();
    }

    ///
    /// <inheritdoc cref="IUnitOfWork{T}.BeginTransaction(bool)"/>
    public IDbContextTransaction BeginTransaction(bool useIfExists = false)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction == null)
        {
            return _context.Database.BeginTransaction();
        }

        return useIfExists ? transaction : _context.Database.BeginTransaction();
    }

    ///
    /// <inheritdoc cref="IUnitOfWork{T}.BeginTransactionAsync(bool)"/>
    public Task<IDbContextTransaction> BeginTransactionAsync(bool useIfExists = false)
    {
        throw new NotImplementedException();
    }

    private void Initialise()
    {
        _repositories!.Add(typeof(User), new UserRepository(_context, _logger));
        _repositories!.Add(typeof(File), new FileRepository(_context, _logger));
        _repositories!.Add(typeof(Category), new CategoryRepository(_context, _logger));
        _repositories!.Add(typeof(Comment), new CommentRepository(_context, _logger));
        _repositories!.Add(typeof(ContentType), new ContentTypeRepository(_context, _logger));
        _repositories!.Add(typeof(FileGroup), new ContentTypeRepository(_context, _logger));
        _repositories!.Add(typeof(Rating), new ContentTypeRepository(_context, _logger));
        _repositories!.Add(typeof(Tag), new ContentTypeRepository(_context, _logger));
    }

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            disposed = true;
        }
    }



    #endregion
}