﻿#region → Usings   .
using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using citPOINT.CultureApp.Data.Web;
using citPOINT.eNeg.Apps.Common.Interfaces;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 17.08.11     Yousra Reda       Creation
 * 17.08.11     Yousra Reda       Save current Login User
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.CultureApp.Common
{
    /// <summary>
    /// Culture App Configurations.
    /// </summary>
    public class CultureAppConfigurations
    {
        #region → Properties     .

        #region Static

        /// <summary>
        /// Gets or sets the current login user.
        /// </summary>
        /// <value>The current login user.</value>
        public static IUserInfo CurrentLoginUser { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public static string AppName { get { return "Culture App"; } }

        /// <summary>
        /// Gets or sets the negotiation ID paramter.
        /// </summary>
        /// <value>The negotiation ID paramter.</value>
        public static Guid NegotiationIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the conversation ID paramter.
        /// </summary>
        /// <value>The conversation ID paramter.</value>
        public static Guid ConversationIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the partner culture ID.
        /// </summary>
        /// <value>The partner culture ID.</value>
        public static int PartnerCultureID { get; set; }
        
        /// <summary>
        /// Gets or sets the main platform info.
        /// </summary>
        /// <value>The main platform info.</value>
        public static IMainPlatformInfo MainPlatformInfo { get; set; }

        /// <summary>
        /// Gets the main service URI.
        /// </summary>
        /// <value>The main service URI.</value>
        public static Uri MainServiceUri
        {
            get
            {
                if (CultureAppConfigurations.MainPlatformInfo != null)
                {

                    var app = CultureAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(CultureAppConfigurations.AppName);

                    if (app != null && !string.IsNullOrEmpty(app.ApplicationMainServicePath))
                    {
                        return new Uri(app.ApplicationMainServicePath, UriKind.Absolute);
                    }
                }

                return new Uri(string.Empty, UriKind.Absolute);
            }
        }
        #endregion

        #endregion
    }
}

