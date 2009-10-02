namespace ScotAltNet.Kata
{
    using System;
    using System.Collections.Generic;

    public enum Roman
    {
     I = 1,
     V = 5,
     X = 10,
     L = 50,
     C = 100,
     D = 500,
     M = 1000
    }

    public static class NumberConverter
    {
      

        public static string ConvertToUrnfield(int i)
        {
            double remainder = GetRemainder(i);
            int wholeNumber = (int)Math.Floor(i/5d);
            return Pad(remainder, "/") + Pad(wholeNumber, @"\");
        }

        public static double GetRemainder(int i)
        {
            return i%5;
        }

        public static string Pad(double r, string padChar)
        {
            string res = "";
            
            for (int j = 0; j < r; j++)
            {
                res += padChar;
            }
            return res;
        }

        public static string ConvertToRoman(int arabic)
        {
            int[] bucket = GetIntArrayFromNumber(arabic);
            Array.Reverse(bucket);

            string result = "";
            string[] powersOfTen = new[] {"I", "X", "C", "M"};
            string[] powersOfFive = new[] { "V", "L", "D"};
            
            for (int i = 0; i < bucket.Length; i++ )
            {
                string tResult = "";
                if (bucket[i] < 4)
                    tResult = Pad(bucket[i], powersOfTen[i]);

                if (bucket[i] >= 5 && bucket[i] < 9)
                    tResult = powersOfFive[i] + Pad(bucket[i]%5, powersOfTen[i]);

                if (bucket[i] == 9)
                    tResult = powersOfTen[i] + powersOfTen[i + 1];

                if(bucket[i] == 4)
                {
                    tResult = powersOfTen[i] + powersOfFive[i]; 
                }

                result = tResult + result;
            }

            return result;

        }



        public static int[] GetIntArrayFromNumber(int s)
        {
            List<int> i = new List<int>();
            foreach(char c in s.ToString())
            {
                i.Add(int.Parse(c.ToString()));
            }
            return i.ToArray();
        }
    }
}