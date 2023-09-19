﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.Models.Response
{
    public class LoginResponseModel
    {
        public bool IsActive { get; set; }
        [Required]
        public string Token { get; set; } = null!;
        public LoginResponseModel(bool isActive, string token)
        {
            IsActive = isActive;
            Token = token;
        }
    }
}
