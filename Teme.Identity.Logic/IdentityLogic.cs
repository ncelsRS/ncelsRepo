using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic;
using System.Linq;
using System.Collections.Concurrent;

namespace Teme.Identity.Logic
{
    public class IdentityLogic : BaseLogic, IIdentityLogic
    {
        private readonly IConfiguration _config;
        private static readonly ConcurrentDictionary<string, int> _updateTokens = new ConcurrentDictionary<string, int>();
        public IdentityLogic(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(AuthUser user)
        {
            var issuer = _config["IdentityConfig:Issuer"];
            var certPath = _config["IdentityConfig:CertPath"];
            var certPass = _config["IdentityConfig:CertPass"];
            var extTime = _config[$"IdentityConfig:ExpirationSeconds"];

            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()) };
            var scopeClaims = user.Scopes.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x.Scope));
            claims.AddRange(scopeClaims);

            var cert = new X509Certificate2(certPath, certPass);
            var creds = new SigningCredentials(new X509SecurityKey(cert), SecurityAlgorithms.RsaSha256);
            var expires = DateTime.Now.AddSeconds(Double.Parse(extTime));

            var token = new JwtSecurityToken(
                issuer,
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateUpdateToken(int id)
        {
            var newRefreshToken = Guid.NewGuid().ToString("N");
            _updateTokens.TryAdd(newRefreshToken, id);
            return newRefreshToken;
        }

        public int RealiseUpdateToken(string token)
        {
            if (!_updateTokens.TryRemove(token, out var id))
                return 0;
            return id;
        }
    }
}
