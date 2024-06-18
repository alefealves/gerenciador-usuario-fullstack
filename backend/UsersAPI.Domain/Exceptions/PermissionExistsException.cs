namespace UsersAPI.Domain.Exceptions
{
    public class PermissionExistsException : Exception
    {
        public PermissionExistsException()
            : base($"Está regra de permissão com esse Perfil e SubModúlo já está cadastrada. Informe outra regra.")
        {

        }
    }
}