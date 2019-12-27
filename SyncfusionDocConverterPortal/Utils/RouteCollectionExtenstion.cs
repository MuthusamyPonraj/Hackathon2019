using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SyncfusionDocConverterPortal.Utils
{
    public static class RouteCollectionExtenstion
    {
        /// <summary>
        /// map route lower case
        /// </summary>
        /// <param name="routes">route value</param>
        /// <param name="name">route name</param>
        /// <param name="url">uniform resource locator</param>
        /// <param name="defaults">default value</param>
        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRouteLowercase(name, url, defaults, null);
        }

        /// <summary>
        /// map route lower case
        /// </summary>
        /// <param name="routes">route value</param>
        /// <param name="name">route name</param>
        /// <param name="url">uniform resource locator</param>
        /// <param name="defaults">default value</param>
        /// <param name="constraints">constraint value</param>
        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new LowercaseRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if (string.IsNullOrEmpty(name))
            {
                routes.Add(route);
            }
            else
            {
                routes.Add(name, route);
            }
        }
    }
}