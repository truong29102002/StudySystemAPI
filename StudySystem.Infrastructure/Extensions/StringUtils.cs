﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Infrastructure.Extensions
{
    public static class StringUtils
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
