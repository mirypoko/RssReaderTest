using Domain;
using Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRssService
    {
        IGenericReadRepository<News> News { get; }
        IGenericReadRepository<RssSource> RssSources { get; }

        Task<ServiceResult> AddSourceAsync(RssSource rssSource);
        void Dispose();
        Task<List<RssSource>> LoadSourcesFromDb();
        Task<List<News>> ReadNewsAsync(RssSource source);
        Task<List<News>> ReadNewsFromAllSources();
        Task<ServiceResult> RemoveSource(RssSource rssSource);
    }
}