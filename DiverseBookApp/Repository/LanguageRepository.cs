using DiverseBookApp.Data;
using DiverseBookApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookAppContext _context = null;

        public LanguageRepository(BookAppContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguage()
        {
            return await _context.Language.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
            }).ToListAsync();
        }
    }
}
