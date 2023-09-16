using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Entities
{
    // The class "Category" will be acting as the data model for the Category Table in the database.
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        [JsonIgnore]
        public string CategoryCreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CategoryCreationDate { get; set; }

        [JsonIgnore]
        public ICollection<Note> Notes { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
