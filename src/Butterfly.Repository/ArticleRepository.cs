using System;
using Butterfly.ServiceModel;
using Butterfly.Cache;
using Petecat.DependencyInjection.Attribute;

namespace Butterfly.Repository
{
    [DependencyInjectable(Inference = typeof(IArticleRepository), Singleton = true)]
    public class ArticleRepository : IArticleRepository
    {
        private IArticleCacheContainer _ArticleCacheContainer;

        public ArticleRepository(IArticleCacheContainer articleCacheContainer)
        {
            _ArticleCacheContainer = articleCacheContainer;
        }

        public bool CreateArticle(ArticleInfo article)
        {
            return _ArticleCacheContainer.CreateArticle(article);
        }

        public bool DeleteArticle(ArticleInfo article)
        {
            throw new NotImplementedException();
        }

        public ArticleInfo GetArticleById(string id)
        {
            return _ArticleCacheContainer.GetArticleById(id);
        }

        public ArticleInfo[] GetArticlesByPage(Paging paging)
        {
            return _ArticleCacheContainer.GetArticlesByPage(paging);
        }

        public bool ModifyArticle(ArticleInfo article)
        {
            return _ArticleCacheContainer.ModifyArticle(article);
        }
    }
}