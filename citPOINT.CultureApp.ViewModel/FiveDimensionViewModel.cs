#region → Usings   .
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using citPOINT.CultureApp.Common;
using System.Collections.Generic;
using citPOINT.CultureApp.Data.Web;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 18.08.11     Y.Mohammed     • creation
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
    #region → Using  MEF to export FiveDimensionViewModel
    /// <summary>
    /// this class is to Five Dimension View Model.
    /// </summary>
    [Export(CultureAppViewModelTypes.FiveDimensionViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class FiveDimensionViewModel : ViewModelBase
    {

        #region → Fields         .

        private IFiveDimensionModel mFiveDimensionModel;
        private IEnumerable<Culture> mCultureEntries;

        private Culture mCurrentUserCulture;

        private NegotiationCulture mCurrentNegotiationCulture;
        private ConversationCulture mCurrentConversationCulture;
        private IEnumerable<CultureFiveDimension> mFiveDimensionValues;
        private List<FiveDimensionData> mFiveDimensionValuesDataSeries;

        private bool mIsAlways;
        private bool mIsAskMe;
        private bool mIsTryToRecognize;
        private bool mCanExcuteBackToCommand;
        private bool RunQueueForApplyChanges;
        private bool mIsBusy;

        private RelayCommand mSubmitChangeCommand;
        private RelayCommand mBackToGraphCommand;
        private RelayCommand mNavigateToHelpCommand;

        private bool mIsUserCultureNotDefined;
        private Culture mCurrentPartnerCulture;
        private RelayCommand mCultureSelectionChangedCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return mIsBusy; }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");

                if (!this.IsBusy)
                {
                    if (RunQueueForApplyChanges)
                    {
                        ApplyChanges();
                    }
                }
            }
        }

        #region → Used in Binding Negotiation Culture choice .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is always.
        /// </summary>
        /// <value><c>true</c> if this instance is always; otherwise, <c>false</c>.</value>
        public bool IsAlways
        {
            get
            {
                return mIsAlways;
            }
            set
            {
                mIsAlways = value;
                if (value == true && CurrentNegotiationCulture != null)
                {
                    CurrentNegotiationCulture.NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.Always;
                }
                RaisePropertyChanged("IsAlways");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is try to recognize.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is try to recognize; otherwise, <c>false</c>.
        /// </value>
        public bool IsTryToRecognize
        {
            get
            {
                return mIsTryToRecognize;
            }
            set
            {
                mIsTryToRecognize = value;
                if (value == true && CurrentNegotiationCulture != null)
                {
                    CurrentNegotiationCulture.NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.TryToRecognize;
                }
                RaisePropertyChanged("IsTryToRecognize");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ask me.
        /// </summary>
        /// <value><c>true</c> if this instance is ask me; otherwise, <c>false</c>.</value>
        public bool IsAskMe
        {
            get
            {
                return mIsAskMe;
            }
            set
            {
                mIsAskMe = value;
                if (value == true && CurrentNegotiationCulture != null)
                {
                    CurrentNegotiationCulture.NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.AskMe;
                }
                RaisePropertyChanged("IsAskMe");
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets the five dimension values data series.
        /// </summary>
        /// <value>The five dimension values data series.</value>
        public List<FiveDimensionData> FiveDimensionValuesDataSeries
        {
            get
            {
                return mFiveDimensionValuesDataSeries;
            }
            set
            {
                if (mFiveDimensionValuesDataSeries != value)
                {
                    mFiveDimensionValuesDataSeries = value;
                    RaisePropertyChanged("FiveDimensionValuesDataSeries");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user culture not defined.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user culture not defined; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserCultureNotDefined
        {
            get
            {
                return mIsUserCultureNotDefined;
            }
            set
            {
                mIsUserCultureNotDefined = value;
                RaisePropertyChanged("IsUserCultureNotDefined");
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can excute back to command.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can excute back to command; otherwise, <c>false</c>.
        /// </value>
        public bool CanExcuteBackToCommand
        {
            get
            {
                return mCanExcuteBackToCommand;
            }
            set
            {
                mCanExcuteBackToCommand = value;
                this.RaisePropertyChanged("CanExcuteBackToCommand");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [need to send apps statisticals messages].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [need to send apps statisticals messages]; otherwise, <c>false</c>.
        /// </value>
        public bool NeedToSendAppsStatisticalsMessages { get; set; }

        /// <summary>
        /// Gets or sets the culture entries.
        /// </summary>
        /// <value>The culture entries.</value>
        public IEnumerable<Culture> CultureEntries
        {
            get
            {
                if (mCultureEntries == null)
                {
                    mCultureEntries = new List<Culture>();
                }
                return mCultureEntries;
            }
            set
            {
                mCultureEntries = value;
                RaisePropertyChanged("CultureEntries");
            }
        }

        /// <summary>
        /// Gets or sets the current user culture.
        /// </summary>
        /// <value>The current user culture.</value>
        public Culture CurrentUserCulture
        {
            get
            {
                return mCurrentUserCulture;
            }
            set
            {
                mCurrentUserCulture = value;
                this.RaisePropertyChanged("CurrentUserCulture");
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current partner culture.
        /// </summary>
        /// <value>The current partner culture.</value>
        public Culture CurrentPartnerCulture
        {
            get
            {
                return mCurrentPartnerCulture;
            }
            set
            {
                mCurrentPartnerCulture = value;
                this.RaisePropertyChanged("CurrentPartnerCulture");
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current negotiation culture.
        /// </summary>
        /// <value>The current negotiation culture.</value>
        public NegotiationCulture CurrentNegotiationCulture
        {
            get
            {
                if (mCurrentNegotiationCulture == null)
                {
                    mCurrentNegotiationCulture = new NegotiationCulture();
                }
                return mCurrentNegotiationCulture;
            }
            set
            {
                mCurrentNegotiationCulture = value;
                RaisePropertyChanged("CurrentNegotiationCulture");
            }
        }

        /// <summary>
        /// Gets or sets the current conversation culture.
        /// </summary>
        /// <value>The current conversation culture.</value>
        public ConversationCulture CurrentConversationCulture
        {
            get
            {
                if (mCurrentConversationCulture == null)
                {
                    mCurrentConversationCulture = new ConversationCulture();
                }
                return mCurrentConversationCulture;
            }
            set
            {
                mCurrentConversationCulture = value;
                RaisePropertyChanged("CurrentConversationCulture");
            }
        }

        /// <summary>
        /// Gets or sets the five dimension values.
        /// </summary>
        /// <value>The five dimension values.</value>
        public IEnumerable<CultureFiveDimension> FiveDimensionValues
        {
            get
            {
                if (mFiveDimensionValues == null)
                {
                    mFiveDimensionValues = new List<CultureFiveDimension>();
                }
                return mFiveDimensionValues;
            }
            set
            {
                mFiveDimensionValues = value;
                RaisePropertyChanged("FiveDimensionValues");
            }
        }

        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="FiveDimensionViewModel"/> class.
        /// </summary>
        /// <param name="FiveDimensionModel">The five dimension model.</param>
        [ImportingConstructor]
        public FiveDimensionViewModel(IFiveDimensionModel FiveDimensionModel)
        {
            mFiveDimensionModel = FiveDimensionModel;

            IsUserCultureNotDefined = !CultureAppConfigurations.CurrentLoginUser.CultureID.HasValue;

            #region → Set up event handling       .
            mFiveDimensionModel.GetConversationCultureComplete += new EventHandler<eNegEntityResultArgs<ConversationCulture>>(mFiveDimensionModel_GetConversationCultureComplete);
            mFiveDimensionModel.GetConversationPartnerCultureComplete += new Action<InvokeOperation<int>>(mFiveDimensionModel_GetConversationPartnerCultureComplete);
            mFiveDimensionModel.GetCultureComplete += new EventHandler<eNegEntityResultArgs<Culture>>(mFiveDimensionModel_GetCultureComplete);
            mFiveDimensionModel.GetCultureFiveDimensionComplete += new EventHandler<eNegEntityResultArgs<CultureFiveDimension>>(mFiveDimensionModel_GetCultureFiveDimensionComplete);
            mFiveDimensionModel.GetNegotiationCultureComplete += new EventHandler<eNegEntityResultArgs<NegotiationCulture>>(mFiveDimensionModel_GetNegotiationCultureComplete);
            mFiveDimensionModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mFiveDimensionModel_PropertyChanged);
            mFiveDimensionModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mFiveDimensionModel_SaveChangesComplete);
            #endregion

            #region → Load Lookup tables          .
            GetCultureAsync();
            GetNegotiationCultureAsync();
            #endregion

            #region → Register Messages           .
            // register for SubmitChangesMessage
            CultureAppMessanger.SubmitChangesMessage.Register(this, OnSubmitChanges);
            #endregion Register Messages

        }
        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Ms the five dimension model_ get culture complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mFiveDimensionModel_GetCultureComplete(object sender, eNegEntityResultArgs<Culture> e)
        {
            if (!e.HasError)
            {
                CultureEntries = new List<Culture>(e.Results.OrderBy(g => g.CultureName));

                AdjustCulturesName();

                #region → Define which view will appear at the begining                         .
                if (CultureAppConfigurations.ConversationIDParameter == Guid.Empty)
                {
                    CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.NegotiationCultureView);
                }
                #endregion
            }
            else
            {
                // notify user if there is any error
                CultureAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the five dimension model_ get negotiation culture complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mFiveDimensionModel_GetNegotiationCultureComplete(object sender, eNegEntityResultArgs<NegotiationCulture> e)
        {
            if (!e.HasError)
            {
                #region → If found a record for the negotiation ID given, then initialize some flages used in binding.
                if (e.Results.Count() > 0)
                {
                    CurrentNegotiationCulture = e.Results.First();
                    IsAlways = CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.Always;
                    IsAskMe = CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.AskMe;
                    IsTryToRecognize = CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.TryToRecognize;
                }
                #endregion

                #region → If this is the first time to visit this negotiation    .
                else
                {
                    CurrentNegotiationCulture = mFiveDimensionModel.AddNegotiationCulture(CultureAppConfigurations.NegotiationIDParameter, true);
                    IsAskMe = true;
                }
                #endregion

                if (CultureAppConfigurations.ConversationIDParameter != Guid.Empty)
                {
                    //Try to load conversation cultures if conversation ID is not empty
                    GetConversationCultureAsync();
                }
                else
                {
                    CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.NegotiationCultureView);
                }
            }
            else
            {
                // notify user if there is any error
                CultureAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the five dimension model_ get conversation culture complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mFiveDimensionModel_GetConversationCultureComplete(object sender, eNegEntityResultArgs<ConversationCulture> e)
        {
            if (!e.HasError)
            {
                #region → If found a record for the conversation ID given, so get five dimensions values to can navigate to draw the chart.
                if (e.Results.Count() > 0)
                {
                    CurrentConversationCulture = e.Results.First();

                    CultureAppConfigurations.PartnerCultureID = CurrentConversationCulture.PartnerCultureID.Value;

                    if (CultureAppConfigurations.CurrentLoginUser.CultureID.HasValue)
                    {
                        GetCultureFiveDimensionAsync();
                        return;
                    }
                }
                #endregion

                #region → If the call of getConversation culture happened, but with not an empty guid, then add a new conversation according to negoiation culture type.
                else if (CultureAppConfigurations.ConversationIDParameter != Guid.Empty)
                {
                    this.CurrentConversationCulture = mFiveDimensionModel.AddConversationCulture(CultureAppConfigurations.ConversationIDParameter, true);

                    if (CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.TryToRecognize)
                    {
                        //try to recognize partner culture through splitting his email address 
                        // and getting email domain to find relative culture.
                        GetConversationPartnerCultureAsync();
                        return;
                    }

                    if (CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.Always)
                    {
                        this.CurrentConversationCulture.PartnerCultureID = CurrentNegotiationCulture.DefaultCultureID;
                        CultureAppConfigurations.PartnerCultureID = CurrentNegotiationCulture.DefaultCultureID.Value;
                        CultureAppMessanger.SubmitChangesMessage.Send();
                        return;
                    }
                }
                #endregion

                #region → Navigate to the relative view depending on founding user & partner culture or not founding any of them  .

                //Hack:....
                if (CultureAppConfigurations.CurrentLoginUser.CultureID.HasValue && this.CurrentConversationCulture.PartnerCultureID.HasValue)
                {
                    CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
                }
                else
                {
                    CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
                }
                #endregion
            }
            else
            {
                // notify user if there is any error
                CultureAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the five dimension model_ get conversation partner culture complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mFiveDimensionModel_GetConversationPartnerCultureComplete(InvokeOperation<int> obj)
        {
            if (!obj.HasError)
            {
                if (obj.Value != 0)
                {
                    CurrentConversationCulture.PartnerCultureID = obj.Value;
                    CultureAppConfigurations.PartnerCultureID = obj.Value;

                    #region → Get partner culture and if found in addition to user culture, so it naviagtes to draw the chart view .
                    if (CultureAppConfigurations.CurrentLoginUser.CultureID.HasValue && this.CurrentConversationCulture.PartnerCultureID.HasValue)
                    {
                        CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
                        CultureAppMessanger.SubmitChangesMessage.Send();
                        return;
                    }
                    #endregion
                }

                CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
            }
            else
            {
                // notify user if there is any error
                CultureAppMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Ms the five dimension model_ get culture five dimension complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mFiveDimensionModel_GetCultureFiveDimensionComplete(object sender, eNegEntityResultArgs<CultureFiveDimension> e)
        {
            if (!e.HasError)
            {
                FiveDimensionValues = e.Results.AsEnumerable();

                if (CultureAppConfigurations.ConversationIDParameter != Guid.Empty)
                {
                    #region → generate data series responsible for drawing the chart data points               .
                    this.CurrentPartnerCulture = this.CultureEntries.Where(ss => ss.CultureID == this.CurrentConversationCulture.PartnerCultureID).FirstOrDefault();

                    FiveDimensionValuesDataSeries = CultureGraphSeries.GenerateDataSeries(FiveDimensionValues);
                    CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
                    #endregion

                    #region → Send Statistical message to eNeg with the new values for user & partner cultures .
                    if (NeedToSendAppsStatisticalsMessages)
                    {
                        Culture userCulture = this.CultureEntries.Where(s => s.CultureID == CurrentUserCulture.CultureID).First();
                        Culture partnerCulture = this.CultureEntries.Where(s => s.CultureID == CurrentPartnerCulture.CultureID).First();

                        string MessageBody = CultureGraphSeries.GenerateeNegMessage
                            (FiveDimensionValuesDataSeries, userCulture, partnerCulture);
                        mFiveDimensionModel.SendAppsStatisticalsMessages(CurrentConversationCulture.ConversationID.Value, MessageBody);
                        NeedToSendAppsStatisticalsMessages = false;
                    }
                    #endregion
                }
            }
            else
            {
                // notify user if there is any error
                CultureAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the mFiveDimensionModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mFiveDimensionModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = this.mFiveDimensionModel.IsBusy;

                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the SaveChangesComplete event of the mFiveDimensionModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eNeg.Common.SubmitOperationEventArgs"/> instance containing the event data.</param>
        private void mFiveDimensionModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                #region → Update user culture if it changed from it last values coming from eNeg when login in App .
                if (CurrentUserCulture != null && CultureAppConfigurations.CurrentLoginUser.CultureID != CurrentUserCulture.CultureID)
                {
                    CultureAppConfigurations.CurrentLoginUser.CultureID = CurrentUserCulture.CultureID;
                    mFiveDimensionModel.UpdateUserCultureAsync();
                    IsUserCultureNotDefined = false;
                }
                #endregion

                #region → Mark a flag "NeedToSendAppsStatisticalsMessages" as true to send support message to eneg with cultures 5 dimensios values .
                if (e.SubmitOp.ChangeSet.AddedEntities.Where(s => s.GetType().Equals(typeof(ConversationCulture))).Count() > 0 ||
                    e.SubmitOp.ChangeSet.ModifiedEntities.Where(s => s.GetType().Equals(typeof(ConversationCulture))).Count() > 0)
                {
                    CultureAppConfigurations.PartnerCultureID = this.CurrentConversationCulture.PartnerCultureID.Value;
                    NeedToSendAppsStatisticalsMessages = true;
                }
                #endregion

                #region → If user & partner cultures are defined, then load cultureFiveDimension values to draw chart .
                PlotChart();
                #endregion

                this.RaiseCanExecuteChanged();
            }
            else
            {
                // notify user if there is any error
                CultureAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        /// <value>The submit change command.</value>
        public RelayCommand SubmitChangeCommand
        {
            get
            {
                if (mSubmitChangeCommand == null)
                {
                    mSubmitChangeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            #region → Validate user culture and partner culture in case of Always is .
                            if (!this.IsValid())
                                return;

                            #endregion

                            #region → Submit changes if this command raised due to changes in context, then in callback update user culture if reuired .
                            if (this.mFiveDimensionModel.HasChanges)
                            {
                                CultureAppMessanger.SubmitChangesMessage.Send();
                            }
                            #endregion

                            #region → Update user culture if reuired                                 .
                            else if (CultureAppConfigurations.CurrentLoginUser.CultureID != CurrentUserCulture.CultureID)
                            {
                                CultureAppConfigurations.CurrentLoginUser.CultureID = CurrentUserCulture.CultureID;
                                mFiveDimensionModel.UpdateUserCultureAsync();
                                PlotChart();
                                this.IsUserCultureNotDefined = false;
                                this.RaiseCanExecuteChanged();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            CultureAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => (this.mFiveDimensionModel.HasChanges ||
                        (CurrentUserCulture != null &&
                        CultureAppConfigurations.CurrentLoginUser.CultureID != CurrentUserCulture.CultureID)) &&
                        !this.IsBusy);
                }
                return mSubmitChangeCommand;
            }
        }

        /// <summary>
        /// Gets the back to graph command.
        /// </summary>
        /// <value>The back to graph command.</value>
        public RelayCommand BackToGraphCommand
        {
            get
            {
                if (mBackToGraphCommand == null)
                {
                    mBackToGraphCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mFiveDimensionModel.IsBusy)
                            {
                                CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            CultureAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mBackToGraphCommand;
            }
        }

        /// <summary>
        /// Gets the culture selection changed command.
        /// </summary>
        /// <value>The culture selection changed command.</value>
        public RelayCommand CultureSelectionChangedCommand
        {
            get
            {
                if (mCultureSelectionChangedCommand == null)
                {
                    mCultureSelectionChangedCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mFiveDimensionModel.IsBusy)
                            {
                                this.ClearValid();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            CultureAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mCultureSelectionChangedCommand;
            }
        }
        /// <summary>
        /// Gets the back to graph command.
        /// </summary>
        /// <value>The back to graph command.</value>
        public RelayCommand NavigateToHelpCommand
        {
            get
            {
                if (mNavigateToHelpCommand == null)
                {
                    mNavigateToHelpCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mFiveDimensionModel.IsBusy)
                            {
                                CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.HelpView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            CultureAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mNavigateToHelpCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [submit changes].
        /// </summary>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        private void OnSubmitChanges(Boolean flag)
        {
            this.mFiveDimensionModel.SaveChangesAsync();
        }

        /// <summary>
        /// Plots the chart.
        /// </summary>
        private void PlotChart()
        {
            if (CultureAppConfigurations.PartnerCultureID != 0 &&
                CultureAppConfigurations.CurrentLoginUser.CultureID != null)
            {
                CultureAppMessanger.ChangeScreenMessage.Send(CultureAppViewTypes.ConversationCultureGraphView);
                GetCultureFiveDimensionAsync();
            }
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            this.SubmitChangeCommand.RaiseCanExecuteChanged();
            this.BackToGraphCommand.RaiseCanExecuteChanged();
            this.NavigateToHelpCommand.RaiseCanExecuteChanged();

            this.CanExcuteBackToCommand = this.BackToGraphCommand.CanExecute(null);
        }

        /// <summary>
        /// Adjusts the name of the cultures.
        /// </summary>
        private void AdjustCulturesName()
        {
            if (!IsUserCultureNotDefined)
            {
                CurrentUserCulture = CultureEntries.Where(s => s.CultureID == CultureAppConfigurations.CurrentLoginUser.CultureID.Value).FirstOrDefault();
            }

            if (this.CurrentConversationCulture != null && this.CurrentConversationCulture.PartnerCultureID.HasValue)
            {
                this.CurrentPartnerCulture = this.CultureEntries.Where(ss => ss.CultureID == this.CurrentConversationCulture.PartnerCultureID).FirstOrDefault();
            }
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValid()
        {
            var isValid = true;

            CurrentNegotiationCulture.ValidationErrors.Clear();

            if (CurrentConversationCulture != null)
            {
                CurrentConversationCulture.ValidationErrors.Clear();
            }

            if (CurrentUserCulture == null || CurrentUserCulture.CultureID == 0)
            {
                CurrentUserCulture = CurrentUserCulture ?? new Culture();
                CurrentUserCulture.ValidationErrors.Add(new ValidationResult(Resources.UserCultureRequired));
                isValid = false;
            }


            if (CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.Always &&
                (CurrentNegotiationCulture.DefaultCultureID == null || CurrentNegotiationCulture.DefaultCultureID == 0))
            {
                CurrentNegotiationCulture.ValidationErrors.Add(new ValidationResult(Resources.PartnerCultureRequiredIsAlways, new string[] { "DefaultCultureID" }));
                isValid = false;
            }

            if (CultureAppConfigurations.ConversationIDParameter != Guid.Empty &&
                CurrentConversationCulture != null &&
                (CurrentConversationCulture.PartnerCultureID == null || CurrentConversationCulture.PartnerCultureID == 0))
            {
                CurrentConversationCulture.ValidationErrors.Add(new ValidationResult(Resources.PartnerCultureRequired, new string[] { "PartnerCultureID" }));
                isValid = false;
            }

            return isValid;
        }


        /// <summary>
        /// Clears the valid.
        /// </summary>
        private void ClearValid()
        {
            if (CurrentUserCulture != null && CurrentUserCulture.CultureID != 0)
            {
                CurrentUserCulture.ValidationErrors.Clear();
            }

            if (!(CurrentNegotiationCulture.NegotiationCultureType == CultureAppConstant.NegotiationCultureTypes.Always &&
                (CurrentNegotiationCulture.DefaultCultureID == null || CurrentNegotiationCulture.DefaultCultureID == 0)))
            {
                var validationResult = CurrentNegotiationCulture.ValidationErrors.Where(s => s.MemberNames.Contains("DefaultCultureID")).FirstOrDefault();

                if (validationResult != null)
                {
                    CurrentNegotiationCulture.ValidationErrors.Remove(validationResult);
                }
            }

            if (CurrentConversationCulture != null)
            {
                if (!(CultureAppConfigurations.ConversationIDParameter != Guid.Empty &&
                    (CurrentConversationCulture.PartnerCultureID == null || CurrentConversationCulture.PartnerCultureID == 0)))
                {
                    var validationResult = CurrentConversationCulture.ValidationErrors.Where(s => s.MemberNames.Contains("PartnerCultureID")).FirstOrDefault();

                    if (validationResult != null)
                    {
                        CurrentConversationCulture.ValidationErrors.Remove(validationResult);
                    }
                }
            }
        }


        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the culture async.
        /// </summary>
        public void GetCultureAsync()
        {
            mFiveDimensionModel.GetCultureAsync();
        }

        /// <summary>
        /// Gets the negotiation culture async.
        /// </summary>
        public void GetNegotiationCultureAsync()
        {
            mFiveDimensionModel.GetNegotiationCultureAsync();
        }

        /// <summary>
        /// Gets the conversation culture async.
        /// </summary>
        public void GetConversationCultureAsync()
        {
            mFiveDimensionModel.GetConversationCultureAsync();
        }

        /// <summary>
        /// Gets the conversation partner culture async.
        /// </summary>
        public void GetConversationPartnerCultureAsync()
        {
            mFiveDimensionModel.GetConversationPartnerCultureAsync();
        }

        /// <summary>
        /// Gets the culture five dimension async.
        /// </summary>
        public void GetCultureFiveDimensionAsync()
        {
            mFiveDimensionModel.GetCultureFiveDimensionAsync();
        }

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            RunQueueForApplyChanges = true;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (!this.IsBusy)
                {
                    CultureAppConfigurations.PartnerCultureID = 0;

                    IsUserCultureNotDefined = !CultureAppConfigurations.CurrentLoginUser.CultureID.HasValue;

                    RunQueueForApplyChanges = false;

                    this.mFiveDimensionModel.RejectChanges();

                    this.CurrentUserCulture = null;

                    this.CurrentConversationCulture = null;

                    this.AdjustCulturesName();

                    FiveDimensionValuesDataSeries = new List<FiveDimensionData>();

                    this.GetNegotiationCultureAsync();
                }

            });

        }

        #endregion

        #endregion
    }
}