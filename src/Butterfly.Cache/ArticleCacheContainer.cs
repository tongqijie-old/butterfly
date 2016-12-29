using Butterfly.ServiceModel;
using Petecat.Monitor;
using Petecat.Extending;
using System.Linq;
using System.IO;
using System;
using Petecat.Formatter;
using Petecat.DependencyInjection.Attribute;

namespace Butterfly.Cache
{
    [DependencyInjectable(Inference = typeof(IArticleCacheContainer), Singleton = true)]
    public class ArticleCacheContainer : CacheContainer<ArticleInfo>, IArticleCacheContainer
    {
        private IJsonFormatter _JsonFormatter;

        public ArticleCacheContainer(IFileSystemMonitor fileSystemMonitor, IJsonFormatter jsonFormatter)
            : base(fileSystemMonitor, "./data".FullPath())
        {
            _JsonFormatter = jsonFormatter;
        }

        public ArticleInfo[] GetArticlesByPage(Paging paging)
        {
            var total = Select().Length;
            paging.TotalPages = total / paging.PageSize + ((total % paging.PageSize) == 0 ? 0 : 1);

            return Select().OrderByDescending(x => x.CreationDate.ConvertTo<DateTime>())
                .Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToArray();
        }

        public ArticleInfo GetArticleById(string id)
        {
            var articles = Select(x => x.Id.EqualsWith(id));
            if (articles.Length > 0)
            {
                return articles[0];
            }
            else
            {
                return null;
            }
        }

        public bool CreateArticle(ArticleInfo article)
        {
            if (article.Id.HasValue())
            {
                return false;
            }

            var random = Path.GetRandomFileName();
            article.Id = random.Substring(0, random.IndexOf('.'));
            article.CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            article.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _JsonFormatter.WriteObject(article, Path.Combine(Folder, article.Id + ".json"));
            return true;
        }

        public bool ModifyArticle(ArticleInfo article)
        {
            if (!article.Id.HasValue())
            {
                return false;
            }

            var articles = Select(x => x.Id.EqualsWith(article.Id));
            if (articles.Length == 0)
            {
                return false;
            }

            var clone = articles[0].DeepCopy<ArticleInfo>();
            clone.Title = article.Title;
            clone.Abstract = article.Abstract;
            clone.Content = article.Content;
            clone.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _JsonFormatter.WriteObject(clone, Path.Combine(Folder, clone.Id + ".json"));
            return true;
        }
    }
}