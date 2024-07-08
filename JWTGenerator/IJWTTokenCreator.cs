using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTGenerator
{
    public interface IJWTTokenCreator
    {
        public string GenerateJWTToken();
    }
}
