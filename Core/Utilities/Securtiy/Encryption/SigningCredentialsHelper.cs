using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Securtiy.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey security)
        {
            return new SigningCredentials(security, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
