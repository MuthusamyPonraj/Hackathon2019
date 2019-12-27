using System.Web.Routing;

namespace SyncfusionDocConverterPortal.Utils
{
    public class LowercaseRoute : Route
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LowercaseRoute" /> class.
        /// </summary>
        /// <param name="url">uniform resource locator</param>
        /// <param name="routeHandler">route handler</param>
        public LowercaseRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LowercaseRoute" /> class.
        /// </summary>
        /// <param name="url">uniform resource locator</param>
        /// <param name="defaults">default route</param>
        /// <param name="routeHandler">route handler</param>
        public LowercaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LowercaseRoute" /> class.
        /// </summary>
        /// <param name="url">uniform resource locator</param>
        /// <param name="defaults">default value</param>
        /// <param name="constraints">constraints value</param>
        /// <param name="routeHandler">route handler</param>
        public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LowercaseRoute" /> class.
        /// </summary>
        /// <param name="url">uniform resource locator</param>
        /// <param name="defaults">defaults value</param>
        /// <param name="constraints">constraints value</param>
        /// <param name="dataTokens">data tokens</param>
        /// <param name="routeHandler">route handler</param>
        public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        /// <summary>
        /// Get virtual path
        /// </summary>
        /// <param name="requestContext">request context</param>
        /// <param name="values">route value</param>
        /// <returns>route path</returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            System.Web.Routing.VirtualPathData path = base.GetVirtualPath(requestContext, values);

            if (path != null)
            {
                path.VirtualPath = path.VirtualPath.ToLowerInvariant();
            }

            return path;
        }
    }
}