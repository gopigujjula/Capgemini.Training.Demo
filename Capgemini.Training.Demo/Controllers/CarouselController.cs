using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capgemini.Training.Demo.Models;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace Capgemini.Training.Demo.Controllers
{
    public class CarouselController : Controller
    {
        // GET: Carousel
        public ActionResult Index()
        {
            var datasource = RenderingContext.Current.Rendering?.Item;//carousel item
            //var nextText = datasource.Fields["Next Button Text"].Value;
            //var previousText = datasource.Fields["Previous Button Text"].Value;
            
            var nextText = new MvcHtmlString(FieldRenderer.Render(datasource, "Next Button Text"));
            var previousText = new MvcHtmlString(FieldRenderer.Render(datasource, "Previous Button Text"));

            var slideCountParameter = RenderingContext.Current.Rendering.Parameters["SlideCount"];
            int.TryParse(slideCountParameter, out int result);            

            MultilistField slidesField = datasource?.Fields["Slides"];
            var slideItems = slidesField?.GetItems();//carousel slide items
            int slideCount = result == 0 ? slideItems.Count() : result;

            List<SlideModel> slides = new List<SlideModel>();
            //Sitecore.Context.Language
            foreach (var slideItem in slideItems?.Take(slideCount))
            {
                slides.Add(new SlideModel
                {
                    SlideTitle = new MvcHtmlString(FieldRenderer.Render(slideItem, "Slide Text")),
                    SlideImage = new MvcHtmlString(FieldRenderer.Render(slideItem, "Slide Image", "class=w-100")),
                    SlideLink = new MvcHtmlString(FieldRenderer.Render(slideItem, "Slide Link", "class=btn btn-primary py-md-3 px-md-5 mt-2"))
                });
            }

            return View("~/Views/Capgemini/Components/Carousel.cshtml",
                new CarouselModel
                {
                    NextButtonText = nextText,
                    PrevButtonText = previousText,
                    Slides = slides
                });
        }
    }
}