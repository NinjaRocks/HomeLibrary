using System.Collections.Generic;
using System.IO;
using DbUp.Engine;
using DbUp.Engine.Transactions;

namespace MicroFx.Data.Migration
{
    public class ScriptProvider : IScriptProvider
    {
        private readonly string folder;
        private readonly string directory;

        public ScriptProvider(IScriptDirectoryProvider scriptDirectoryProvider, string folder)
        {
            this.folder = folder;
            this.directory = scriptDirectoryProvider.GetDirectoryPath();
        }
        public ScriptProvider(string directory, string folder)
        {
            this.folder = folder;
            this.directory = directory;
        }
        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            var scripts = new List<SqlScript>();

            var path = Path.Combine(directory, folder);

            var files = Directory.GetFiles(path, "*.Sql");

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Exists)
                    scripts.Add(new SqlScript(fileInfo.Name, File.ReadAllText(file)));
            }

            return scripts;
        }
    }
}