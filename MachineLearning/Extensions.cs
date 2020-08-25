﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning
{
    public static class Extensions
    {
        private const double Epsilon = 1e-10;

        public static bool IsZero(this double d)
        {
            return Math.Abs(d) < Epsilon;
        }
    }
}
