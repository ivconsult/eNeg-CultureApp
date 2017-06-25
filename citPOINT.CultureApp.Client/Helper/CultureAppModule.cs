#region → Usings   .

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using citPOINT.CultureApp.Common;
using citPOINT.CultureApp.Data.Web;
using citPOINT.CultureApp.ViewModel;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Model;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 21.03.12     M.Wahab       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.CultureApp.Client
{
    /// <summary>
    /// Preference App Module.
    /// </summary>
    [ModuleExport(typeof(CultureAppModule))]
    public class CultureAppModule : IModule
    {
        #region → Fields         .

        private readonly IRegionManager regionManager;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public static CompositionContainer Container { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureAppModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="MainPlatformInfo">The main platform info.</param>
        [ImportingConstructor()]
        public CultureAppModule(IRegionManager regionManager, IMainPlatformInfo MainPlatformInfo)
        {
            this.regionManager = regionManager;

            CultureAppConfigurations.MainPlatformInfo = MainPlatformInfo;

            //CultureAppConfigurations.ActionTypeParameter = CultureAppConfigurations.ActionTypes.Report.ToString();

            this.IntializeContainer();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private void IntializeContainer()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CultureAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(FiveDimensionViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(FiveDimensionModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CultureFiveDimension).Assembly));

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(PreferenceSetNeg).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion
                    (CultureAppConfigurations.AppName.Replace(" ", "") + "Region",
                     typeof(MainPageView));
            }
            catch (System.Exception ex)
            {
                CultureAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, CultureAppConfigurations.AppName);
            }

        }

        #endregion

        #endregion

    }
}
