using System;
using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class ModelBase
    {
        public ModelBase() : base()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
