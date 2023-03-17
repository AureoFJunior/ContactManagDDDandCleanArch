namespace ContactManag.Domain.Models
{
    public class User : BaseEntity
    {
        public User(){}

        public User(string firstName, string lastName, string userName, string password, string email, short? isLogged = 0)
        {
            ValidateCategory(firstName, lastName, userName, password, email);
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Email = email;
            IsLogged = isLogged;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public short? IsLogged { get; set; }

        private void ValidateCategory(string firstName, string lastName, string userName, string password, string email)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new InvalidOperationException("The name is invalid");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("The email is invalid");

            if (string.IsNullOrEmpty(userName))
                throw new InvalidOperationException("The username is invalid");

            if (string.IsNullOrEmpty(password))
                throw new InvalidOperationException("The password is invalid");
        }
    }
}