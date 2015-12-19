using SQLite;

namespace Agenda.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}