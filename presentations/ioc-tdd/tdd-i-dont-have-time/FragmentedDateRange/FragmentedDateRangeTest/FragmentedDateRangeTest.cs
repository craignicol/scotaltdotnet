using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using scotalt.BusinessObjects;

namespace scotalt.BusinessObjects.Tests
{
    [TestFixture]
    public class FragmentedDateRangeTest
    {
        [SetUp]
        public void Setup()
        { }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public void EmptyFragmentedDateRange()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            Assert.AreEqual( 0, fdr.Range.Count );
        }

        [Test]
        public void AddToEmptyFragmentedDateRange()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            DateRange range = new DateRange(new DateTime(2008, 1, 1), new DateTime(2008, 6, 6));
            fdr.Insert( range );

            Assert.AreEqual( range.FromDate, fdr.From );
            Assert.AreEqual( range.ToDate, fdr.To );
        }

        [Test]
        public void AddUnboundedRangesToEmptyFragmentedDateRange()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            fdr.Insert( new DateRange( null, null ) );
            DateRange range = new DateRange( new DateTime( 2008, 1, 1 ), null );
            fdr.Insert( range );

            Assert.AreEqual( range.FromDate, fdr.From );
            Assert.AreEqual( range.ToDate, fdr.To );
        }


        [Test]
        public void AddTwoRangesToFragmentedDataRange()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            fdr.Insert( new DateRange( new DateTime( 2008, 1, 1 ), new DateTime( 2008, 3, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 4, 4 ), new DateTime( 2008, 6, 6 ) ) );

            Assert.AreEqual( new DateTime( 2008, 1, 1 ), fdr.From );
            Assert.AreEqual( new DateTime( 2008, 6, 6 ), fdr.To );
        }

        [Test]
        public void AddManyOverlappingRanges()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            fdr.Insert( new DateRange( new DateTime( 2008, 1, 1 ), new DateTime( 2008, 3, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 2, 2 ), new DateTime( 2008, 4, 4 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 3, 1 ), new DateTime( 2008, 5, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 4, 1 ), new DateTime( 2008, 6, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 5, 1 ), new DateTime( 2008, 7, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 6, 1 ), new DateTime( 2008, 8, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 7, 1 ), new DateTime( 2008, 9, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 8, 1 ), new DateTime( 2008, 10, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 9, 1 ), new DateTime( 2008, 11, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 10, 1 ), new DateTime( 2008, 12, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 11, 1 ), new DateTime( 2009, 1, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 12, 1 ), new DateTime( 2009, 2, 3 ) ) );

            Assert.AreEqual( 1, fdr.Range.Count );
            Assert.AreEqual( new DateTime( 2008, 1, 1 ), fdr.From );
            Assert.AreEqual( new DateTime( 2009, 2, 3 ), fdr.To );
        }

        [Test]
        public void RemoveDateRangeFromEmpty()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            fdr.Remove( new DateRange( new DateTime( 2008, 2, 1 ), new DateTime( 2008, 5, 1 ) ) );


            Assert.AreEqual( 0, fdr.Range.Count );
            Assert.AreEqual( null, fdr.From );
            Assert.AreEqual( null, fdr.To );

        }

        [Test]
        public void RemoveDateRange()
        {
            FragmentedDateRange fdr = new FragmentedDateRange();
            fdr.Insert( new DateRange( new DateTime( 2008, 1, 1 ), new DateTime( 2008, 3, 3 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 2, 2 ), new DateTime( 2008, 4, 4 ) ) );
            fdr.Insert( new DateRange( new DateTime( 2008, 3, 1 ), new DateTime( 2008, 5, 3 ) ) );

            fdr.Remove( new DateRange( new DateTime( 2008, 2, 1 ), new DateTime( 2008, 5, 1 ) ) );

            Assert.AreEqual( 2, fdr.Range.Count );
            Assert.AreEqual( new DateTime( 2008, 1, 1 ), fdr.From );
            Assert.AreEqual( new DateTime( 2008, 5, 3 ), fdr.To );
        }
    }
}
