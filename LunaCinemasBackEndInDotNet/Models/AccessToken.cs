using System;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class AccessToken
    {
        public Guid Id { get; }
        public string UserId { get; set; }
        public DateTime Time { get; }

        public AccessToken(string userId)
        {
            UserId = userId;
            Id = new Guid();
            Time = DateTime.Now;
        }
    }
}