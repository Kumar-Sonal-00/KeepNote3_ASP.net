using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Entities
{
    // The class "Note" will be acting as the data model for the Note Table in the database.
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }

        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
        public string NoteStatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public int ReminderId { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public Reminder Reminder { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
