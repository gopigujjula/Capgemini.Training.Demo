using Capgemini.Training.Demo.Models;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using System.Web.UI.WebControls;
using Sitecore.Data.Fields;

namespace Capgemini.Training.Demo.Controllers
{
    public class HeaderController : Controller
    {
        public ActionResult Index()
        {            
            List<Item> navigationItems = new List<Item>();
         
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);//sitecore/content/Capgemini/Home
            if(homeItem != null)
            {
                navigationItems.Add(homeItem);
            }

            if(homeItem?.Children!=null)
            {
                foreach (Item child in homeItem.Children)
                {
                    //var hideInNavigationField = (CheckboxField)child.Fields["Hide in Navigation"];
                    
                    //;if (hideInNavigationField != null && !hideInNavigationField.Checked)
                        navigationItems.Add(child);
                }
            }

            return View("~/Views/Capgemini/Components/Header.cshtml", new HeaderModel
            {
                HeaderDatasource = RenderingContext.Current.Rendering.Item,
                NavigationItems = navigationItems
            });
        }
    }
}