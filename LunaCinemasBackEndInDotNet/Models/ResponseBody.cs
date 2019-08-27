using LunaCinemasBackEndInDotNet.BusinessLogic;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class ResponseBody : IResponseEntity
    {
        public string Body { get; set; }

        public ResponseBody(string body)
        {
            Body = body;
        }

    }
}