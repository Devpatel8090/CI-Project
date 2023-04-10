using System;
using System.Collections.Generic;

namespace CI_Platfrom.Entities.Models;

public partial class ContactU
{
    public long ContactUsId { get; set; }

    public string Name { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
