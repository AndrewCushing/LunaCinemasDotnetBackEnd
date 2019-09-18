using System;
using System.Globalization;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public abstract class ContentFilter
    {
        private string[] prohibitedWords = {"natwest","hsbc"};
        protected virtual string filterStuff(string thingToFilter)
        {
            for (int j = 0; j < prohibitedWords.Length; j++)
            {
                thingToFilter = thingToFilter.Replace(prohibitedWords[j], GetStars(prohibitedWords[j]), true, new CultureInfo("es-ES", false));
            }
            return thingToFilter;
        }

        private string GetStars(string v)
        {
            string result = "";
            for (int i = 0; i < v.Length; i++)
            {
                result += "*";
            }

            return result;
        }
    }
}