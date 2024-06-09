using backend.Dtos.Stock;
using backend.Models;

namespace backend.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockModel);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExistsAsync(int id);
    }
}