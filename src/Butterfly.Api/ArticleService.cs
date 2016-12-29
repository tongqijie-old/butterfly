using Butterfly.ArticleManagement;
using Butterfly.ServiceModel;
using Petecat.HttpServer;
using Petecat.HttpServer.Attribute;

namespace Butterfly.Api
{
    [RestServiceInjectable(ServiceName = "as", Singleton = true)]
    public class ArticleService
    {
        private IArticleHandler _ArticleHandler;

        public ArticleService(IArticleHandler articleHandler)
        {
            _ArticleHandler = articleHandler;
        }

        [RestServiceMethod(MethodName = "gabp", HttpVerb = HttpVerb.Get)]
        public ApiResponse GetArticlesByPage([ParameterInfo(Alias = "pn")] int pageNumber)
        {
            return _ArticleHandler.GetArticlesByPage(new ApiRequest()
            {
                Paging = new Paging() { PageNumber = pageNumber, PageSize = 10 },
            });
        }

        [RestServiceMethod(MethodName = "gabi", HttpVerb = HttpVerb.Get)]
        public ApiResponse GetArticleById([ParameterInfo(Alias = "id")] string articleId)
        {
            return _ArticleHandler.GetArticleById(new ApiRequest()
            {
                Items = new KeyValueItem[]
                {
                    new KeyValueItem() { Key = "id", Value = articleId }
                },
            });
        }

        [RestServiceMethod(MethodName = "oa", HttpVerb = HttpVerb.Post)]
        public ApiResponse OperateArticle(
            [ParameterInfo(Source = ParameterSource.Body)] ArticleInfo article, 
            [ParameterInfo(Source = ParameterSource.QueryString)] string action,
            [ParameterInfo(Alias = "key", Source = ParameterSource.QueryString)] string apiKey)
        {
            return _ArticleHandler.OperateArticle(new ApiRequest<ArticleInfo>()
            {
                Body = article,
                Items = new KeyValueItem[]
                {
                    new KeyValueItem() { Key = "action", Value = action },
                    new KeyValueItem() { Key = "apikey", Value = apiKey },
                },
            });
        }
    }
}