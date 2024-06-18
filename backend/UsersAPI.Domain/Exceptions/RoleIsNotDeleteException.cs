namespace UsersAPI.Domain.Exceptions
{
    public class RoleIsNotDeleteException : Exception
    {
        public RoleIsNotDeleteException(string roleName)
            : base($"O perfil '{roleName}' não pode ser excluído, existe(m) Usuário(s) vinculado(s) a ele.")
        {

        }
    }
}