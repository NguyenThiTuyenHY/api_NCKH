using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblnhomtg
    {
        public int Id { get; set; }
        public int? Iddetai { get; set; }
        public string Hoten { get; set; }
        public int? Idnv { get; set; }
        public string Chucvu { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tbldetai IddetaiNavigation { get; set; }
    }
}
