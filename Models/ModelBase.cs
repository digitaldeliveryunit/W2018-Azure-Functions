using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace com.petronas.myevents.api.Models
{
    public class ModelBase
    {
        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key] [JsonProperty("id")] 
        public string Id { get; set; }

        // [JsonIgnore]
        public bool IsDeleted { get; set; }

        // [JsonIgnore]
        public string Discriminator { get; set; }
    }
}