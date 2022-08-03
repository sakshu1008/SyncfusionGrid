using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Information.Models
{
    [DataContract(IsReference = true)]
    public partial class InformationT
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? City { get; set; }

        [Required]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CityT? CityNavigation { get; set; }
    }
}
