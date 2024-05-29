using api8.Models.DTOs;
using api8.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            
            var user = new IdentityUser 
            { 
                UserName = registerDTO.UserName,
                Email = registerDTO.UserName
            };
            var IdentityResult =await userManager.CreateAsync(user, registerDTO.Password);
            if(IdentityResult.Succeeded) 
            {
                if((registerDTO.Role!=null) && registerDTO.Role.Any()) 
                {
                    IdentityResult= await userManager.AddToRolesAsync(user, registerDTO.Role);
                    if(IdentityResult.Succeeded)
                    {
                        return Ok("User has successfully registered. Please Login!");
                    }
                }
            }
            return BadRequest("Something went wrong!");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.UserName);
            if(user != null) 
            {
                var CheckPassword = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (CheckPassword)
                {
                    //Get Roles
                    var rolers = await userManager.GetRolesAsync(user);
                    //Generate Tokens
                    if (rolers != null) 
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(user, rolers.ToList());
                        var token = new LoginResponseDTO()
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(token);
                    }
                    
                }
            }
            return BadRequest("userName or password incorrect");

        }
    }
}
