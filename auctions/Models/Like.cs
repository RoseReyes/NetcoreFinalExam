using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace auctions.Models
{
    public class Like {
        
        public int LikeId { get; set; }
        public int UserId {get; set;}
        public  User User { get; set; }
        public int PostId {get; set;}
        public  Post Post { get; set;} 
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}