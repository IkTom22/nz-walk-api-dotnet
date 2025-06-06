using Microsoft.EntityFrameworkCore.Update.Internal;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    // Definition od the method 
    public interface IWalkRepository
    {
        //return type Walk
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}