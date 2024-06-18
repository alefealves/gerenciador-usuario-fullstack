namespace UsersAPI.Domain.Exceptions
{
    public class ModuleIsNotDeleteException : Exception
    {
        public ModuleIsNotDeleteException(string moduleName)
            : base($"O módulo '{moduleName}' não pode ser excluído, existe(m) Submódulo(s) vinculado(s) a ele.")
        {

        }
    }
}