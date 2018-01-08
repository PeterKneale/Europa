using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolrNet;
using System.Collections.Generic;
using SolrNet.Commands.Parameters;
using System;

namespace Europa.Search.Index
{
    public interface IIndexer
    {
        Task Update(PodcastDocument document);
        Task Update(IEnumerable<PodcastDocument> documents);
    }

    public interface ISearcher
    {
        Task<IEnumerable<PodcastDocument>> Search(string query);
    }

    public class Indexer : IIndexer, ISearcher
    {
        private readonly ISolrOperations<PodcastDocument> _solrPodcasts;
        private readonly ILogger<Indexer> _log;

        public Indexer(ISolrOperations<PodcastDocument> solrPodcasts, ILogger<Indexer> log)
        {
            _solrPodcasts = solrPodcasts;
            _log = log;
        }
        
        public async Task Update(IEnumerable<PodcastDocument> documents)
        {
            foreach (var document in documents)
            {
                await DoUpdate(document);
            }
            await _solrPodcasts.CommitAsync();
        }

        public async Task Update(PodcastDocument document)
        {
            await DoUpdate(document);
            await _solrPodcasts.CommitAsync();
        }

        public async Task<IEnumerable<PodcastDocument>> Search(string query)
        {
            var response = await _solrPodcasts.QueryAsync(SolrQuery.All, new QueryOptions
            {
                Rows = 0,
                Facet = new FacetParameters
                {
                    Queries = new[] {
                        new SolrFacetFieldQuery("category"),
                        new SolrFacetFieldQuery("tags")
                    }
                }
            });
            foreach (var facet in response.FacetFields["category"])
            {
                Console.WriteLine("Category {0}: ({1} matches)", facet.Key, facet.Value);
            }
            foreach (var facet in response.FacetFields["tags"])
            {
                Console.WriteLine("Tag {0}: ({1} matches)", facet.Key, facet.Value);
            }
            return response;
        }
        
        private async Task DoUpdate(PodcastDocument document)
        {
            var json = JsonConvert.SerializeObject(document);
            var response = await _solrPodcasts.AddAsync(document);
            if (response.Status == 0)
            {
                await _solrPodcasts.CommitAsync();
                _log.LogInformation($"Indexed podcast: {json}");
            }
            else
            {
                var details = string.Join(";", response.Params.Select(x => x.Key + "=" + x.Value).ToArray());
                _log.LogError($"Error indexing podcast: {json}. Response Status: {response.Status}. Details: {details}");
            }
        }
    }
}