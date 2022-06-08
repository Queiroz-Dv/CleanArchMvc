using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Commands
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public ProductRemoveCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
