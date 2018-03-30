using System;
using System.Configuration;
using MicroFx.Data;

namespace HomeLibrary.API
{
    public class ScriptDirectoryProvider : IScriptDirectoryProvider
    {
        public string GetDirectoryPath()
        {
            return ConfigurationManager.AppSettings["ScriptRootDirectory"] ??
                   AppDomain.CurrentDomain.BaseDirectory + @"\DbScripts";
        }
    }
}