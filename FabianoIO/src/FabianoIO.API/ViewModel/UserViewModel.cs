﻿using System.ComponentModel.DataAnnotations;

namespace FabianoIO.API.ViewModel
{
    public class UserViewModel
    {
        public class RegisterUserViewModel
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
            public string LastName { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [DataType(DataType.Date)]

            public DateTime DateOfBirth { get; set; }

            public bool IsAdmin { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "As senhas não conferem.")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginUserViewModel
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string Password { get; set; }
        }

        public class UserTokenViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public IEnumerable<ClaimViewModel> Claims { get; set; }
        }

        public class LoginResponseViewModel
        {
            public string AccessToken { get; set; }
            public double ExpiresIn { get; set; }
            public UserTokenViewModel UserToken { get; set; }
        }

        public class LoginResponseTestViewModel
        {
            public bool Success { get; set; }
            public LoginResponseViewModel Data { get; set; } = new();
        }

        public class ClaimViewModel
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }
    }
}