using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataAccess.Classes
{
    public class DAOFactory : DbProviderFactory
    {
        private static string connectionString;
        private static DbConnection connection = null;

        private static DbProviderFactory factory;

        public static DbConnection getConnection()
        {
            factory = Singleton<DAOFactory>.Instance();

            try
            {
                // String de conexão banco de Stage
                connectionString = @"Data Source=172.16.130.10; Initial Catalog=SERVICE_PROCALCULO; User Id=sa; Password=mac.08.sa";
                var asyncConnectionString = 
                    new SqlConnectionStringBuilder(connectionString)
                        {
                            AsynchronousProcessing = true
                        }.ToString();

                factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                connection = factory.CreateConnection();
                connection.ConnectionString = asyncConnectionString;
                
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
            }
            catch (Exception e)
            {
                new LogDAO().GravarLog(new Model.ErroLog()
                {
                    Descricao = "Erro ao criar conexão com banco de dados!",
                    ExceptioMessage = e.Message,
                    StackTrace = e.StackTrace,
                    DataErro = DateTime.Now
                });
            }

            return connection;
        }

        public static DbProviderFactory getFactory()
        {
            return factory;
        }
    }
}
