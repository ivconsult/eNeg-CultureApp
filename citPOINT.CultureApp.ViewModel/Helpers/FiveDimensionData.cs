#region → Usings   .
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 23.08.11     Y.Mohammed     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.CultureApp.ViewModel
{
    /// <summary>
    /// Used as a data type to pushed as a datasource for 5 dimension graph
    /// </summary>
    public class FiveDimensionData : Entity
    {
        /// <summary>
        /// Gets or sets the name of the dimension.
        /// </summary>
        /// <value>The name of the dimension.</value>
        public string DimensionName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user culture dimension value.
        /// </summary>
        /// <value>The user culture dimension value.</value>
        public int UserCultureDimensionValue { get; set; }

        /// <summary>
        /// Gets or sets the partner culture dimension value.
        /// </summary>
        /// <value>The partner culture dimension value.</value>
        public int PartnerCultureDimensionValue { get; set; }
    }
}
