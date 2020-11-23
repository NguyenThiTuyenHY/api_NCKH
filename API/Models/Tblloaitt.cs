using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblloaitt
    {
        public Tblloaitt()
        {
            Tbltintucs = new HashSet<Tbltintuc>();
        }

        public int Id { get; set; }
        public string Tenloaitt { get; set; }
        //[Newtonsoft.Json.JsonIgnore]
        //[System.Xml.Serialization.XmlIgnore]
        public ICollection<Tbltintuc> Tbltintucs { get; set; }
    }
}
