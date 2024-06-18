namespace UsersAPI.Domain.Entities
{
    public class SubModule
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public Guid ParentSubModuleId { get; set; }
        public string? SubModuleName { get; set; }
        public DateTime? CreatedAt { get; set; }


        /// <summary>
        /// Relacionamentos entre Entidades
        /// </summary>
        public List<Permission>? Permissions { get; set; }
        public Module? Module { get; set; }
    }
}