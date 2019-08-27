using LunaCinemasBackEndInDotNet.BusinessLogic;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class ResponseSuccessMarker : IResponseEntity
    {
        public bool Success { get; set; }

        public ResponseSuccessMarker(bool success)
        {
            Success = success;
        }
    }
}