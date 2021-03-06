﻿using System.Web.Script.Serialization;

namespace EKContent.bus.Entities
{
    public class StyleSetting 
    {
        public string Key { get; set; }
        public string Value { get; set; }
       [ScriptIgnore]
        public string DefaultValue { get; set; }
       [ScriptIgnore]
       public string Category { get; set; }

        public bool isDefault()
        {
            return Value == DefaultValue;
        }
    }
}