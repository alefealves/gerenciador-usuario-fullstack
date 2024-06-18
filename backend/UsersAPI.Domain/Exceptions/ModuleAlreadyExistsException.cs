namespace UsersAPI.Domain.Exceptions
{
    public class ModuleAlreadyExistsException : Exception
    {
        public ModuleAlreadyExistsException(string moduleName)
            : base($"O módulo '{moduleName}' já está cadastrado. Informe outro módulo.")
        {

        }
    }
}