using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SimpleBlog.Models;
using NHibernate;

namespace SimpleBlog
{
    public static class DatabaseManager
    {

        private const string SESSION_KEY = "SimpleBlog.Database.SessionKey";
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Getter for NHibernate session
        /// </summary>
        public static ISession Session
        {
            get {  return (ISession)HttpContext.Current.Items[SESSION_KEY]; }
        }

        /// <summary>
        /// Configure ORM NHibernate mapping with database tables
        /// </summary>
        public static void Configure()
        {
             Configuration config = new Configuration();

            // 1. Configure based on connection string
            // -----------------------------------------------------------
            config.Configure(); 
   
            // 2. Add Nhibernate mappings
            // -----------------------------------------------------------
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<PostMap>();
            mapper.AddMapping<TagMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities()); 

            // 3. Create session factory
            // -----------------------------------------------------------
            _sessionFactory = config.BuildSessionFactory(); 
        }

        /// <summary>
        /// Open NHibernate session. Called each Http request.
        /// </summary>
        public static void OpenSession()
        {
            HttpContext.Current.Items[SESSION_KEY] = _sessionFactory.OpenSession(); 
        }

        /// <summary>
        /// Close NHibernate session.
        /// </summary>
        public static void CloseSession()
        {
            ISession session = HttpContext.Current.Items[SESSION_KEY] as ISession;
            if (session != null)
            {
                session.Close(); 
            }
            
            HttpContext.Current.Items.Remove(SESSION_KEY); 

        }


    }
}