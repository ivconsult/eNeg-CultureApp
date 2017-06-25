
#region → Usings   .
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.CultureApp.Data.Web;
using citPOINT.CultureApp.Common;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.08.11     M.Wahab       •creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Model
{

    #region  Using MEF to export FiveDimensionModel
    /// <summary>
    /// Model Layer for User Organizations managements.
    /// </summary>
    [Export(typeof(IFiveDimensionModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class FiveDimensionModel : IFiveDimensionModel
    {
        #region → Fields         .
        private CultureAppContext mContext;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private CultureAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new CultureAppContext(CultureAppConfigurations.MainServiceUri);

                    mContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mContext;
            }
        }

        /// <summary>
        /// True if _ctx.HasChanges is true; otherwise, false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }

            private set
            {
                if (this.mHasChanges != value)
                {
                    this.mHasChanges = value;
                    this.OnPropertyChanged("HasChanges");
                }
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = mContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Call back of getting Negotiation Culture.
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationCulture>> GetNegotiationCultureComplete;

        /// <summary>
        /// Call back of Conversation Culture complete.
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationCulture>> GetConversationCultureComplete;

        /// <summary>
        /// Call back of Culture Five Dimension 
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<CultureFiveDimension>> GetCultureFiveDimensionComplete;

        /// <summary>
        /// Call Back of Get cultures.
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;

        /// <summary>
        /// Call Back of Get Conversation Partner Culture.
        /// </summary>
        public event Action<InvokeOperation<int>> GetConversationPartnerCultureComplete;

        /// <summary>
        /// Call back of sending apps statisticals messages complete.
        /// </summary>
        public event Action<InvokeOperation<bool>> SendAppsStatisticalsMessagesComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        #endregion Private

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation

        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion Protected

        #region → Public         .

        #region IFiveDimensionModel Interface implementation

        /// <summary>
        /// Gets the culture async.
        /// </summary>
        public void GetCultureAsync()
        {
            PerformQuery<Culture>(Context.GetCulturesQuery(), GetCultureComplete);
        }

        /// <summary>
        /// Gets the negotiation culture async.
        /// </summary>
        public void GetNegotiationCultureAsync()
        {
            PerformQuery<NegotiationCulture>(Context.GetNegotiationCulturesQuery()
                                                    .Where(ss => ss.Deleted == false && ss.NegotiationID == CultureAppConfigurations.NegotiationIDParameter)
                                                    , GetNegotiationCultureComplete);
        }

        /// <summary>
        /// Gets the conversation culture async.
        /// </summary>
        public void GetConversationCultureAsync()
        {
            PerformQuery<ConversationCulture>(Context.GetConversationCulturesQuery()
                                                    .Where(ss => ss.Deleted == false && ss.ConversationID == CultureAppConfigurations.ConversationIDParameter)
                                                    , GetConversationCultureComplete);
        }

        /// <summary>
        /// Gets the conversation partner culture async.
        /// </summary>
        public void GetConversationPartnerCultureAsync()
        {
            this.Context.GetConversationPartnerCulture(CultureAppConfigurations.ConversationIDParameter
                , GetConversationPartnerCultureComplete, null);
        }

        /// <summary>
        /// Gets the culture five dimension.
        /// </summary>
        public void GetCultureFiveDimensionAsync()
        {
            PerformQuery<CultureFiveDimension>(Context.GetCultureFiveDimensionsForTwoCulturesQuery(CultureAppConfigurations.CurrentLoginUser.CultureID, CultureAppConfigurations.PartnerCultureID),
                                                  GetCultureFiveDimensionComplete);
        }

        /// <summary>
        /// Adds the negotiation culture.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public NegotiationCulture AddNegotiationCulture(Guid negotiationID, bool setInContext)
        {
            #region → Add New organization  .

            NegotiationCulture negotiationCulture = new NegotiationCulture()
            {
                NegotiationCultureID = Guid.NewGuid(),
                NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.AskMe,
                NegotiationID = negotiationID,
                Deleted = false,
                DeletedBy = CultureAppConfigurations.CurrentLoginUser.UserID,
                DeletedOn = DateTime.Now,
            };


            if (setInContext)
                this.Context.NegotiationCultures.Add(negotiationCulture);


            #endregion

            return negotiationCulture;
        }

        /// <summary>
        /// Adds the conversation culture.
        /// </summary>
        /// <param name="ConversationID">The conversation ID.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public ConversationCulture AddConversationCulture(Guid ConversationID, bool SetInContext)
        {
            #region → Add New organization  .

            ConversationCulture ConversationCulture = new ConversationCulture()
            {
                ConversationCultureID = Guid.NewGuid(),
                ConversationID = ConversationID,
                Deleted = false,
                DeletedBy = CultureAppConfigurations.CurrentLoginUser.UserID,
                DeletedOn = DateTime.Now,
            };


            if (SetInContext)
                this.Context.ConversationCultures.Add(ConversationCulture);


            #endregion

            return ConversationCulture;
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        public void SendAppsStatisticalsMessages(Guid conversationID, string messageContent)
        {
            InvokeOperation<bool> result
                                = this.Context
                                      .SendAppsStatisticalsMessages(
                                           CultureAppConfigurations.AppName,
                                           CultureAppConfigurations.CurrentLoginUser.UserID,
                                           conversationID,
                                           messageContent,
                                           CultureAppConfigurations.AppName + " Feedback",
                                           "Culture App",
                                           "eNeg System",
                                           SendAppsStatisticalsMessagesComplete,
                                           null);
        }

        /// <summary>
        /// Updates the user culture.
        /// </summary>
        public void UpdateUserCultureAsync()
        {
            Context.UpdateUserCulture(CultureAppConfigurations.CurrentLoginUser.UserID,
                CultureAppConfigurations.CurrentLoginUser.CultureID.Value);
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        #endregion

        #endregion

        #endregion Methods
    }
}