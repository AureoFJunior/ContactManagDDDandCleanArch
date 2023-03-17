using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManag.Domain.Models
{
    public class Contact : BaseEntity
    {
        public Contact() { }

        public Contact(string type, string value, int personId)
        {
            ValidateCategory(type, value, personId);
            Type = type;
            Value = value;
            PersonId = personId;
        }

        public string Type { get; set; }
        public string Value { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        private void ValidateCategory(string type, string value,int personId)
        {
            if (string.IsNullOrEmpty(type))
                throw new InvalidOperationException("The type is invalid");

            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException("The value is invalid");

            if(personId == 0)
                throw new InvalidOperationException("The person is invalid");
        }
    }
}