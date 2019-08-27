using System.Collections;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ResponseObject<T> : IEnumerator where T : IResponseEntity
    {
        private ResponseSuccessMarker successful;
        private ResponseBody body;
        private List<T> contentList;
        private int index;

        public object Current
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return successful;
                    case 1:
                        return body;
                    default:
                        return contentList[index - 2];
                }
            }
        }

        public ResponseObject(bool successful, string body, List<T> contentList)
        {
            this.successful = new ResponseSuccessMarker(successful);
            this.body = new ResponseBody(body);
            SetBody(body);
            this.contentList = contentList;
        }

        private void SetBody(string body)
        {
            this.body.Body = body;
        }

        private void SetSuccess(bool successful)
        {
            this.successful.Success = successful;
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

        public bool MoveNext()
        {
            index++;
            return index < contentList.Count;
        }

        public void Reset()
        {
            index = 0;
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }
    }
}