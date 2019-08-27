using System.Collections;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ResponseObject<T>
    {
        public bool successful { get; private set; }
        public string body { get; private set; }
        public List<T> contentList { get; private set; }

        public ResponseObject(bool successful, string body, List<T> contentList)
        {
            this.successful = successful;
            this.body = body;
            SetBody(body);
            this.contentList = contentList;
        }

        private void SetBody(string body)
        {
            this.body = body;
        }

        private void SetSuccess(bool successful)
        {
            this.successful = successful;
        }

        public ResponseObject<T> SetPositive(string body, List<T> contentList)
        {
            SetSuccess(true);
            SetBody(body);
            this.contentList = contentList;
            return this;
        }

        public ResponseObject<T> SetNegative(string body, List<T> contentList)
        {
            SetSuccess(false);
            SetBody(body);
            this.contentList = contentList;
            return this;
        }

        private void InsertContent(List<T> contentToInsert)
        {
            if (contentList.Count>2)
            {
                contentList.RemoveRange(2, contentList.Count);
            }
            contentList.AddRange(contentToInsert);
        }
    }
}