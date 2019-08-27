using System.Collections.Generic;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ResponseObject<T>
    {
        private bool successful;
        private string body;
        private List<T> contentList;

        public ResponseObject(bool successful, string body, List<T> contentList)
        {
            this.successful = successful;
            this.body = body;
            this.contentList = contentList;
        }

        public ResponseObject<T> setPositive(string body, List<T> contentList)
        {
            successful = true;
            this.body = body;
            this.contentList = contentList;
            return this;
        }
    }
}