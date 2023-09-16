using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // The class "User" will be acting as the data model for the User Table in the database.
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
    }
}
