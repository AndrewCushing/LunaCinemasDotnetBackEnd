namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public abstract class ContentFilter
    {
        private string[] prohibitedWords = {"natwest","hsbc"};
        protected virtual string filterStuff(string thingToFilter)
        {

        }
    }
}