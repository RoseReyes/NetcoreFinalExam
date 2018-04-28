using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace auctions.Models
{
  
     public class User {
        
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Password { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        List<Post> Posts { get; set; }
        List<Like> Likes { get; set; }
            
            public User(){
                Posts = new List<Post>();
                Likes = new List<Like> ();
            }

    }
}