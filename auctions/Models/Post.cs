using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace auctions.Models
{
  public class Post {
        
        public int PostId { get; set; }
        public string Idea { get; set; }
        public int UserId { get; set; }
        public User User { get; set;}
        public List<Like> likes {get; set;} 

        public Post () {
            likes = new List<Like>();
        }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}