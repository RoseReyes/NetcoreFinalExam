using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace auctions.Models

{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")]
        [Display(Name="Email")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage="Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Name should only contain letters")]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required(ErrorMessage="Alias is required")]
        [Display(Name="Alias")]
        public string Alias { get; set; }

        [Required(ErrorMessage="Password is required")]
        // [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+$", ErrorMessage="Password should contain 1 leter, 1 digit and 1 special characters")]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string ConfirmPassword { get; set; }   
    }
}
