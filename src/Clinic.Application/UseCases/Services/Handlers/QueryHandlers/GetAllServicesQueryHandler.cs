using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.Services.Queries;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Services.Handlers.QueryHandlers
{
    public class GetAllServicesQueryHandler(IClinincDbContext clinincDbContext, IDistributedCache distributedCache) : IRequestHandler<GetAllServicesQuery, IEnumerable<Service>>
    {
        private readonly IDistributedCache _distributedCache = distributedCache;
        private readonly IClinincDbContext _clinincDbContext = clinincDbContext;

        public async Task<IEnumerable<Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _distributedCache.GetStringAsync("Services");
                if (data == null)
                {
                    var value = await _clinincDbContext.Services.Where(s => s.IsDeleted == false).Skip(request.PageIndex - 1).Take(request.Size).ToListAsync();

                    var option = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        // Other serialization options can be set here
                    };

                    await _distributedCache.SetStringAsync(
                        key: "Services",
                        value: JsonSerializer.Serialize(value, option),
                        options: new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                            SlidingExpiration = TimeSpan.FromSeconds(20)
                        }
                    );
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    // Other serialization options can be set here
                };

                return JsonSerializer.Deserialize<Service[]>(data, options);
            }
            catch (Exception ex)
            {
                throw new Exception(message: "Somthing went wrong in Backend)", ex);
            }
        }
    }
}
