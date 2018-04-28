using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace auctions.Models

{
    public class NewAuctionViewModel
    {
        
        [Required(ErrorMessage="Product name is required")]
        [Display(Name="Product Name")]
        [MinLength(3)]
        public string ProdName { get; set; }
        
        [Required(ErrorMessage="Description is required")]
        [MinLength(10)]
        [Display(Name="Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage="Starting bid is required")]
        [Display(Name="Starting Bid")]
        public double StartBid { get; set; }
        
        [Required(ErrorMessage="End Date is required")]
        [Display(Name="End Date")]
        public DateTime EndDate { get; set; }
    }
}
