using CinemaBookingAPI.Exceptions;
using CinemaBookingAPI.Models.DTOs;
using CinemaBookingAPI.Models.Entities;
using CinemaBookingAPI.Repositories.Interfaces;
using CinemaBookingAPI.Services.Interfaces;

namespace CinemaBookingAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id)
                ?? throw new CustomerNotFoundException($"Customer with id {id} not found.");
            return MapToDto(item);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            await _repository.AddAsync(customer);
            await _repository.SaveChangesAsync();

            return MapToDto(customer);
        }

        private static CustomerDto MapToDto(Customer c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email
        };
    }
}