

#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;
#endregion

#region → History  .

/* Date         User        Change
 * 
 * 16.08.11     M.Wahab     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.CultureApp.Data.Web
{
    /// <summary>
    /// Class represent Culture entity loaded from eNeg.
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class Culture : EntityObject
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the conversation ID.
        /// </summary>
        /// <value>The conversation ID.</value>
        [DataMemberAttribute()]
        [Key]
        public int CultureID { get; set; }

        /// <summary>
        /// Gets or sets the name of the conversation.
        /// </summary>
        /// <value>The name of the conversation.</value>
        [DataMemberAttribute()]
        public string CultureName { get; set; }

        #endregion
     
    }
}
