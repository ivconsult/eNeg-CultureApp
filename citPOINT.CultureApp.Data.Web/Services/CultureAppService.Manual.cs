#region → Usings   .
using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel;
using System;
using citPOINT.CultureApp.Data.Web.ServiceReference1;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Configuration;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 11.08.11     Yousra Reda       Creation
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
    /// Partial class that implements application logic 
    /// using the CultureAppEntities context.
    /// </summary>
    public partial class CultureAppService
    {

        #region → Fields         .
        private eNegServiceSoapClient mLoader;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader.
        /// </summary>
        /// <value>The loader.</value>
        public eNegServiceSoapClient Loader
        {
            get
            {
                if (mLoader == null)
                {
                    mLoader = new eNegServiceSoapClient();
                    InjectCredentials();
                }
                return mLoader;
            }
        }

        #endregion Properties

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentials()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)Loader.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }
        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the culture five dimensions for two cultures.
        /// </summary>
        /// <param name="UserCultureID">The user culture ID.</param>
        /// <param name="OtherCultureID">The other culture ID.</param>
        /// <returns></returns>        
        [Query(HasSideEffects = true)]
        public IQueryable<CultureFiveDimension> GetCultureFiveDimensionsForTwoCultures(int? UserCultureID, int OtherCultureID)
        {
            return this.ObjectContext
                       .CultureFiveDimensions
                       .Where(ss => ss.CultureID == UserCultureID ||
                                    ss.CultureID == OtherCultureID);
        }

        /// <summary>
        /// Gets the cultures.
        /// </summary>
        /// <returns></returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Culture> GetCultures()
        {
            List<Culture> Cultures = new List<Culture>();
            var results = this.Loader.GetCultures().RootResults.AsQueryable();
            foreach (var culture in results)
            {
                Culture current = new Culture()
                {
                    CultureID = culture.CultureID,
                    CultureName = culture.CultureName
                };
                Cultures.Add(current);
            }
            return Cultures.AsQueryable();
        }

        /// <summary>
        /// Gets the conversation partner culture.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <returns>Culture ID</returns>
        [Invoke]
        public int GetConversationPartnerCulture(Guid conversationID)
        {
            string partnerMail = this.Loader.GetPartnerMailForConversation(conversationID);

            #region → in case if thier is no messages .

            if (partnerMail == null || partnerMail.Length < 6)
            {
                return 0;
            }           

            #endregion

            #region → Getting the Mail Extension      .

            partnerMail = partnerMail.Replace(">", "").Replace("<", "").Trim();

            string mailExtnesion = partnerMail.ToLower().Substring(partnerMail.Length - 3);
            if (mailExtnesion[0] != '.')
            {
                return 0;
            }
            #endregion
            
            var partnerCulture = this.ObjectContext
                                     .DomainCultureMappings
                                     .Where(ss => ss.CultureID != null &&
                                                  ss.DomainExt.ToLower() == mailExtnesion);

            if (partnerCulture == null || partnerCulture.Count() == 0)
            {
                return 0;
            }

            return partnerCulture.First().CultureID.Value;
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageSender">The message sender.</param>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <returns>
        /// the result of executing the intended operation
        /// </returns>
        public bool SendAppsStatisticalsMessages(string AppName, Guid UserID, Guid conversationID, string messageContent, string messageSubject, string messageSender, string messageReceiver)
        {
            return Loader.UpdateAppsStatisticalsByMessages(AppName, UserID, conversationID, messageContent, messageSubject, messageSender, messageReceiver);
        }

        /// <summary>
        /// Updates the user culture.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="cultureID">The culture ID.</param>
        /// <returns>
        /// the result of executing the intended operation
        /// </returns>
        public bool UpdateUserCulture(Guid userID, int cultureID)
        {
            return Loader.UpdateUserCulture(userID, cultureID);
        }
        #endregion
        #endregion
    }
}
