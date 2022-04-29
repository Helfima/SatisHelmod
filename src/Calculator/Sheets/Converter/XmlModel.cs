﻿using Calculator.Databases.Models;
using Calculator.Sheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calculator.Sheets.Converter
{
    [Serializable]
    [XmlRoot("Model")]
    public class XmlModel
    {
        [XmlAttribute("Version")]
        public int Version;

        [XmlElement("Sheets")]
        public List<XmlSheet> Sheets = new List<XmlSheet>();
    }
}
