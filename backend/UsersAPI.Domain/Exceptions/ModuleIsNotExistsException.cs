namespace UsersAPI.Domain.Exceptions
{
    public class ModuleIsNotExistsException : Exception
    {
        public ModuleIsNotExistsException(Guid id)
            : base($"O ID '{id}' não está vinculado a nenhum módulo. Informe outro Módulo.")
        {

        }
    }
}