using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Common
{
    public static class Functions
    {
        public static string APIPhysicalPath = string.Empty;
        public static string GetMessage(string Key)
        {
            //Assembly assembly = System.Reflection.Assembly.Load("DLMS.Common");
            //ResourceManager _resourceManager = new ResourceManager("DLMS.Common.Messages", assembly);            
            return Common.Messages.ResourceManager.GetString(Key);
        }
        public static string GetMessage(string Key, params string[] list)
        {
            string message = Common.Messages.ResourceManager.GetString(Key);
            //for (int i = 0; i < list.Length; i++)
            {
                message = string.Format(message, list);
            }
            return message;
        }
        public static string Encrypt(string password)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        private static string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                //if (words != "") words += "AND ";
                var unitsMap = new[]
                {
                    "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
                };
                var tensMap = new[]
                {
                    "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
                };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public static string ConvertAmountInWords(decimal d)
        {
            //Grab a string form of your decimal value ("12.34")
            var formatted = d.ToString();

            if (formatted.Contains("."))
            {
                string[] sides = formatted.Split('.');

                if (Int32.Parse(sides[1]) > 0)
                {
                    return ConvertNumbertoWords(Int32.Parse(sides[0])) + " AND " + ConvertNumbertoWords(Int32.Parse(sides[1].Substring(0, 2))) + " PAISA";
                }
            }

            return ConvertNumbertoWords(Convert.ToInt32(d));
        }

        public static string ConvertToRanges(string csvNumbers)
        {

            csvNumbers = SortCSVNumberList(csvNumbers);

            string result = string.Empty;
            string[] arr1 = csvNumbers.Split(',');
            int[] arr = new int[arr1.Length];

            for (int x = 0; x < arr1.Length; x++) // Convert string array to integer array
            {
                arr[x] = Convert.ToInt32(arr1[x].ToString());
            }

            int start, end;  // track start and end
            end = start = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                // as long as entries are consecutive, move end forward
                if (arr[i] == (arr[i - 1] + 1))
                {
                    end = arr[i];
                }
                else
                {
                    // when no longer consecutive, add group to result
                    // depending on whether start=end (single item) or not
                    if (start == end)
                        result += start + ",";
                    else
                        result += start + "-" + end + ",";

                    start = end = arr[i];
                }
            }

            // handle the final group
            if (start == end)
                result += start;
            else
                result += start + "-" + end;

            return result;
        }

        public static string SortCSVNumberList(string csvNumbers)
        {
            return String.Join(",", csvNumbers.Split(',').Select(x => int.Parse(x)).OrderBy(x => x));
        }

    }
}
