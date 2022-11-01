using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capgemini.Training.Demo.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Booklist()
        {
            var searchText = Request.QueryString["text"];

            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);//sitecore/content/Capgemini/Home
            var bookTemplateId = new Sitecore.Data.ID("{C77CBF36-E660-4C1A-AB94-85F172CA180A}");
            SearchResults<SearchResultItem> results;
            
            using (var context = ContentSearchManager.GetIndex((SitecoreIndexableItem)homeItem).CreateSearchContext())
            {
                IQueryable<SearchResultItem> query = context.GetQueryable<SearchResultItem>();
                query = query.Where(f => f.TemplateId == bookTemplateId && f.Paths.Contains(homeItem.ID)
                                        && f.Language == Sitecore.Context.Language.Name);
                if(!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(f => f.Content.Contains(searchText));
                }
                query = query.OrderByDescending(f => f.CreatedDate)
                  .Page(0, 10);
                results = query.GetResults();
            }

            List<SearchResultItem> resultItems = results?.Hits.Select(f => f.Document).ToList();

            return View("~/Views/Capgemini/Components/Booklist.cshtml", resultItems);
        }
    }
}