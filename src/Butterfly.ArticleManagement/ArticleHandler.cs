using Butterfly.ServiceModel;
using Petecat.DynamicProxy.Attribute;
using Butterfly.Repository;
using Petecat.Extending;
using Petecat.Configuring;
using Butterfly.Configuration;

namespace Butterfly.ArticleManagement
{
    [DynamicProxyInjectable(Inference = typeof(IArticleHandler), Singleton = true)]
    public class ArticleHandler : IArticleHandler
    {
        private IArticleRepository _ArticleRepository;

        private IStaticFileConfigurer _StaticFileConfigurer;

        public ArticleHandler(IArticleRepository articleRepository, IStaticFileConfigurer staticFileConfigurer)
        {
            _ArticleRepository = articleRepository;
            _StaticFileConfigurer = staticFileConfigurer;
        }

        [MethodInterceptor(Type = typeof(IHandlingInterceptor))]
        public virtual ApiResponse GetArticleById(ApiRequest request)
        {
            return new ApiResponse()
            {
                Data = _ArticleRepository.GetArticleById(request.Get<string>("id")),
            };
        }

        [MethodInterceptor(Type = typeof(IHandlingInterceptor))]
        public virtual ApiResponse GetArticlesByPage(ApiRequest request)
        {
            return new ApiResponse()
            {
                Data = new PageArticles()
                {
                    Articles = _ArticleRepository.GetArticlesByPage(request.Paging),
                    Paging = request.Paging,
                },
            };
        }

        [MethodInterceptor(Type = typeof(IHandlingInterceptor))]
        public virtual ApiResponse OperateArticle(ApiRequest<ArticleInfo> request)
        {
            var apiKey = request.Get<string>("apikey");

            if (!_StaticFileConfigurer.GetValue<IApiConfiguration>().ApiKey.EqualsWith(apiKey))
            {
                return new ApiResponse()
                {
                    Error = true,
                    Message = "key is not valid.",
                };
            }

            var action = request.Get<string>("action");

            if (action.EqualsWith("create") && _ArticleRepository.CreateArticle(request.Body))
            {
                return new ApiResponse()
                {
                    Data = new ArticleInfo()
                    {
                        Id = request.Body.Id,
                    },
                };
            }
            else if (action.EqualsWith("modify") && _ArticleRepository.ModifyArticle(request.Body))
            {
                return new ApiResponse()
                {
                    Data = new ArticleInfo()
                    {
                        Id = request.Body.Id,
                    },
                };
            }
            else
            {
                return new ApiResponse()
                {
                    Error = true,
                    Message = "invalid action.",
                };
            }
        }
    }
}