using System;
using DbUp;
using MicroFx.Logging;

namespace MicroFx.Data.Migration
{
    public class DbMigrator : IStartupTask
    {
        private readonly IDbConnectionProvider dbConnectionProvider;
        private static readonly ILogger logger = LogManager.GetLogger(typeof(DbMigrator));

        private readonly IScriptDirectoryProvider scriptDirectoryProvider;

        public DbMigrator(IDbConnectionProvider dbConnectionProvider
                        , IScriptDirectoryProvider scriptDirectoryProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
            this.scriptDirectoryProvider = scriptDirectoryProvider;
        }

        public void Start()
        {
            logger.Info("Db migration started..");

            var upgrader = DeployChanges.To
                    .SqlDatabase(dbConnectionProvider.GetConnectionString())
                    .WithTransaction()
                    .WithScripts(new ScriptProvider(scriptDirectoryProvider, "schema"))
                    .WithScripts(new ScriptProvider(scriptDirectoryProvider, "data"))
                    //.JournalTo(new CustomJournal())
                    .LogTo(new LogProvider())
                    .Build();
            try
            {
                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    logger.Error(result.Error);
                    Environment.ExitCode = -1;
                    return;
                }

                logger.Info("Db Update Successful!");
                Environment.ExitCode = 0;
            }
            catch (Exception e)
            {
                logger.Error(e);
                Environment.ExitCode = -1;
            }
        }

        public void Stop()
        {
            
        }
    }
}
