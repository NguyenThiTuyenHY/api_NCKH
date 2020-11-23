using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

#nullable disable

namespace API.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Ten { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
