using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TungaRestaurant.Models
{
    // Add profile data for application users by adding properties to the User class
    public class UserInfo : IdentityUser
    {
        [Required]
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        [Required]
        public int Sex { get; set; }
        [Required]
        public string Address {get;set;}
        public Boolean IsVegan { get; set; } = false;
        public UserStatus Status { get; set; } = UserStatus.NORMAL;
        [ForeignKey("Branch")]
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        [ForeignKey("PreferBranch")]
        public int? PreferBranchId { get; set; }
        public Branch PreferBranch { get; set; }
    }
}
