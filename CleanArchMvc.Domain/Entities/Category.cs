using CleanArchMvc.Domain.Validation;
using System.Collections.Generic;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity // Sealed para não ser herdada
    {
        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(name);
        }

        public string Name { get; private set; } // set privado para não atribuir valor externamente 

        public ICollection<Product> Products { get; set; }

        private void ValidateDomain(string name) // Método que valida o nome
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters");

            Name = name;
        }
    
        public void Update(string name)
        {
            ValidateDomain(name);
        }
    }
}