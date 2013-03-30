using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Commandly.Web
{
    public static class Helpers
    {
   
        public static IEnumerable<Tuple<String, String, Object>> ToInputModel(this object model)
        {
            return from property in model.GetType().GetProperties()
                   select Tuple.Create(
                       property.Name, property.PropertyType.GetInputType(), property.GetValue(model, null));
        }

        public static string GetInputType(this Type type)
        {
            if (type == typeof (DateTime)) return "datetime";
            if (type == typeof (bool)) return "checkbox";
            return "text";
        }
    }
}