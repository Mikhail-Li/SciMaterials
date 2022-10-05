using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SciMaterials.API.Helpers;

public class FileUploadFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var form_parameters = context.ApiDescription.ParameterDescriptions
            .Where(paramDesc => paramDesc.IsFromForm());

        if (form_parameters.Any())
        {
            // already taken care by swashbuckle. no need to add explicitly.
            return;
        }
        if (operation.RequestBody != null)
        {
            // NOT required for form type
            return;
        }
        if (context.ApiDescription.HttpMethod == HttpMethod.Post.Method)
        {
            var uploadFileMediaType = new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Type = "object",
                    Properties =
                    {
                        ["files"] = new OpenApiSchema
                        {
                            Type = "array",
                            Items = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        }
                    },
                    Required = new HashSet<string> { "files" }
                }
            };

            operation.RequestBody = new OpenApiRequestBody
            {
                Content = { ["multipart/form-data"] = uploadFileMediaType }
            };
        }
    }
}

public static class Helper
{
    internal static bool IsFromForm(this ApiParameterDescription ApiParameter)
    {
        var source = ApiParameter.Source;
        var element_type = ApiParameter.ModelMetadata?.ElementType;

        return source == BindingSource.Form
            || source == BindingSource.FormFile
            || (element_type != null && typeof(IFormFile).IsAssignableFrom(element_type));
    }
}