using System;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers._1._1.Identity
{
    [ApiController]
    [ApiVersion( "1.1" )]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Post Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403, $"Web-Api login. User {model.Email} not found!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new {token = jwt, status = "Logged in"});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }


        /// <summary>
        /// Post Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return StatusCode(403, ("User with this email already exist"));
            }
            var user = new AppUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var roleResult = _userManager.AddToRoleAsync(user, "Customer").Result;

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var userId = await _userManager.GetUserIdAsync(user);
                return await Login(new LoginDTO()
                {
                    Email = model.Email, Password = model.Password
                });
                /*
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
                if (confirmEmail.Succeeded) {
                    _logger.LogInformation("Email Confirmed.");
                    return await Login(new LoginDTO()
                    {
                        Email = model.Email, Password = model.Password
                    });
                } 
                else 
                {
                    _logger.LogInformation("Email not Confirmed.");
                    return StatusCode(403, model);
                }
                */
            }
            else
            {
                _logger.LogInformation("Web-Api register. Something went wrong!");
                return StatusCode(403, "Web-Api register. Something went wrong!");
            } 

        }

        public class LoginDTO
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class RegisterDTO
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
        }
    }
}
