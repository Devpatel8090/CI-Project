﻿using System;
using System.Collections.Generic;

namespace CI_Platfrom.Entities.Models;

public partial class CmsPage
{
    public long CmsPageId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Slug { get; set; }

    public int Status { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
