using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupermarketApi.Abstractions.Services;
using SupermarketApi.WebApi.Configuration;
using SupermarketApi.WebApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;
        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService tokenHandlerService)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostUserAsync([FromBody] SaveUserResource resource)
        {
            var existingUser = await _userManager.FindByEmailAsync(resource.Email);
            if(existingUser != null)
            {
                return BadRequest("Usuario existente.");
            }
            var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = resource.Email, UserName = resource.Name }, resource.Password);
            if (isCreated.Succeeded)
            {
                return Ok();
            }
            return BadRequest(isCreated.Errors.Select(x => x.Description));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserResource resource)
        {
            var existingUser = await _userManager.FindByEmailAsync(resource.Email);
            if (existingUser == null)
            {
                return BadRequest(new ResponseLoginUserResource() { 
                    Login = false,
                    Errors = new List<string>()
                    {
                        "Usuario o contraseña incorrecta"
                    }
                });
            }
            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, resource.Password);
            if (isCorrect)
            {
                TokenParameters parameters = new TokenParameters()
                {
                    Id = existingUser.Id,
                    PasswordHash = existingUser.PasswordHash,
                    UserName = existingUser.UserName,
                };
                var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);

                return Ok(new ResponseLoginUserResource()
                {
                    Login = true,
                    Token = jwtToken
                });
            }
            return BadRequest(new ResponseLoginUserResource()
            {
                Login = false,
                Errors = new List<string>()
                    {
                        "Usuario o contraseña incorrecta"
                    }
            });
        }
    }
}
