
#region → Usings   .

using System;
using citPOINT.CultureApp.Data.Web;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;

#endregion

#region → History  .


/* Date         User              Change
 * 
 * 17.08.11     M.Wahab         Creation
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
    /// Iinterface for Five Dimension Model
    /// </summary>
    public interface IFiveDimensionModel
    {

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Call back of getting Negotiation Culture.
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationCulture>> GetNegotiationCultureComplete;

        /// <summary>
        /// Call back of Conversation Culture complete.
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ConversationCulture>> GetConversationCultureComplete;

        /// <summary>
        /// Call back of Culture Five Dimension 
        /// </summary>
        event EventHandler<eNegEntityResultArgs<CultureFiveDimension>> GetCultureFiveDimensionComplete;

        /// <summary>
        /// Call Back of Get cultures.
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;

        /// <summary>
        /// Call Back of Get Conversation Partner Culture.
        /// </summary>
        event Action<InvokeOperation<int>> GetConversationPartnerCultureComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Call back of sending apps statisticals messages complete.
        /// </summary>
        event Action<InvokeOperation<bool>> SendAppsStatisticalsMessagesComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;


        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets the culture async.
        /// </summary>
        void GetCultureAsync();

        /// <summary>
        /// Gets the negotiation culture async.
        /// </summary>
        void GetNegotiationCultureAsync();

        /// <summary>
        /// Gets the conversation culture async.
        /// </summary>
        void GetConversationCultureAsync();

        /// <summary>
        /// Gets the conversation partner culture async.
        /// </summary>
        void GetConversationPartnerCultureAsync();

        /// <summary>
        /// Gets the culture five dimension async.
        /// </summary>
        void GetCultureFiveDimensionAsync();

        /// <summary>
        /// Adds the conversation culture.
        /// </summary>
        /// <param name="ConversationID">The conversation ID.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>return an instance of Conversation Culture</returns>
        ConversationCulture AddConversationCulture(Guid ConversationID, bool SetInContext);

        /// <summary>
        /// Adds the negotiation culture.
        /// </summary>
        /// <param name="NegotiationID">The negotiation ID.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>return an instance of Negotiation Culture</returns>
        NegotiationCulture AddNegotiationCulture(Guid NegotiationID, bool SetInContext);

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        void SendAppsStatisticalsMessages(Guid conversationID, string messageContent);

        /// <summary>
        /// Updates the user culture.
        /// </summary>
        void UpdateUserCultureAsync();

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        void SaveChangesAsync();

        #endregion
    }
}
