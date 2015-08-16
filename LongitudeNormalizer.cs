using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISUtility
{
    public class LongitudeNormalizer
    {

        public static decimal NormalizeLongitude(decimal longitudeDD)
        {
            return Normalize(longitudeDD);
        }

        public static decimal NormalizeLongitude(decimal degrees, decimal minutes, decimal seconds)
        {
            return Normalize(ConvertToDD(degrees, minutes, seconds));
        }

        public static decimal ConvertToDD(decimal degrees, decimal minutes, decimal seconds)
        {
            minutes = minutes + (seconds / 60M);
            return degrees + (minutes / 60M);   
        }

        private static decimal Normalize(decimal longitude)
        {
            decimal retVal;

            //if the value is already between 180 and -180 just return it
            if ((longitude >= -180.0M) && (longitude <= 180.0M))
            {
                retVal = longitude;
            }
            else
            {
                // find the number of times the value is divisible by 180 and the remainder
                int multiple = (int)Math.Truncate( longitude / 180M);
                decimal remainder = longitude % 180;
                bool isOddMultiple = ((multiple % 2) != 0);
                bool isRemainderNeg = (remainder < 0);
                // the normalized value will be 180 minus the abosolute value of the remainder, 
                // if the multiple is odd, and 0 plus the remainder if the multiple is even
                // if the multiple is odd, we also need to check if the remainder is negative and multiply by -1 again
                if (isOddMultiple)
                    retVal = (180M - Math.Abs(remainder)) * (isOddMultiple ? -1M : 1M) * (isRemainderNeg ? -1M : 1M);
                else
                    retVal = (0 + remainder);
            }
            return retVal;
        }
    }

}
