#region → Usings   .
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using citPOINT.CultureApp.Common;
using citPOINT.CultureApp.ViewModel;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Apps.Common.Enums;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 14.08.11     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.CultureApp.Client
{
    /// <summary>
    /// Main page View that used as a container to 
    /// any other view in the App after authenticating user
    /// </summary>
    [Export]
    public partial class MainPageView : UserControl, IObserverApp
    {

        #region → Fields         .

        private NegotiationCultureView mNegotiationView;
        private ConversationCultureGraph mConversationGraphView;
        private HelpView mHelpView;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        private ViewModelRepository ViewModelRepository { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get { return CultureAppConfigurations.AppName; }
        }

        /// <summary>
        /// Gets the negotiation view.
        /// </summary>
        /// <value>The negotiation view.</value>
        public NegotiationCultureView NegotiationView
        {
            get
            {
                if (mNegotiationView == null)
                {
                    mNegotiationView = new NegotiationCultureView();
                }
                return mNegotiationView;
            }
        }

        /// <summary>
        /// Gets the conversation graph view.
        /// </summary>
        /// <value>The conversation graph view.</value>
        public ConversationCultureGraph ConversationGraphView
        {
            get
            {
                if (mConversationGraphView == null)
                {
                    mConversationGraphView = new ConversationCultureGraph();
                }
                return mConversationGraphView;
            }
        }

        /// <summary>
        /// Gets the help view.
        /// </summary>
        /// <value>The help view.</value>
        public HelpView HelpView
        {
            get
            {
                if (mHelpView == null)
                {
                    mHelpView = new HelpView();
                }
                return mHelpView;
            }
        }

        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageView"/> class.
        /// </summary>
        public MainPageView()
        {
            InitializeComponent();

            #region Use MEF To load the View Model

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                CultureAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);

                CultureAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            }

            #endregion

            try
            {
                this.ApplyChanges(false);

                CultureAppConfigurations.MainPlatformInfo.TrackChanges.AddObserverApp(this);
            }
            catch (Exception ex)
            {
                CultureAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, CultureAppConfigurations.AppName);
            }

        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            CultureAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, CultureAppConfigurations.AppName);
        }

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnChangeScreen(string PageName)
        {
            switch (PageName)
            {
                case CultureAppViewTypes.NegotiationCultureView:
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    this.uxMainContent.Content = this.NegotiationView;
                    break;

                case CultureAppViewTypes.ConversationCultureGraphView:
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    this.uxMainContent.Content = this.ConversationGraphView;
                    break;
                case CultureAppViewTypes.HelpView:
                    this.uxMainContent.Content = this.HelpView;
                    break;
            }
        }


        #endregion

        #region → public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public void ApplyChanges(bool isActive)
        {
            if (isActive)
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;

                #region → Change Negotiation      .

                if (CultureAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
                {
                    CultureAppConfigurations.NegotiationIDParameter = CultureAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;
                }
                else
                {
                    CultureAppConfigurations.NegotiationIDParameter = Guid.Empty;
                }

                #endregion

                #region → Change Conversation     .

                if (CultureAppConfigurations.MainPlatformInfo.CurrentConversation != null)
                {
                    CultureAppConfigurations.ConversationIDParameter = CultureAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
                }
                else
                {
                    CultureAppConfigurations.ConversationIDParameter = Guid.Empty;
                }

                #endregion

                #region → Change User             .

                if (CultureAppConfigurations.CurrentLoginUser != null && CultureAppConfigurations.CurrentLoginUser.UserID != CultureAppConfigurations.MainPlatformInfo.UserInfo.UserID)
                {
                    if (this.ViewModelRepository != null)
                    {
                        this.ViewModelRepository.Cleanup();

                        this.ViewModelRepository = null;
                    }
                }

                CultureAppConfigurations.CurrentLoginUser = CultureAppConfigurations.MainPlatformInfo.UserInfo;

                #endregion

                #region → View Model Repository   .

                if (ViewModelRepository != null)
                {
                    ViewModelRepository.FiveDimensionViewModel.ApplyChanges();
                }
                else
                {
                    ViewModelRepository = new ViewModelRepository();
                }

                this.DataContext = ViewModelRepository.FiveDimensionViewModel;

                #endregion

                #region → Adjust Widht and Heihgt .

                this.uxMainContent.Width = CultureAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Width;
                this.uxMainContent.MinWidth = this.uxMainContent.Width;
                this.uxMainContent.MaxWidth = this.uxMainContent.Width;

                this.uxMainContent.Height = CultureAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Height;
                this.uxMainContent.MinHeight = this.uxMainContent.Height;
                this.uxMainContent.MaxHeight = this.uxMainContent.Height;

                #endregion

            }
            else
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion

        #endregion



    }
}
