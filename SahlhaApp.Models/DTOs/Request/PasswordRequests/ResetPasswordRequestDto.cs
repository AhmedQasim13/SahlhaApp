﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahlhaApp.Models.DTOs.Request.PasswordRequests
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; }


        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

    
    }
}
