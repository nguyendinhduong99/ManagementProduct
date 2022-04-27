﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Models.Dtos
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
