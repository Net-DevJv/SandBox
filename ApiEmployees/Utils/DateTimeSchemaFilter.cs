using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiEmployees.Utils
{
    public class DateTimeSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.MemberInfo != null)
            {
                if (context.MemberInfo.Name == "CreationDate"
                    || context.MemberInfo.Name == "UpdateDate")
                {
                    schema.Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }
    }
}
