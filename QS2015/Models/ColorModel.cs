using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QS2015.Models
{
    public class ColorModel
    {
        //private static string[] colorsList = { "Blue", "Red", "Green", "Orange", "Olive", "BlueViolet" };
        //private static string[] colorsList = { "Blue", "Red" };
        private static string[] colorsList = { "white", "lightgrey" };

        public static string getNextColor(int index)
        {
            int residue = index % colorsList.Length;

            return colorsList[residue];
        }
    }
}