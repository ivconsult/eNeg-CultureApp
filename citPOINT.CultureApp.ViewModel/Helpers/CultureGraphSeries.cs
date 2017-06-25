
#region → Usings   .
using System.Collections.Generic;
using citPOINT.CultureApp.Data.Web;
using citPOINT.CultureApp.Common;
using System.Linq;
using System.Text;
using System;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 23.08.11     Y.Mohammed     • creation
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
    /// <summary>
    /// Custom static class used as a helper to generate data points 
    /// will be drawn on Five dimenssion graph
    /// </summary>
    public static class CultureGraphSeries
    {
        /// <summary>
        /// Generates the specified five dimension values.
        /// </summary>
        /// <param name="FiveDimensionValues">The five dimension values.</param>
        /// <returns></returns>
        public static List<FiveDimensionData> GenerateDataSeries(IEnumerable<CultureFiveDimension> FiveDimensionValues)
        {
            List<FiveDimensionData> Data = new List<FiveDimensionData>();

            var tempUserData = FiveDimensionValues.Where(s => s.CultureID == CultureAppConfigurations.CurrentLoginUser.CultureID).First();
            var tempPartnerData = FiveDimensionValues.Where(s => s.CultureID == CultureAppConfigurations.PartnerCultureID).First();

            FiveDimensionData DataPoint = new FiveDimensionData()
            {
                DimensionName = "PDI",
                Description = Resources.PDI_Description,
                UserCultureDimensionValue = tempUserData.PDI.Value,
                PartnerCultureDimensionValue = tempPartnerData.PDI.Value
            };
            Data.Add(DataPoint);

            DataPoint = new FiveDimensionData()
            {
                DimensionName = "IDV",
                Description = Resources.IDV_Description,
                UserCultureDimensionValue = tempUserData.IDV.Value,
                PartnerCultureDimensionValue = tempPartnerData.IDV.Value
            };
            Data.Add(DataPoint);

            DataPoint = new FiveDimensionData()
            {
                DimensionName = "MAS",
                Description = Resources.MAS_Description,
                UserCultureDimensionValue = tempUserData.MAS.Value,
                PartnerCultureDimensionValue = tempPartnerData.MAS.Value
            };
            Data.Add(DataPoint);

            DataPoint = new FiveDimensionData()
            {
                DimensionName = "UAI",
                Description = Resources.UAI_Description,
                UserCultureDimensionValue = tempUserData.UAI.Value,
                PartnerCultureDimensionValue = tempPartnerData.UAI.Value
            };
            Data.Add(DataPoint);

            DataPoint = new FiveDimensionData()
            {
                DimensionName = "LTO",
                Description = Resources.LTO_Description,
                UserCultureDimensionValue = tempUserData.LTO.Value,
                PartnerCultureDimensionValue = tempPartnerData.LTO.Value
            };
            Data.Add(DataPoint);

            return Data;
        }

        public static string GenerateeNegMessage(List<FiveDimensionData> dimensionsValues, Culture userCulture, Culture partnerCulture)
        {
            StringBuilder sp = new StringBuilder();

            #region → Message Header                                        .

            sp.Append("Statistical message from Culture App:-");
            sp.Append(Environment.NewLine);
            sp.Append(Environment.NewLine);
            sp.Append("--------------------------------------------------");
            sp.Append(Environment.NewLine);
            #endregion

            #region → Define used cultures                                  .

            sp.Append("Your Culture is: ");
            sp.Append(userCulture.CultureName);
            sp.Append(Environment.NewLine);

            sp.Append("Your Partner Culture is: ");
            sp.Append(partnerCulture.CultureName);
            sp.Append(Environment.NewLine);

            #endregion

            #region → Five Dimesions values                                 .

            var PDI = dimensionsValues.Where(s => s.DimensionName == "PDI").First();
            var IDV = dimensionsValues.Where(s => s.DimensionName == "IDV").First();
            var MAS = dimensionsValues.Where(s => s.DimensionName == "MAS").First();
            var UAI = dimensionsValues.Where(s => s.DimensionName == "UAI").First();
            var LTO = dimensionsValues.Where(s => s.DimensionName == "LTO").First();

            sp.Append(string.Format("\r\tPDI : ({0} / {1}) {2}", PDI.UserCultureDimensionValue, PDI.PartnerCultureDimensionValue, Environment.NewLine));
            sp.Append(string.Format("\r\tIDV : ({0} / {1}) {2}", IDV.UserCultureDimensionValue, IDV.PartnerCultureDimensionValue, Environment.NewLine));
            sp.Append(string.Format("\r\tMAS : ({0} / {1}) {2}", MAS.UserCultureDimensionValue, MAS.PartnerCultureDimensionValue, Environment.NewLine));
            sp.Append(string.Format("\r\tUAI : ({0} / {1}) {2}", UAI.UserCultureDimensionValue, UAI.PartnerCultureDimensionValue, Environment.NewLine));
            sp.Append(string.Format("\r\tLTO : ({0} / {1}) {2}", LTO.UserCultureDimensionValue, LTO.PartnerCultureDimensionValue, Environment.NewLine));

            #endregion

            #region → Add hint to describe the values in prackets for user  .
            sp.Append(Environment.NewLine);
            sp.Append(Environment.NewLine);
            sp.Append("--------------------------------------------------");
            sp.Append(Environment.NewLine);
            sp.Append("Note: (user culture score / partner culture score)");
            #endregion

            return sp.ToString();
        }
    }
}
