using System;

namespace MicroFx.Logging
{
    public static class LogManager
    {
        public static ILogger GetLogger(string source)
        {
            return new Logger(source);
        }
        public static ILogger GetLogger(Type type)
        {
            return new Logger(type.Name);
        }
        public static ILogger GetLogger<T>()
        {
            return new Logger(typeof(T).Name);
        }
    }
}