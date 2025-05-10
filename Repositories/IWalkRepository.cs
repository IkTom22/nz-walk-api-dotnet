using Microsoft.EntityFrameworkCore.Update.Internal;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    // Definition od the method 
    public interface IWalkRepository
    {
        //return type Walk
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}