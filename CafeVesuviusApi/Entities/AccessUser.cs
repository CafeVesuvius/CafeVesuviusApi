using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public class AccessUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();
}
