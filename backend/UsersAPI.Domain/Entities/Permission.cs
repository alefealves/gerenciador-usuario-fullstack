namespace UsersAPI.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid SubModuleId { get; set; }
        public bool? Create { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
        public bool? Read { get; set; }
        public DateTime? CreatedAt { get; set; }


        /// <summary>
        /// Relacionamentos entre Entidades
        /// </summary>
        public Role? Role { get; set; }
        public SubModule? SubModule { get; set; }
    }
}