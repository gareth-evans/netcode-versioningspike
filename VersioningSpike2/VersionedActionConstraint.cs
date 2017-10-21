using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace VersioningSpike2
{
    public class VersionedActionConstraint : IActionConstraint
    {
        public bool Accept(ActionConstraintContext context)
        {
            var request = context.RouteContext.HttpContext.Request;

            switch (request.Method)
            {
                case "POST":
                case "PUT":
                    var fromBodyParam = GetFromBodyParameter(context);

                    var mimeTypes = fromBodyParam.ParameterType
                        .GetCustomAttributes(false)
                        .OfType<MimeTypeAttribute>()
                        .SingleOrDefault();

                    var contentType = request.Headers["Content-Type"].FirstOrDefault();

                    return mimeTypes?.MimeType == contentType;
                default:
                    var acceptMimeTypes = request.Headers["Accept"]
                        .SelectMany(x => x.Split(','))
                        .Select(type => type.Trim());

                    var controllerActionDescriptor = context
                        .CurrentCandidate
                        .Action as ControllerActionDescriptor;

                    var producesAttribute = controllerActionDescriptor
                        .MethodInfo
                        .GetCustomAttributes(false)
                        .OfType<ProducesAttribute>()
                        .Single();

                    var mimeType = producesAttribute
                            .Type
                            .GetCustomAttributes(false)
                            .OfType<MimeTypeAttribute>()
                            .Single();

                    return acceptMimeTypes.Contains(mimeType.MimeType);
                    
            }

        }

        private static ControllerParameterDescriptor GetFromBodyParameter(ActionConstraintContext context)
        {
            return context.CurrentCandidate
                .Action
                .Parameters
                .OfType<ControllerParameterDescriptor>()
                .Where(HasFromBodyAttribute)
                .FirstOrDefault();
        }

        private static bool HasFromBodyAttribute(ControllerParameterDescriptor parameterDescriptor)
        {
            var customAttributes = 
                parameterDescriptor
                    .ParameterInfo
                    .GetCustomAttributes(false);

            return customAttributes
                .OfType<FromBodyAttribute>()
                .Any();
        }

        public int Order { get; }
    }
}