using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlazorHRAgency.Data
{
    public class RoleEditModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> RolesCollection { get; set; }
        public IList<string> UserRoles { get; set; }
        public RoleEditModel()
        {
            RolesCollection = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
