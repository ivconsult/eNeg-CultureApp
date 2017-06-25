
#region → Usings   .
using System;

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

namespace citPOINT.CultureApp.Common
{
    /// <summary>
    /// Constant for All Tables (Lockup Tables)
    /// </summary>
    public static class CultureAppConstant
    {
        #region  →  Negotiation Culture Types     .

        /// <summary>
        /// Used to return relative tiny int values coreesponding to certain choice
        /// </summary>
        public class NegotiationCultureTypes
        {
            #region → Fields         .

            private static byte mAlways = 0;
            private static byte mTryToRecognize = 1;
            private static byte mAskMe = 2;

            #endregion  Fields

            #region → Properties     .
            
            /// <summary>
            /// always set cultue to.
            /// </summary>
            public static byte Always
            {
                get
                {
                    return mAlways;
                }
            }

            /// <summary>
            /// try to auto-recognize it and only aks me if unknown
            /// </summary>
            public static byte TryToRecognize
            {
                get
                {
                    return mTryToRecognize;
                }

            }

            /// <summary>
            /// always ask me
            /// </summary>
            public static byte AskMe
            {
                get
                {
                    return mAskMe;
                }
            }

            #endregion Properties
        }
        #endregion
    }
}
