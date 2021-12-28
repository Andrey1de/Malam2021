﻿using Malam2021.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Malam2021.Extensions
{ 
    public static class StringCustomExtensions
    {
        public static bool IsZ(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public static bool IsZ(this Array arr)
        {
            return arr == null || arr.Length == 0 ;
        }
        public static string ToNZ(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? "" : str;
        }
        public static string[] SplitZ(this string str, string delimChars)
        {
            if (string.IsNullOrEmpty(str)) return new string[] { };
            if (string.IsNullOrEmpty(delimChars)) return new string[] { str };
            return str.Split(delimChars.ToCharArray(), 
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
        public static string ClearWhiteSpaces(this string str)
        {
            return Regex.Replace(str.ToNZ(), @"[\s+\n\r]", "").ToLower();
        }


        public static string[] SplitDelimPairs(this string strDelimited, string delimeters = ",;|")
        {
            strDelimited = strDelimited.ClearWhiteSpaces().ToLower();
           // Console.WriteLine(strDelimited);

            string[] arr = strDelimited.Split(delimeters.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);
            return arr;
        }

        //public static FromTo[] SplitDelimFromTo(this string strDelimited,
        //         string pairDelimiter = "-/", string delimeters = ",;|")

        //{
            
        //    strDelimited = strDelimited.ToNZ().Replace("%2C", ",").Replace("%2F", "-")
        //            .ToLower().TrimEnd(",;".ToCharArray());
        //    List<FromTo> list = new List<FromTo>();

        //    if (!strDelimited.IsZ())
        //    {
              
        //        strDelimited = Regex.Replace(strDelimited, @"[\s+\n\r]", "").ToLower();
        //        string[] arrPair = strDelimited.SplitDelimPairs(delimeters);

        //        foreach (var str in arrPair)
        //        {
        //            var _arr = str.Split(pairDelimiter.ToCharArray(),
        //                StringSplitOptions.RemoveEmptyEntries);
        //            if (_arr.Length == 2)
        //            {
        //                list.Add(new FromTo() { From = _arr[0], To = _arr[1] });
        //            }
        //        }


        //    }           
        //    return list.ToArray();

        //}

    }
}
