﻿using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Models
{
    public class Logistic : Item
    {
        public int Rate { get; set; }

        public ItemForm Transport { get; set; }
    }
}