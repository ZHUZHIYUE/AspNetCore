using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Server.Kestrel.Core.Internal
{
    public class KestrelHttpContextFactory : HttpContextFactory
    {
        public KestrelHttpContextFactory(IOptions<FormOptions> formOptions)
            : this(formOptions, httpContextAccessor: null)
        {
        }
    

        public KestrelHttpContextFactory(IOptions<FormOptions> formOptions, IHttpContextAccessor httpContextAccessor) : base(formOptions, httpContextAccessor)
        {
        }

        protected override HttpContext CreateHttpContext(IFeatureCollection featureCollection)
        {
            if (featureCollection is HttpProtocol protocol)
            {
                // First request, the HttpContext is null, so make a new one
                if (protocol.HttpContext == null)
                {
                    protocol.HttpContext = new DefaultHttpContext(featureCollection);
                }
                else
                {
                    // Otherwise just initialize the feature collection on the existing connection
                    protocol.HttpContext.Initialize(featureCollection);
                }

                return protocol.HttpContext;
            }

            // Since Kestrel is registered by default, we need to fallback to the default behavior
            return base.CreateHttpContext(featureCollection);
        }
    }
}
