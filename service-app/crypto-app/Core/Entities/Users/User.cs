using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Enums;

namespace crypto_app.Core.Entities.Users
{
    public class User : BaseUser
    {
        public string? AppIcon { get; set; }
        public ApplicationTheme Theme { get; set; } = ApplicationTheme.LightTheme;
    }
}