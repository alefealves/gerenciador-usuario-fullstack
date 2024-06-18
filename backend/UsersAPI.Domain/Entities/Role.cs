namespace UsersAPI.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
        public DateTime? CreatedAt { get; set; }


        /// <summary>
        /// Relacionamentos entre Entidades
        /// </summary>
        public List<Permission>? Permissions { get; set; }
        public List<User>? Users { get; set; }
    }
}