using System;
using DbUp;
using MicroFx.Logging;

namespace MicroFx.Data.Migration
{
    public class DbMigrator : IStartupTask
    {
        private static readonly ILogger logger = LogManager.GetLogger(typeof(DbMigrator));

        private readonly string dbConnectionString;
        private readonly ISchemaProvider schemaProvider;
        private readonly IDataScriptProvider dataScriptProvider;

        public DbMigrator(string dbConnectionString)
            :this(dbConnectionString, new ScriptProvider("Schema"), new ScriptProvider("Data"))
        {}

        public DbMigrator(string dbConnectionString, ISchemaProvider schemaProvider, IDataScriptProvider dataScriptProvider)
        {
            this.dbConnectionString = dbConnectionString;
            this.schemaProvider = schemaProvider;
            this.dataScriptProvider = dataScriptProvider;
        }

        public void Start()
        {
            logger.Info("Db migration started..");

            //var path = AppDomain.CurrentDomain.BaseDirectory + @"\DbScripts";

            var upgrader = DeployChanges.To
                    .SqlDatabase(dbConnectionString)
                    .WithTransaction()
                    .WithScripts(schemaProvider)
                    .WithScripts(dataScriptProvider)
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
