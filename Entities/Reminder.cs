using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Entities
{
    public class Reminder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReminderId { get; set; }

        public string ReminderName { get; set; }
        public string ReminderDescription { get; set; }
        public string ReminderType { get; set; }
        public string CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ReminderCreationDate { get; set; }

        [JsonIgnore]
        public int NoteId { get; set; }

        [JsonIgnore]
        public string ReminderCreatedBy { get; set; }

        [JsonIgnore]
        public Note Note { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
