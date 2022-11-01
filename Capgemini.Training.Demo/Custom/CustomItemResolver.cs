using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;

namespace Capgemini.Training.Demo.Custom
{
    public class CustomItemResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            //http://capgemini.dev.local/Books/Capgemini-Book
            if (Sitecore.Context.Item == null)
            {
                ///Books/Capgemini-Book
                var requestUrl = args.Url.ItemPath;
                var index = requestUrl.LastIndexOf('/');

                if (index > 0)
                {
                    var bookTemplateId = new Sitecore.Data.ID("{C77CBF36-E660-4C1A-AB94-85F172CA180A}");
                    var itemName = requestUrl.Substring(index + 1).Replace("-", " ");//Capgemini Book

                    using (var context = ContentSearchManager.GetIndex("sitecore_web_index").CreateSearchContext())
                    {
                        var result = context.GetQueryable<SearchResultItem>().
                            Where(f => f.TemplateId == bookTemplateId
                                        && f.Language == Sitecore.Context.Language.Name
                                        && f.Name == itemName).FirstOrDefault();
                        if (result != null)
                            Sitecore.Context.Item = result.GetItem();
                    }
                }
            }
        }
    }
}