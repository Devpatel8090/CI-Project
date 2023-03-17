﻿using System;
using System.Collections.Generic;

namespace CI_Platfrom.Entities.Models;

public partial class Comment
{
    public long CommentId { get; set; }

    public long UserId { get; set; }

    public long MissionId { get; set; }

    public string? CommentText { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
