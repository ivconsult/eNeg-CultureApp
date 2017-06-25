
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.CultureApp.Common;
using citPOINT.CultureApp.ViewModel;
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
    /// Five Dimension View Model Test class
    /// </summary>
    [TestClass]
    public class FiveDimensionViewModel_Test
    {
        #region → Fields         .
        private FiveDimensionViewModel FiveDimensionvm;
        private string ErrorMessage;
        #endregion

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        /// <value>The VM.</value>
        public FiveDimensionViewModel TheVM
        {
            get { return FiveDimensionvm; }
            set { FiveDimensionvm = value; }
        }
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="FiveDimensionViewModel_Test"/> class.
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {
            TheVM = new FiveDimensionViewModel(new MockFiveDimensionModel());

            #region → Registeration for needed messages in eNegMessenger
            // register for RaiseErrorMessage
            CultureAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message  .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;
                }
                else
                    ErrorMessage = ex.Message;
            }
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Gets the cultures_ without condition_ return collection.
        /// </summary>
        [TestMethod]
        public void GetCultures_WithoutCondition_ReturnCollection()
        {
            TheVM.GetCultureAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CultureEntries.Count() > 0, "No Culturies Found");
        }

        /// <summary>
        /// Gets the negotiation culture_ passing current negotiation I d_ initialize current negotiation culture.
        /// </summary>
        [TestMethod]
        public void GetNegotiationCulture_PassingCurrentNegotiationID_InitializeCurrentNegotiationCulture()
        {
            TheVM.GetNegotiationCultureAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentNegotiationCulture != null, "No Negotiation Culture Found");
        }

        /// <summary>
        /// Gets the conversation culture_ passing current conversation I d_ intialize current conversation culture.
        /// </summary>
        [TestMethod]
        public void GetConversationCulture_PassingCurrentConversationID_IntializeCurrentConversationCulture()
        {
            TheVM.GetConversationCultureAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentConversationCulture != null, "No Conversation Culture Found");
        }

        /// <summary>
        /// Gets the culture five dimension_ passing user and partner culture I DS_ return five dimension values.
        /// </summary>
        [TestMethod]
        public void GetCultureFiveDimension_PassingUserAndPartnerCultureIDs_ReturnFiveDimensionValues()
        {
            TheVM.GetCultureFiveDimensionAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.FiveDimensionValues.Count() > 0, "No Culturies Five Dimensions Found");
        }

        #endregion

        #endregion
    }
}
