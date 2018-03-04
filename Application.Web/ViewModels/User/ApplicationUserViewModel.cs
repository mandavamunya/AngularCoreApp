using Application.Core.Enums;
using Application.Models.PublicationViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.UserViewModel
{
    public class ApplicationUserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public bool EmailConfirmed { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public Team Team { get; set; }

        public List<JournoRankingViewModel> JournoRankings { get; set; }
    }
}
