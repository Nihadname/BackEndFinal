using Microsoft.AspNetCore.Identity;

namespace BackEndFinal.ViewModels
{
    public class RoleUpdateVM
    {
        public RoleUpdateVM(IList<string> roles,string userName, List<IdentityRole> identityRoles)
        {
            this.roles = roles;
            UserName = userName;
            IdentityRoles = identityRoles;
        }
        public string UserName { get; set; }
        public List<IdentityRole> IdentityRoles { get; set; }
        public IList<string> roles { get; set; }
    }
}
