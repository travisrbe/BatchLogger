﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string ChosenName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
