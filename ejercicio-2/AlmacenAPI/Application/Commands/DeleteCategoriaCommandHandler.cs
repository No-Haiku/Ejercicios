using AlmacenAPI.Commands;
using AlmacenAPI.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AlmacenAPI.Application.Commands
{
    public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand, Unit>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public DeleteCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        
        public async Task<Unit> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            await _categoriaRepository.DeleteCategoriaAsync(request.Id);
            return Unit.Value; 
        }
    }
}
