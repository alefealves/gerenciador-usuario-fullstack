namespace UsersAPI.Domain.Exceptions
{
    public class RoleAlreadyExistsException : Exception
    {
        public RoleAlreadyExistsException(string roleName)
            : base($"O perfil '{roleName}' já está cadastrado. Informe outro perfil.")
        {

        }
    }
}