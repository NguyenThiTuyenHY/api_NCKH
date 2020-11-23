using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

#nullable disable

namespace API.Models
{
    public partial class Tblhoso
    {
        public int Id { get; set; }
        public int? Iddetai { get; set; }
        public string Ten { get; set; }
        public DateTime? Ngay { get; set; }
        public string Minhchung { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual Tbldetai IddetaiNavigation { get; set; }
    }
}
