using System;
using System.Collections.Generic;
using System.Linq;

namespace scotalt.BusinessObjects
{
    /// <summary>
    /// A FragmentedDateRange allows gap between the from date and to date.
    /// It is a list of DateRange objects. The presence of a DateRange in
    /// the list indicates the range applies to those dates.
    /// </summary>
    public class FragmentedDateRange
    {
        public DateTime? From
        {
            get
            {
                if ( Range.Count > 0 )
                {
                    return Range[0].FromDate;
                }
                return null;
            }
        }

        public DateTime? To
        {
            get
            {
                if ( Range.Count > 0 )
                {
                    return Range[Range.Count - 1].ToDate;
                }
                return null;
            }
        }

        public IList<DateRange> Range
        {
            get;
            protected set;
        }

        public FragmentedDateRange()
        {
            Range = new List<DateRange>();
        }

        protected FragmentedDateRange(IList<DateRange> range)
        {
            Range = range;
        }

        private IList<DateRange> SubRangeList(DateRange select, bool inclusive)
        {
            if ( Range.Count == 0 || (select.FromDate == null && select.ToDate == null) )
            {
                return Range;
            }
            else if ( select.FromDate == null )
            {
                if ( inclusive )
                {
                    return Range.Where( range => range.FromDate <= select.ToDate ).ToList();
                }
                else
                {
                    return Range.Where( range => range.ToDate <= select.ToDate ).ToList();
                }
            }
            else if ( select.ToDate == null )
            {
                if ( inclusive )
                {
                    return Range.Where( range => range.ToDate >= select.FromDate ).ToList();
                }
                else
                {
                    return Range.Where( range => range.FromDate >= select.FromDate ).ToList();
                }
            }
            else
            {
                if ( inclusive )
                {
                    return Range.Where( range => range.FromDate <= select.ToDate
                                                                            && range.ToDate >= select.FromDate ).ToList();
                }
                else
                {
                    return Range.Where( range => range.ToDate <= select.ToDate
                                                                            && range.FromDate >= select.FromDate ).ToList();
                }
            }
        }
        /// <summary>
        /// Fins the sub range of the current range that overlaps the given range
        /// </summary>
        /// <param name="select">the range to search</param>
        /// <param name="inclusive">include ranges that partially overlap the given range</param>
        /// <returns></returns>
        public FragmentedDateRange SubRange(DateRange select, bool inclusive)
        {
            return new FragmentedDateRange( SubRangeList( select, inclusive ) );
        }

        public void Insert(DateRange toAdd)
        {
            if ( Range.Count == 0 )
            {
                Range.Add( toAdd );
            }
            else
            {
                IList<DateRange> newRange = SubRangeList( new DateRange( null, toAdd.FromDate ), false );

                FragmentedDateRange midRange = SubRange( new DateRange( toAdd.FromDate, toAdd.ToDate ), true );
                DateTime? newFrom = midRange.From < toAdd.FromDate ? midRange.From : toAdd.FromDate;
                DateTime? newTo = midRange.To > toAdd.ToDate ? midRange.To : toAdd.ToDate;
                newRange.Add( new DateRange( newFrom, newTo ) );

                newRange.Concat( SubRangeList( new DateRange( toAdd.ToDate, null ), false ) );

                Range = newRange;
            }
        }

        public void Remove(DateRange toRemove)
        {
            if ( Range.Count == 0 )
            {
                return;
            }
            else
            {
                IList<DateRange> newRange = SubRangeList( new DateRange( null, toRemove.FromDate ), false );

                FragmentedDateRange midRange = SubRange( new DateRange( toRemove.FromDate, toRemove.ToDate ), true );
                if ( midRange.From < toRemove.FromDate )
                {
                    newRange.Add( new DateRange( midRange.From, toRemove.FromDate ) );
                }
                if ( toRemove.ToDate < midRange.To )
                {
                    newRange.Add( new DateRange( toRemove.ToDate, midRange.To ) );
                }

                newRange.Concat( SubRangeList( new DateRange( toRemove.ToDate, null ), false ) );

                Range = newRange;
            }
        }
    }
}
