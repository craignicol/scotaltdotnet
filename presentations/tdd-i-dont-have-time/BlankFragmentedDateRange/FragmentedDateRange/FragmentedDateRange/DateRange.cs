using System;

namespace scotalt.BusinessObjects
{
    /// <summary>
    /// Repository for commonly used string formats
    /// </summary>
    public class CommonFormats
    {
        public const string ShortDate = "dd/MM/yyyy";
    }


    public class DateRange
    {
        #region private members

        private DateTime? _fromDate = null;
        private DateTime? _toDate = null;

        #endregion

        #region properties

        //Default constructor
        public DateRange() { }

        public DateRange(DateTime? from, DateTime? to)
        {
            FromDate = from;
            ToDate = to;
        }

        /// <summary>
        /// The From date of the date range
        /// </summary>
        public DateTime? FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        /// <summary>
        /// The To date of the date range
        /// </summary>
        public DateTime? ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }



        override public string ToString()
        {
            return ToString( CommonFormats.ShortDate );
        }

        public string ToString(string dateFormat)
        {
            if ( ToDate == null && FromDate == null )
            {
                return "any date";
            }
            if ( ToDate == null )
            {
                return String.Format( "on or after {0}", FromDate.Value.ToString( dateFormat ) );
            }
            if ( FromDate == null )
            {
                return String.Format( "on or before {0}", ToDate.Value.ToString( dateFormat ) );
            }
            return String.Format( "between {0} and {1} inclusive", FromDate.Value.ToString( dateFormat ), ToDate.Value.ToString( dateFormat ) );

        }


        /// <summary>
        /// Returns true if testDate is between the fromDate and toDate (inclusive)
        /// </summary>
        /// <param name="testDate"></param>
        /// <returns></returns>
        public bool Contains(DateTime testDate)
        {
            return (FromDate == null || testDate >= FromDate) && (ToDate == null || testDate <= ToDate);
        }

        static public DateTime? DateMin(DateTime? x, DateTime? y)
        {
            if ( x == null )
                return y;
            if ( y == null )
                return x;
            if ( x.Value < y.Value )
                return x;
            else
                return y;
        }

        static public DateTime? DateMax(DateTime? x, DateTime? y)
        {
            if ( x == null )
                return y;
            if ( y == null )
                return x;
            if ( x.Value > y.Value )
                return x;
            else
                return y;
        }

        static public DateRange intersect(DateRange x, DateRange y)
        {
            DateRange result = new DateRange( DateMax( x.FromDate, y.FromDate ), DateMin( x.ToDate, y.ToDate ) );
            if ( result.FromDate > result.ToDate )
                return null;
            else
                return result;
        }

        #endregion
    }
}
