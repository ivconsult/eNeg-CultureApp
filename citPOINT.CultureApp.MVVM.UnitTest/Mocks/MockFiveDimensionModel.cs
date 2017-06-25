
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using citPOINT.CultureApp.Common;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;
using citPOINT.eNeg.Common;
using citPOINT.CultureApp.Data.Web;
using citPOINT.CultureApp.Data;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 06.09.11     Y.Mohammed     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.CultureApp.MVVM.UnitTest
{
    /// <summary>
    /// Mock of Five Dimension Model
    /// </summary>
    public class MockFiveDimensionModel : IFiveDimensionModel
    {
        #region → Fields         .
        private CultureAppContext mContext;
        private List<Culture> mCultureSource;
        private List<NegotiationCulture> mNegotiationCultures;
        private List<ConversationCulture> mConversationCultures;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return true; }
        }

        /// <summary>
        /// property with a getter only to can use eNegService
        /// </summary>
        public CultureAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new CultureAppContext(new Uri("http://localhost:9003/citPOINT-CultureApp-Data-Web-CultureAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }

        #region <9> Cultures             .

        /// <summary>
        /// Gets the Culture source.
        /// </summary>
        /// <value>The Culture source.</value>
        public List<Culture> CultureSource
        {
            get
            {
                if (mCultureSource == null)
                {
                    mCultureSource = new List<Culture>();
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 1,
                        CultureName = @"Arab World",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 2,
                        CultureName = @"Argentina",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 3,
                        CultureName = @"Australia",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 5,
                        CultureName = @"Austria",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 6,
                        CultureName = @"Bangladesh",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 7,
                        CultureName = @"Belgium",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 8,
                        CultureName = @"Brazil",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 9,
                        CultureName = @"Bulgaria",
                    });
                }
                return mCultureSource;
            }
        }
        #endregion

        #region <3> ConversationCultures .

        /// <summary>
        /// Gets the conversation cultures.
        /// </summary>
        /// <value>The conversation cultures.</value>
        public List<ConversationCulture> ConversationCultures
        {
            get
            {
                if (mConversationCultures == null)
                {
                    mConversationCultures = new List<ConversationCulture>()
                       {
                           new ConversationCulture ()
                            { 
                                ConversationCultureID=Guid.NewGuid(),
                                ConversationID=new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03EA"),
                                PartnerCultureID=1,
                                Deleted=false,
                                DeletedBy=Guid.NewGuid(),
                                DeletedOn=DateTime.Now
                            },
                            new ConversationCulture ()
                            { 
                                ConversationCultureID=Guid.NewGuid(),
                                ConversationID=Guid.NewGuid(),
                                PartnerCultureID=2,
                                Deleted=false,
                                DeletedBy=Guid.NewGuid(),
                                DeletedOn=DateTime.Now
                            },
                            new ConversationCulture ()
                            { 
                                ConversationCultureID=Guid.NewGuid(),
                                ConversationID=Guid.NewGuid(),
                                PartnerCultureID=5,
                                Deleted=false,
                                DeletedBy=Guid.NewGuid(),
                                DeletedOn=DateTime.Now
                            }
                       };
                }
                return mConversationCultures;
            }
        }

        #endregion ConversationCultures

        #region <3> NegotiationCultures  .

        /// <summary>
        /// Gets the negotiation cultures.
        /// </summary>
        /// <value>The negotiation cultures.</value>
        public List<NegotiationCulture> NegotiationCultures
        {
            get
            {
                if (mNegotiationCultures == null)
                {
                    mNegotiationCultures = new List<NegotiationCulture>()
                       {
                           new NegotiationCulture ()
                            { 
                                NegotiationCultureID=Guid.NewGuid(),
                                NegotiationID=new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB"),
                                NegotiationCultureType=CultureAppConstant.NegotiationCultureTypes.Always,
                                DefaultCultureID=2,
                                Deleted=false,
                                DeletedBy=Guid.NewGuid(),
                                DeletedOn=DateTime.Now
                            },
                            new NegotiationCulture ()
                            { 
                                NegotiationCultureID=Guid.NewGuid(),
                                NegotiationID=Guid.NewGuid(),
                                NegotiationCultureType=CultureAppConstant.NegotiationCultureTypes.AskMe,
                                DefaultCultureID=3,
                                Deleted=false,
                                DeletedBy=Guid.NewGuid(),
                                DeletedOn=DateTime.Now
                            },
                             new NegotiationCulture ()
                            { 
                                NegotiationCultureID=Guid.NewGuid(),
                                NegotiationID=Guid.NewGuid(),
                                NegotiationCultureType=CultureAppConstant.NegotiationCultureTypes.TryToRecognize,
                                DefaultCultureID=4,
                                Deleted=false,
                                DeletedBy=Guid.NewGuid(),
                                DeletedOn=DateTime.Now
                            }
                       };
                }
                return mNegotiationCultures;
            }
        }

        #endregion NegotiationCultures

        #region <9> CultureFiveDimensions.

        /// <summary>
        /// Gets the culture five dimensions.
        /// </summary>
        /// <value>The culture five dimensions.</value>
        public List<CultureFiveDimension> CultureFiveDimensions
        {
            get
            {
                if (mCultureFiveDimensions == null)
                {
                    mCultureFiveDimensions = new List<CultureFiveDimension>()
                       {
                           new CultureFiveDimension ()
                            { 
                                CultureID=1,
                                PDI=10,
                                IDV=35,
                                MAS=80,
                                UAI=65,
                                LTO=25
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=2,
                                PDI=13,
                                IDV=60,
                                MAS=42,
                                UAI=71,
                                LTO=13
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=3,
                                PDI=55,
                                IDV=45,
                                MAS=83,
                                UAI=17,
                                LTO=100
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=4,
                                PDI=29,
                                IDV=48,
                                MAS=59,
                                UAI=52,
                                LTO=78
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=5,
                                PDI=30,
                                IDV=15,
                                MAS=70,
                                UAI=55,
                                LTO=15
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=6,
                                PDI=45,
                                IDV=79,
                                MAS=18,
                                UAI=26,
                                LTO=11
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=7,
                                PDI=18,
                                IDV=53,
                                MAS=35,
                                UAI=98,
                                LTO=17
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=8,
                                PDI=62,
                                IDV=15,
                                MAS=78,
                                UAI=14,
                                LTO=36
                            },
                            new CultureFiveDimension ()
                            { 
                                CultureID=9,
                                PDI=75,
                                IDV=95,
                                MAS=11,
                                UAI=88,
                                LTO=30
                            }
                       };
                }
                return mCultureFiveDimensions;
            }
        }
        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockFiveDimensionModel"/> class.
        /// </summary>
        public MockFiveDimensionModel()
        {
            CultureAppConfigurations.ConversationIDParameter = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03EA");
            CultureAppConfigurations.NegotiationIDParameter = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB");
            CultureAppConfigurations.PartnerCultureID = 5;

            CultureAppConfigurations.CurrentLoginUser = new LoginUser();
            CultureAppConfigurations.CurrentLoginUser.CultureID = 1;
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
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call back of sending apps statisticals messages complete.
        /// </summary>
        public event Action<InvokeOperation<bool>> SendAppsStatisticalsMessagesComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;
        private List<CultureFiveDimension> mCultureFiveDimensions;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets the culture async.
        /// </summary>
        public void GetCultureAsync()
        {
            if (GetCultureComplete != null)
            {
                GetCultureComplete(this, new eNegEntityResultArgs<Culture>(CultureSource));
            }
        }

        /// <summary>
        /// Gets the negotiation culture async.
        /// </summary>
        public void GetNegotiationCultureAsync()
        {
            if (GetNegotiationCultureComplete != null)
            {
                GetNegotiationCultureComplete(this, new eNegEntityResultArgs<NegotiationCulture>(NegotiationCultures));
            }
        }

        /// <summary>
        /// Gets the conversation culture async.
        /// </summary>
        public void GetConversationCultureAsync()
        {
            if (GetConversationCultureComplete != null)
            {
                GetConversationCultureComplete(this, new eNegEntityResultArgs<ConversationCulture>(ConversationCultures));
            }
        }

        /// <summary>
        /// Gets the conversation partner culture async.
        /// </summary>
        public void GetConversationPartnerCultureAsync()
        {
            if (GetConversationPartnerCultureComplete != null)
            {
                GetConversationPartnerCultureComplete(null);
            }
        }

        /// <summary>
        /// Gets the culture five dimension async.
        /// </summary>
        /// <param name="UserCultureID">The user culture ID.</param>
        /// <param name="PartnerCultureID">The partner culture ID.</param>
        public void GetCultureFiveDimensionAsync()
        {
            if (GetCultureFiveDimensionComplete != null)
            {
                GetCultureFiveDimensionComplete(this, new eNegEntityResultArgs<CultureFiveDimension>(CultureFiveDimensions));
            }
        }

        /// <summary>
        /// Adds the conversation culture.
        /// </summary>
        /// <param name="ConversationID">The conversation ID.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>
        /// return an instance of Conversation Culture
        /// </returns>
        public ConversationCulture AddConversationCulture(Guid ConversationID, bool SetInContext)
        {
            ConversationCulture ConvCulture = new ConversationCulture()
            {
                ConversationCultureID = Guid.NewGuid(),
                ConversationID = Guid.NewGuid(),
                PartnerCultureID = 9,
                Deleted = false,
                DeletedBy = Guid.NewGuid(),
                DeletedOn = DateTime.Now
            };
            if (SetInContext)
            {
                ConversationCultures.Add(ConvCulture);
            }
            return ConvCulture;
        }

        /// <summary>
        /// Adds the negotiation culture.
        /// </summary>
        /// <param name="NegotiationID">The negotiation ID.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>
        /// return an instance of Negotiation Culture
        /// </returns>
        public NegotiationCulture AddNegotiationCulture(Guid NegotiationID, bool SetInContext)
        {
            NegotiationCulture NegCulture = new NegotiationCulture()
            {
                NegotiationCultureID = Guid.NewGuid(),
                NegotiationID = Guid.NewGuid(),
                NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.AskMe,
                DefaultCultureID = 5,
                Deleted = false,
                DeletedBy = Guid.NewGuid(),
                DeletedOn = DateTime.Now
            };
            if (SetInContext)
            {
                NegotiationCultures.Add(NegCulture);
            }
            return NegCulture;
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        public void SendAppsStatisticalsMessages(Guid conversationID, string messageContent)
        {

        }

        /// <summary>
        /// Updates the user culture.
        /// </summary>
        public void UpdateUserCultureAsync()
        {

        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {

        }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }

        #endregion
    }
}
