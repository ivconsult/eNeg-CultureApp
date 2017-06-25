#region → Usings   .
using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.CultureApp.Common;
using citPOINT.eNeg.Data.Web.Test;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.08.11     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion
namespace citPOINT.CultureApp.Data.Web.Test
{
    /// <summary>
    /// Class for testing [Insert - Update - Delete] 
    /// operations for CultureApp Database
    /// </summary>
    [TestClass]
    public class CultureAppServiceTest
    {

        #region → Fields         .
        CultureAppContext mContext;
        List<NegotiationCulture> mNegotiationCultures;
        List<ConversationCulture> mConversationCultures;
        List<CultureFiveDimension> mCultureFiveDimensions;

        int CountOfAllEntries = 0;
        private TestContext testContextInstance;
        #endregion

        #region → Properties     .

        #region Object Count

        /// <summary>
        /// Get Count of NegotiationCultures
        /// </summary>
        public int NegotiationCulturesCount
        {
            get
            {
                return this.NegotiationCultures.Count;
            }
        }

        /// <summary>
        /// Get Count of ConversationCultures
        /// </summary>
        public int ConversationCulturesCount
        {
            get
            {
                return this.ConversationCultures.Count;
            }
        }

        #endregion

        #region Mock Objects

        #region <5> CultureFiveDimensions

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
                        new CultureFiveDimension()
                        {
                            CultureID = 1,
                            PDI = 28,
                            IDV = 55,
                            MAS = 30,
                            UAI = 75,
                            LTO = 10
                        },
                        new CultureFiveDimension()
                        {
                            CultureID = 2,
                            PDI = 15,
                            IDV = 30,
                            MAS = 85,
                            UAI = 12,
                            LTO = 90
                        },
                        new CultureFiveDimension()
                        {
                            CultureID = 3,
                            PDI = 70,
                            IDV = 34,
                            MAS = 78,
                            UAI = 14,
                            LTO = 4
                        },
                        new CultureFiveDimension()
                        {
                            CultureID = 4,
                            PDI = 36,
                            IDV = 78,
                            MAS = 18,
                            UAI = 29,
                            LTO = 58
                        },
                        new CultureFiveDimension()
                        {
                            CultureID = 5,
                            PDI = 47,
                            IDV = 35,
                            MAS = 11,
                            UAI = 94,
                            LTO = 55
                        }
                    };
                }
                return mCultureFiveDimensions;
            }
        }
        #endregion

        #region <3> NegotiationCultures
        /// <summary>
        /// Gets or sets the negotiation cultures.
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
                        new NegotiationCulture()
                        {
                            NegotiationCultureID = Guid.NewGuid(),
                            NegotiationID = Guid.NewGuid(),
                            NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.Always,
                            DefaultCultureID = CultureFiveDimensions[0].CultureID,
                            Deleted = false,
                            DeletedBy = Guid.NewGuid(),
                            DeletedOn = DateTime.Now
                        },
                        new NegotiationCulture()
                        {
                            NegotiationCultureID = Guid.NewGuid(),
                            NegotiationID = Guid.NewGuid(),
                            NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.Always,
                            DefaultCultureID = CultureFiveDimensions[1].CultureID,
                            Deleted = false,
                            DeletedBy = Guid.NewGuid(),
                            DeletedOn = DateTime.Now
                        },
                        new NegotiationCulture()
                        {
                            NegotiationCultureID = Guid.NewGuid(),
                            NegotiationID = Guid.NewGuid(),
                            NegotiationCultureType = CultureAppConstant.NegotiationCultureTypes.Always,
                            DefaultCultureID = CultureFiveDimensions[2].CultureID,
                            Deleted = false,
                            DeletedBy = Guid.NewGuid(),
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mNegotiationCultures;
            }
        }
        #endregion

        #region <3> ConversationCultures
        /// <summary>
        /// Gets or sets the negotiation cultures.
        /// </summary>
        /// <value>The negotiation cultures.</value>
        public List<ConversationCulture> ConversationCultures
        {
            get
            {
                if (mConversationCultures == null)
                {
                    mConversationCultures = new List<ConversationCulture>()
                    {
                        new ConversationCulture()
                        {
                            ConversationCultureID = Guid.NewGuid(),
                            ConversationID = Guid.NewGuid(),
                            PartnerCultureID = CultureFiveDimensions[0].CultureID,
                            Deleted = false,
                            DeletedBy = Guid.NewGuid(),
                            DeletedOn = DateTime.Now
                        },
                        new ConversationCulture()
                        {
                            ConversationCultureID = Guid.NewGuid(),
                            ConversationID = Guid.NewGuid(),
                            PartnerCultureID = CultureFiveDimensions[1].CultureID,
                            Deleted = false,
                            DeletedBy = Guid.NewGuid(),
                            DeletedOn = DateTime.Now
                        },
                        new ConversationCulture()
                        {
                            ConversationCultureID = Guid.NewGuid(),
                            ConversationID = Guid.NewGuid(),
                            PartnerCultureID = CultureFiveDimensions[2].CultureID,
                            Deleted = false,
                            DeletedBy = Guid.NewGuid(),
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mConversationCultures;
            }
        }
        #endregion

        #endregion

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
                    mContext = new CultureAppContext(new Uri("http://localhost:9003/citPOINT-CultureApp-Data-Web-CultureAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion Properties

        #region → Constructor    .
        public CultureAppServiceTest()
        {
            CountOfAllEntries = NegotiationCulturesCount +
                ConversationCulturesCount;
        }
        #endregion

        #region → Methods        .

        #region Test Insert All Entries
        /// <summary>
        ///A test for Insert All Entries
        ///</summary>
        [TestMethod]
        [Description(@"Test Insert Operations for all entries 
            (NegotiationCulture - ConversationCulture)")]
        public void InsertAllEntries()
        {
            try
            {
                foreach (var item in NegotiationCultures)
                {
                    this.Context.NegotiationCultures.Add(item);
                }

                foreach (var item in ConversationCultures)
                {
                    this.Context.ConversationCultures.Add(item);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(InsertAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntries", ex);
            }
        }

        /// <summary>
        /// Inserts all entries complete.
        /// </summary>
        /// <param name="subOp">The sub op.</param>
        private void InsertAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    UpdateAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        #region Test Update All Entries

        /// <summary>
        ///A test for Update All Entries
        ///</summary>
        public void UpdateAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                foreach (var item in this.Context.NegotiationCultures)
                {
                    item.DefaultCultureID = CultureFiveDimensions[4].CultureID;
                }

                foreach (var item in this.Context.ConversationCultures)
                {
                    item.PartnerCultureID = CultureFiveDimensions[4].CultureID;
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(UpdateAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntries", ex);
            }
        }


        /// <summary>
        /// Event Complete of  Update All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        private void UpdateAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", "Number of Records updated is not right.");
                }
                else
                {
                    DeleteAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", subOp.Error);
            }
        }
        #endregion

        #region Test Delete All Entries


        /// <summary>
        ///A test for Delete All Entries
        ///only for Delete Flag
        ///</summary>
        public void DeleteAllEntries()
        {
            try
            {
                this.Context.RejectChanges();
                while (this.ConversationCultures.Count > 0)
                {
                    this.Context.ConversationCultures.Remove(this.ConversationCultures[0]);
                    this.ConversationCultures.RemoveAt(0);
                }

                while (this.NegotiationCultures.Count > 0)
                {
                    this.Context.NegotiationCultures.Remove(this.NegotiationCultures[0]);
                    this.NegotiationCultures.RemoveAt(0);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(DeleteAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Delete All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        void DeleteAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    eNegMessageBox.ShowMessageBox(true, "Inset - Update - Delete All Entries ", DeleteString);
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", subOp.Error);
            }
        }
        #endregion

        /// <summary>
        /// get SQL Statement to Clear Database
        /// </summary>
        private string DeleteString
        {
            get
            {
                return @"
---------------------------------------------------
You must run these SQL commands Before retest again
---------------------------------------------------

DELETE [ConversationCulture];
DELETE [NegotiationCulture];
";
            }
        }
        #endregion Methods
    }
}
