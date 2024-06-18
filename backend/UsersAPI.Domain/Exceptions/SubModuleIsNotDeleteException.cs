namespace UsersAPI.Domain.Exceptions
{
    public class SubModuleIsNotDeleteException : Exception
    {
        public SubModuleIsNotDeleteException(string subModuleName)
            : base($"O submódulo '{subModuleName}' não pode ser excluído, existe(m) Regra(s) de Permissão(ões) vinculada(s) a ele.")
        {

        }
    }
}