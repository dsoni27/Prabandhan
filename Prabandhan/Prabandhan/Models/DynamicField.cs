using Newtonsoft.Json;
using Parbandhan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Models
{
    public class DynamicField : ViewModelBase
    {
        public string Key { get; set; }
        public string Control { get; set; }
        public string DataType { get; set; }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public Dictionary<string, FieldDefinition> ParseMetaData(string metaData)
        {
            if (string.IsNullOrWhiteSpace(metaData))
                return new Dictionary<string, FieldDefinition>();

            return JsonConvert.DeserializeObject<Dictionary<string, FieldDefinition>>(metaData)
                   ?? new Dictionary<string, FieldDefinition>();
        }
    }
}
