using CinemaBookingAPI.Models.Entities;

namespace CinemaBookingAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task<bool> SaveChangesAsync();
    }
}