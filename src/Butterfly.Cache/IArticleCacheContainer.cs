using Butterfly.ServiceModel;

namespace Butterfly.Cache
{
    public interface IArticleCacheContainer
    {
        ArticleInfo[] GetArticlesByPage(Paging paging);

        ArticleInfo GetArticleById(string id);

        bool CreateArticle(ArticleInfo article);

        bool ModifyArticle(ArticleInfo article);
    }
}
