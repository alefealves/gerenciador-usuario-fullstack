namespace UsersAPI.Domain.Entities
{
    public class Module
    {
        public Guid Id { get; set; }
        public string? ModuleName { get; set; }
        public DateTime? CreatedAt { get; set; }


        /// <summary>
        /// Relacionamento entre Entidades
        /// </summary>
        public List<SubModule>? SubModules { get; set; }
    }
}