namespace ContactManag.Domain.Models
{
    public class Person : BaseEntity
    {
        public Person() { }

        public Person(string name, DateTime birthDate)
        {
            ValidateCategory(name, birthDate);
            Name = name;
            BirthDate = birthDate;  
        }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Contact>? Contact { get; set; }

        private void ValidateCategory(string name, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The name is invalid");

            if (DateTime.MinValue == birthDate)
                throw new InvalidOperationException("Birthdate is invalid");
        }
    }
}