using System.Collections;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ResponseObject<T>
    {
        public bool successful { get; }
        public string body { get; }
        public List<T> contentList { get; }

        public ResponseObject(bool successful, string body, List<T> contentList)
        {
            this.successful = successful;
            this.body = body;
            this.contentList = contentList;
        }
    }
}