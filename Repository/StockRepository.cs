using backend.Data;
using backend.Dtos.Stock;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync((stock) => stock.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);

            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockModel)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync((stock) => stock.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.CompanyName = stockModel.CompanyName;
            existingStock.Industry = stockModel.Industry;
            existingStock.LastDiv = stockModel.LastDiv;
            existingStock.MarketCap = stockModel.MarketCap;
            existingStock.Purchase = stockModel.Purchase;
            existingStock.Symbol = stockModel.Symbol;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}