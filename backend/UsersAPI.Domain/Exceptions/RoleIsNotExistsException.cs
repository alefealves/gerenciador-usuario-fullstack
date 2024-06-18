namespace UsersAPI.Domain.Exceptions
{
  public class RoleIsNotExistsException : Exception
  {
    public RoleIsNotExistsException(Guid id)
        : base($"O ID '{id}' não está vinculado a nenhum perfil. Informe outro Perfil.")
    {

    }
  }
}
