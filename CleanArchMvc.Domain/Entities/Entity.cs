namespace CleanArchMvc.Domain.Entities
{
    public abstract class Entity
    {
        // Classe usada para algo em comum entre outras classes
        public int Id { get; protected set; }
    }
}
