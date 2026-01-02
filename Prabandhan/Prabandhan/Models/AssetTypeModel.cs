using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Models
{
    public class AssetTypeModel
    {
        public int Atid { get; set; }
        public string? AtName { get; set; }
        public string? AllowedExtensions { get; set; }
        public string? MetaDataColumns { get; set; }

        public Dictionary<string, FieldDefinition> MetaFields { get; set; }
    }
}
