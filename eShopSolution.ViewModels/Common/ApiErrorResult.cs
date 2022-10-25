﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    { 
        public ApiErrorResult()
        {
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}
