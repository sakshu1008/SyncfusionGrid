using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Information.Models
{
    [JsonObject(IsReference = true)]
    public partial class CityT
    {
        public CityT()
        {
            InformationTs = new HashSet<InformationT>();
        }

        public int CityId { get; set; }
        public string? CityName { get; set; }

        //[System.Text.Json.Serialization.JsonIgnore]
        //[IgnoreDataMember]
        public virtual ICollection<InformationT> InformationTs { get; set; }
    }
}
