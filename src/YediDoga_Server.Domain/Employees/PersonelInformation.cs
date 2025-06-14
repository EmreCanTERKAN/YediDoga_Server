﻿namespace YediDoga_Server.Domain.Employees;

public sealed record PersonelInformation
{
    public string TcNo { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
}
