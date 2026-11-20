using System;
using System.Collections.Generic;

namespace Paredes_Gabriel_LibraryManagement.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? MembershipId { get; set; }

    public DateOnly? JoinDate { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
