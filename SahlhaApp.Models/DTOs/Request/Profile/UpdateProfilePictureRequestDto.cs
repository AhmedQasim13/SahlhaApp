﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahlhaApp.Models.DTOs.Request.Profile
{
    public class UpdateProfilePictureRequestDto
    {
        public IFormFile ImgUrl { get; set; }
    }
}
