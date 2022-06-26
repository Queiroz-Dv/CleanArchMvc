using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] LoginModel model)
        {
            bool result = await _authenticate.RegisterUser(model.Email, model.Password);
            if (result)
            {
                return Ok($"User {model.Email} was created successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }

        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel model)
        {
            bool result = await _authenticate.Authenticate(model.Email, model.Password);

            if (result)
            {
                return GenerateToken(model);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel model)
        {
            //declarações do usuário
            Claim[] claims = new[]
            {
                new Claim("email", model.Email),
                new Claim("qualquercoisa", "qualquer coisa"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            SymmetricSecurityKey privateKey = new(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar assinatura digital
            SigningCredentials credentials = new(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            DateTime expiration = DateTime.UtcNow.AddMinutes(5);

            //gerar token
            JwtSecurityToken token = new(
                //emissor
                issuer: _configuration["Jwt:Issuer"],
                //audienia
                audience: _configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiracao
                expires: expiration,
                // assinatura digital
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
