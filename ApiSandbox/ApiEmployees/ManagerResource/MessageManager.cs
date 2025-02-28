using System.Resources;
using System.Reflection;

namespace ApiEmployees.ManagerResource
{
    public class MessageManager
    {
        private static readonly ResourceManager resourceManager = new ResourceManager("ApiEmployees.Resources.ApiMessages", Assembly.GetExecutingAssembly());
        public static string GetMessage(string key)
        {
            return resourceManager.GetString(key);
        }
    }
}
