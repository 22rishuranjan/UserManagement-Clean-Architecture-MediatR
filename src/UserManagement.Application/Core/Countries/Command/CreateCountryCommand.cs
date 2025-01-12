
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Core.Countries.Command
{
    public class CreateCountryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly ICountryRepository _countryRepository;

        public CreateCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country
            {
                Name = request.Name,
                Code = request.Code
            };

            await _countryRepository.CreateAsync(country);
            return country.Id;
        }
    }


}
