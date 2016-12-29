using Butterfly.ServiceModel;

namespace Butterfly.Repository
{
    public interface IArticleRepository
    {
        ArticleInfo[] GetArticlesByPage(Paging paging);

        ArticleInfo GetArticleById(string id);

        bool CreateArticle(ArticleInfo article);

        bool ModifyArticle(ArticleInfo article);

        bool DeleteArticle(ArticleInfo article);
    }
}