using System.Collections.Generic;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;

namespace WorkWithEncryptedDatabase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    var credential = new InteractiveBrowserCredential();
                    var azureKeyVaultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(credential);

                    var providers = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>
                    {
                        { SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVaultProvider }
                    };

                    SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
