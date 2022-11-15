using Hms.Models;
using Hms.Models.Utility;
using Hms.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Service
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration,
                            UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //Todo : Remove Role Creation Code and Combine both methods (for admin and user).
        public async Task<ResponseViewModel> AddUserAsync(SignUpViewModel signUpVm)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                var user = new User
                {
                    FirstName = signUpVm.FirstName,
                    LastName = signUpVm.LastName,
                    Email = signUpVm.Email,
                    UserName = signUpVm.Email,
                    IsCustomer = signUpVm.IsCustomer,
                };

                var result = await _userManager.CreateAsync(user, signUpVm.Password);

                if (!signUpVm.IsCustomer)
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                else
                    await _userManager.AddToRoleAsync(user, UserRoles.User);

                if (result.Succeeded)
                {
                    responseViewModel.HasError = false;
                }
                else
                {
                    
                    responseViewModel.HasError = true;
                    responseViewModel.ErrorDescription = result.Errors.FirstOrDefault().Description;
                }
                return responseViewModel;
            }


            catch(Exception ex)
            {
                responseViewModel.HasError = true;
                responseViewModel.ErrorCode = ex.HResult.ToString();
                responseViewModel.ErrorDescription = ex.Message;
                return responseViewModel;
            }
        }

    }

    public interface IUserService
    {
        Task<ResponseViewModel> AddUserAsync(SignUpViewModel signUpVm);
    }
}
