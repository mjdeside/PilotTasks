﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Model.Common
{
    public class Result<T>
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
