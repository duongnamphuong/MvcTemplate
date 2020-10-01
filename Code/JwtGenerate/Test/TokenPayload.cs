using System.Collections.Generic;

namespace JwtGenerate.Test
{
    internal class TokenPayload
    {
        public string Username { get; set; }

        public long IssuedAt { get; set; }

        public int BusinessUserID { get; set; }
        public int BusinessID { get; set; }
        public List<int> AssignedBusinessIDs { get; set; }
    }
}