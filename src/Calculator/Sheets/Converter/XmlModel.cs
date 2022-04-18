﻿using Calculator.Models;
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

        public static XmlModel Parse(DataModel model)
        {
            XmlModel xmlModel = new XmlModel();
            xmlModel.Version = model.Version;
            if (model.Sheets != null)
            {
                foreach (Nodes nodes in model.Sheets)
                {
                    xmlModel.Sheets.Add(XmlSheet.Parse(nodes));
                }
            }
            return xmlModel;
        }

        public DataModel GetObject()
        {
            DataModel dataModel = new DataModel(Version);
            if (Sheets != null)
            {
                //Classes.HMLogger.Debug($"Xml sheets: {Sheets.Count}");
                foreach (XmlSheet xmlSheet in Sheets)
                {
                    dataModel.Sheets.Add(xmlSheet.GetObject());
                }
            }
            return dataModel;
        }

    }
}
