using Microsoft.Extensions.Configuration;

namespace Orange_RestWebAPI.Connections
{
    public class AppHelper
    {
        private IConfiguration _config;
        public static string ConfigPath;

        public AppHelper (IConfiguration config)
        {
            _config = config;
            ConfigPath = _config["Configuratios:LogFilePath"];
        }
      
        public static string AppPath = ConfigPath;

        public static string CommonPath = @"\common";

    }
}
