﻿using System.ComponentModel.DataAnnotations;

namespace Application.Web.Models;

public class LoginViewModel
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}