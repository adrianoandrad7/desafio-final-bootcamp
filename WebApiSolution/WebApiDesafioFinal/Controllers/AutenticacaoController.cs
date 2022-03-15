using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIVS2019;

namespace WebApiDesafioFinal.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : Controller
    {
        private readonly ConfiguracoesJWT ConfiguracoesJWT;
        public AutenticacaoController(IOptions<ConfiguracoesJWT> opcoes)
        {
            ConfiguracoesJWT = opcoes.Value;
        }

        [HttpGet]
        [Route("gerente")]
        public IActionResult ObterTokenGerente()
        {
            var token = GerarTokenGerente();

            var retorno = new
            {
               Token = token
            };

            return Ok(retorno);

        }

        [HttpGet]
        [Route("secretaria")]
        public IActionResult ObterTokenSecretaria()
        {
            var token = GerarTokenSecretaria();

            var retorno = new
            {
                Token = token
            };

            return Ok(retorno);

        }
        private string GerarTokenGerente()
        {
            IList<System.Security.Claims.Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Gerente"));

            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfiguracoesJWT.Segredo)), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://localhost:5001",
                Issuer = "DesafioFinal2022",
                Subject = new ClaimsIdentity(claims),
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }
        private string GerarTokenSecretaria()
        {
            IList<System.Security.Claims.Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Secretaria"));

            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfiguracoesJWT.Segredo)), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://localhost:5001",
                Issuer = "DesafioFinal2022",
                Subject = new ClaimsIdentity(claims),
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }
    }
}
