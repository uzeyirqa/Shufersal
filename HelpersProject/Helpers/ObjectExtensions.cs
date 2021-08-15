using System;
namespace HelpersProject.Helpers
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T obj) =>
    obj == null;
    }
}
