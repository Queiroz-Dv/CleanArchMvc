using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity 
    {
        // Construtores
        public Product(string name, string descripition, decimal price, int stock, string image)
        {
            ValidateDomain(name, descripition, price, stock, image);
        }

        public Product(int id, string name, string descripition, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(name, descripition, price, stock, image);
        }

        // Propriedades

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }

        
        // Comportamentos
        private void ValidateDomain(string name, string descripition, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required");


            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters");


            DomainExceptionValidation.When(string.IsNullOrEmpty(descripition),
                "Invalid description. Description is required");

            DomainExceptionValidation.When(descripition.Length < 5,
               "Invalid name, too short, minimum 5 characters");

            DomainExceptionValidation.When(price < 0,
                "Invalid price value");

            DomainExceptionValidation.When(stock < 0,
                "Invalid price value");

            DomainExceptionValidation.When(image.Length > 250,
              "Invalid imnage name, too long, maximum 250 characters");

            Name = name;
            Description = descripition;
            Price = price;
            Stock = stock;
            Image = image;

        }

        public void Update(string name, string descripition, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, descripition, price, stock, image);
            CategoryId = categoryId;
        }

    }
}
