﻿namespace CopixelApi.Domain.Models;

public class User
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}