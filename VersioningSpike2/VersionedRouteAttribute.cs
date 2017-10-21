using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace VersioningSpike2
{
    public class VersionedRouteAttribute : RouteAttribute,
        IActionConstraintFactory
    {
        public VersionedRouteAttribute(string template) : base(template)
        {
        }

        public IActionConstraint CreateInstance(IServiceProvider services)
        {
            return new VersionedActionConstraint();
        }

        public bool IsReusable => true;
    }
}