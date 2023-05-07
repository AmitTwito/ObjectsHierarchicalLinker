using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ObjectsHierarchyCreator.BE.AccessControl;
using ObjectsHierarchyCreator.BE.Utils;
using ObjectsHierarchyCreator.DAL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsHierarchyCreator.BL
{
    public class AccessControlService : IAccessControlService
    {
        private readonly AppConfig _config;

        private readonly IAccessControlRepository _accessControlRepo;
        public AccessControlService(IAccessControlRepository accessControlRepo, IOptions<AppConfig> config)
        {
            _accessControlRepo = accessControlRepo;
            _config = config.Value;
        }


        public List<User> GetAllUsers()
        {
            return _accessControlRepo.GetAllUsers();
        }


        public Token GetToken(AuthRequest request)
        {
            var user = _accessControlRepo.GetUserByCredentials(request);

            return user == null ? null : new Token { AccessToken = GenerateTokenString(user) };
        }


        private string GenerateTokenString(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_config.JWTKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, user.Username)
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
