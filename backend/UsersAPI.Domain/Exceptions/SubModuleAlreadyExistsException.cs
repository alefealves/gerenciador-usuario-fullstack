namespace UsersAPI.Domain.Exceptions
{
    public class SubModuleAlreadyExistsException : Exception
    {
        public SubModuleAlreadyExistsException(string subModuleName)
            : base($"O submódulo '{subModuleName}' já está cadastrado. Informe outro submódulo.")
        {

        }
    }
}