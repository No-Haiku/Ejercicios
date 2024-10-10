using MediatR;

namespace AlmacenAPI.Application.Commands
{
    public class DeleteProductoCommand : IRequest
    {
        public int Id { get; set; }


    }
}
