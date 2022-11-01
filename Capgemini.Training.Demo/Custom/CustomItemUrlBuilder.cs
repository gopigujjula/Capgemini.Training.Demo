using Sitecore.Links.UrlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Buckets.Extensions;
using Sitecore.Buckets.Managers;
using Sitecore.IO;

namespace Capgemini.Training.Demo.Custom
{
    public class CustomItemUrlBuilder : ItemUrlBuilder
    {
        public CustomItemUrlBuilder(DefaultItemUrlBuilderOptions defaultOptions) : base(defaultOptions)
        {
        }

        public override string Build(Item item, ItemUrlBuilderOptions options)
        {
            ///Books/2022/10/28/08/19/Capgemini Book
            if(BucketManager.IsItemContainedWithinBucket(item))
            {
                var bucketItem = item.GetParentBucketItemOrParent();//Books
                if (bucketItem != null && bucketItem.IsABucket())
                {
                    //capgeminidemo.local/books
                    var bucketUrl = base.Build(bucketItem, options);

                    //capgeminidemo.local/books/Capgemini Book
                    return FileUtil.MakePath(bucketUrl, item.Name.Replace(' ', '-'));
                }

            }
            return base.Build(item, options);
        }
    }
}