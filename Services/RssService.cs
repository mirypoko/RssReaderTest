using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using Domain;
using Domain.Services.Interfaces;
using Services.Interfaces;

namespace Services
{
    public sealed class RssService : IDisposable, IRssService
    {
        public static List<RssSource> SourcesCache { get; private set; } = new List<RssSource>();

        public IGenericReadRepository<News> News { get; private set; }

        public IGenericReadRepository<RssSource> RssSources { get; private set; }

        private readonly IGenericUnitOfWork _uof;

        private readonly IGenericRepository<News> _newsRepository;

        private readonly IGenericRepository<RssSource> _sourceRepository;

        public RssService(IGenericUnitOfWork uof)
        {
            _uof = uof;
            _newsRepository = _uof.GetRepository<News>();
            _sourceRepository = _uof.GetRepository<RssSource>();
            RssSources = _sourceRepository;
            News = _newsRepository;
        }

        public async Task<ServiceResult> AddSourceAsync(RssSource rssSource)
        {
            try
            {
                _sourceRepository.Add(rssSource);
                var d = await _uof.SaveChangesAsync();
                SourcesCache.Add(rssSource);
                return new ServiceResult(true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> RemoveSource(RssSource rssSource)
        {
            try
            {
                _sourceRepository.Remove(rssSource);
                await _uof.SaveChangesAsync();
                var bufLink = SourcesCache.First(r => r.Id == rssSource.Id);
                SourcesCache.Remove(bufLink);
                return new ServiceResult(true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<List<News>> ReadNewsFromAllSources()
        {
            var newsList = new List<News>();
            foreach (var source in SourcesCache)
            {
                newsList = await ReadNewsAsync(source);
            }
            return newsList;
        }

        public async Task<List<News>> ReadNewsAsync(RssSource source)
        {
            var newsList = new List<News>();
            using (XmlReader FeedReader = XmlReader.Create(source.Url))
            {
                SyndicationFeed Channel = SyndicationFeed.Load(FeedReader);
                if (Channel != null)
                {
                    foreach (SyndicationItem RSI in Channel.Items)
                    {
                        var newsItem = new News()
                        {
                            Title = RSI.Title.Text,
                            Description = RSI.Summary.Text,
                            Date = RSI.PublishDate.DateTime,
                            Url = RSI.Id,
                            RssSourceId = source.Id

                        };
                        await AddNewsToRepositoryIfNotExist(source, newsItem);
                        newsList.Add(newsItem);
                    }
                }
            }
            await _uof.SaveChangesAsync();
            return newsList;
        }

        public async Task<List<RssSource>> LoadSourcesFromDb()
        {
            SourcesCache = await _sourceRepository.ToListAsync();
            return SourcesCache;
        }

        private async Task AddNewsToRepositoryIfNotExist(RssSource source, News newsItem)
        {
            var bufNews =
                await _newsRepository.FirstOrDefaultAsync(n =>
                n.RssSourceId == source.Id &&
                n.Title == newsItem.Title &&
                n.Date == newsItem.Date);
            if (bufNews == null)
                _newsRepository.Add(newsItem);
        }

        #region static


        public void Dispose()
        {
            _uof.Dispose();
        }

        #endregion
    }
}
