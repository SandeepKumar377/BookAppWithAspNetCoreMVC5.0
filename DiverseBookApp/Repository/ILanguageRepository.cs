using DiverseBookApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguage();
    }
}