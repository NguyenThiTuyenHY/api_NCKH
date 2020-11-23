using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblsohuudetai
    {
        public int Id { get; set; }
        public int? Iddetai { get; set; }
        public int? Idsohuu { get; set; }
        public string Ghichu { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tbldetai IddetaiNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblsohuu IdsohuuNavigation { get; set; }
    }
}
