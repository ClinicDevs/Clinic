using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.News.Queries;
using Clinic.Domain.Entities;
using MediatR;

namespace Clinic.Application.UseCases.News.Handlers.QueryHandlers
{
    public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, IEnumerable<New>>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public GetAllNewsQueryHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public Task<IEnumerable<New>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
