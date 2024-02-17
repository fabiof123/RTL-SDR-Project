using RtlSdrServer.Debugging;

namespace RtlSdrServer
{
    public class RtlSdrServerConsts
    {
        public const string LocalizationSourceName = "RtlSdrServer";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "c179ffd94219494f8f0d6760bdc0a0d2";
    }
}
