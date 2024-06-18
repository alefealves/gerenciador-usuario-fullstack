namespace UsersAPI.Domain.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email)
            : base($"O email '{email}' já está cadastrado. Informe outro email.")
        {

        }
    }
}