using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capgemini.Training.Demo.Models
{
    public class CarouselModel
    {
        public MvcHtmlString NextButtonText { get; set; }
        public MvcHtmlString PrevButtonText { get; set; }
        public List<SlideModel> Slides { get; set; }
    }
    public class SlideModel
    {
        public MvcHtmlString SlideTitle { get; set; }
        public MvcHtmlString SlideImage { get; set; }
        public MvcHtmlString SlideLink { get; set; }
    }
}