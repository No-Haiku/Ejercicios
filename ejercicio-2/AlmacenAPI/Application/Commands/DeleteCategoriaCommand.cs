using MediatR;

namespace AlmacenAPI.Commands
{
    public class DeleteCategoriaCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
