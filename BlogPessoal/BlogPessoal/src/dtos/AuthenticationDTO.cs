﻿using BlogPessoal.src.utils;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    public class AuthenticateDTO
    {
        public AuthenticateDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string Token { get; set; }
        public AuthorizationDTO(int id, string email, UserType type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}
