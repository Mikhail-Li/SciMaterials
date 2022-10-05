using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SciMaterials.DAL.Contexts;
using SciMaterials.DAL.InitializationDb.Interfaces;

namespace SciMaterials.DAL.InitializationDb.Implementation
{
    public class DbInitializer : IDbInitializer
    {
        private readonly SciMaterialsContext _db;
        private readonly ILogger<SciMaterialsContext> _logger;

        public DbInitializer(SciMaterialsContext db, ILogger<SciMaterialsContext> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> DeleteDbAsync(CancellationToken cancel = default)
        {
            cancel.ThrowIfCancellationRequested();

            try
            {
                var result = await _db.Database.EnsureDeletedAsync(cancel).ConfigureAwait(false);
                return result;
            }
            catch (OperationCanceledException e)
            {
                _logger.LogError("Interrupting an operation when deleting a database: {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError("Error during database initialization: {0}", e.Message);
                throw;
            }
        }

        public async Task InitializeDbAsync(bool RemoveAtStart = false, CancellationToken cancel = default)
        {
            cancel.ThrowIfCancellationRequested();

            try
            {
                if (RemoveAtStart) await DeleteDbAsync(cancel).ConfigureAwait(false);

                var pendingMigration = await _db.Database.GetPendingMigrationsAsync(cancel).ConfigureAwait(false);

                if (pendingMigration.Any())
                {
                    await _db.Database.MigrateAsync(cancel).ConfigureAwait(false);
                }

                await InitializeDbAsync(cancel).ConfigureAwait(false);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogError("Interrupting an operation when deleting a database: {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError("Error during database initialization: {0}", e.Message);
                throw;
            }
        }

        private async Task InitializeDbAsync(CancellationToken cancel = default)
        {
            cancel.ThrowIfCancellationRequested();

            await DataSeeder.Seed(_db, cancel).ConfigureAwait(false);
        }
    }
}
