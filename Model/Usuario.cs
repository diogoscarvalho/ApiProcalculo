﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class Usuario
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
