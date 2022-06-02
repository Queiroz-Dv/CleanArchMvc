using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
            // Herda de Entity por causa de propriedades similares com Category
    public sealed class Product : Entity 
    {
            // Construtores
        public Product(string name, string descripition, decimal price, int stock, string image)
        {
            ValidateDomain(name, descripition, price, stock, image);
        }

        public Product(int id, string name, string descripition, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
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
            // Validações

            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required.");


            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");


            DomainExceptionValidation.When(string.IsNullOrEmpty(descripition),
                "Invalid description. Description is required.");

            DomainExceptionValidation.When(descripition.Length < 5,
               "Invalid name, too short, minimum 5 characters.");

            DomainExceptionValidation.When(price < 0,
                "Invalid price value.");

            DomainExceptionValidation.When(stock < 0,
                "Invalid stock value.");
            // Image com operador de null condicional
            DomainExceptionValidation.When(image?.Length > 250,
              "Invalid imnage name, too long, maximum 250 characters.");
            
               // Set para os parâmetros 
            SetValues(name, descripition, price, stock, image);

        }

        private void SetValues(string name, string descripition, decimal price, int stock, string image)
        {
            SetName(name);
            SetDescription(descripition);
            SetPrice(price);
            SetStock(stock);
            SetImage(image);
        }

        private void SetImage(string image)
        {
            Image = image;
        }

        private void SetStock(int stock)
        {
            Stock = stock;
        }

        private void SetPrice(decimal price)
        {
            Price = price;
        }

        private void SetDescription(string descripition)
        {
            Description = descripition;
        }

        private void SetName(string name)
        {
            Name = name;
        }

       
        public void Update(string name, string descripition, decimal price, int stock, string image, int categoryId)
        {
                // Atualizar registro

            ValidateDomain(name, descripition, price, stock, image);
            CategoryId = categoryId;
        }

    }
}
