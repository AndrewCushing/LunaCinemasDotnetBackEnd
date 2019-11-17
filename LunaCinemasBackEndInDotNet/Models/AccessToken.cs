using System;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class AccessToken
    {
        public string Id { get; set; }
        public Guid Token { get; }
        public string UserId { get; set; }
        public DateTime CreationTime { get; }

        public AccessToken(string userId)
        {
            UserId = userId;
            Token = Guid.NewGuid();
            CreationTime = DateTime.Now;
        }
    }
}