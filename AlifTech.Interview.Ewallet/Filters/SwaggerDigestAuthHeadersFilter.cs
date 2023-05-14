using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AlifTech.Interview.Ewallet.Filters;

public class SwaggerDigestAuthHeadersFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if(!string.IsNullOrWhiteSpace(context.ApiDescription.RelativePath) && 
           context.ApiDescription.RelativePath.ToLower() == "digestgenerator/generate") 
        {
           return; 
        }
        
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();
 
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-UserId",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string" 
            }
        });
        
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Digest",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string" 
            }
        });
    }
}