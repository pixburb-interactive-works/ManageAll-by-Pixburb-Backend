using LightInject;
using Pixburb.Business.Implementation;
using Pixburb.Business.Interface;
using Pixburb.DataAccess.Implementation;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pixburb.Reader.App_Start
{
    public static class ContainerConfig
    {
        #region Constructors and private fields

        /// <summary>
        /// Read Only ContainerLock.
        /// </summary>
        private static readonly object ContainerLock = new object();

        /// <summary>
        /// Service Container.
        /// </summary>
        private static ServiceContainer servicecontainer;

        #endregion Constructors and private fields

        #region Public and protected properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static ServiceContainer Current
        {
            get
            {
                if (servicecontainer == null)
                {
                    lock (ContainerLock)
                    {
                        if (servicecontainer == null)
                        {
                            var container = new ServiceContainer();
                            container.LoadConfiguration();
                            servicecontainer = container;
                        }
                    }
                }

                return servicecontainer;
            }
        }

        #endregion Public and protected properties

        #region Private helpers

        /// <summary>
        /// ContainerConfig Initialize.
        /// </summary>
        public static void Initialize()
        {
            Current.RegisterApiControllers();
            Current.EnableWebApi(GlobalConfiguration.Configuration);
        }

        /// <summary>
        /// Load Configuration.
        /// </summary>
        /// <param name="container">Service Container.</param>
        public static void LoadConfiguration(this ServiceContainer container)
        {
            container.Register<IAdminLoginReader, AdminLoginReader>();
            container.Register<IAdminLoginDataReader, AdminLoginDataReader>();
            container.Register<IInventoryReader, InventoryReader>();
            container.Register<IInventoryDataReader, InventoryDataReader>();
        }

        #endregion Private helpers
    }
}