using System.Data;
using System.Reflection;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using WebPrint.Data.Mapping;

namespace WebPrint.Data
{
    /// <summary>
    /// wrapping ISesionFactory so that other assembly needn't to reference NHibernate.dll 
    /// </summary>
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private static readonly ISessionFactory sessionFactory;

        private static Configuration Configuration { get; set; }

        #region ISessionFactoryProvider 成员

        public ISessionFactory SessionFactory
        {
            get { return sessionFactory; }
        }

        #endregion

        static SessionFactoryProvider()
        {
            Configuration = BuildConfiguration();
            sessionFactory = Configuration.BuildSessionFactory();
        }

        private static Configuration BuildConfiguration()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(db =>
                {
                    db.ConnectionStringName = "pgsqlConn";
                    db.Dialect<PostgreSQL82Dialect>();
                    db.Driver<NpgsqlDriver>();
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    db.IsolationLevel = IsolationLevel.ReadCommitted;
                    db.BatchSize = 100;
                    db.LogFormattedSql = true;
                    db.AutoCommentSql = true;
#if DEBUG
                    db.LogSqlInConsole = true;
#endif
                });

            cfg.AddMapping(BuildMappings());
            /* Not supported in PostgreSQL */
            //SchemaMetadataUpdater.QuoteTableAndColumns(cfg);

            return cfg;
        }

        private static HbmMapping BuildMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly
                                   .GetAssembly(typeof (UserMapping))
                                   .GetExportedTypes()
                                   .Where(t => t.Name.EndsWith("Mapping")));
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }

#if DEBUG
        public static string BuildMappingsXml()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly
                                   .GetAssembly(typeof(UserMapping))
                                   .GetExportedTypes()
                                   .Where(t => t.Name.EndsWith("Mapping")));
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping.AsString();
        }
#endif
    }
}
