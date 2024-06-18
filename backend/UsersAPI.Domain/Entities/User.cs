namespace UsersAPI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Relacionamentos entre Entidades
        /// </summary>
        public Role? Role { get; set; }
    }
}