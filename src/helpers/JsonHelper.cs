using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderFulfilmentService
{
  public static class JsonHelper
  {
    // load the property values from the given json file for the selected property
    public static void GetObjects(string jsonFullPathname, string propertyName, Action<JArray> objectLoader) 
    {
      // read JSON directly from a file
      using (StreamReader file = File.OpenText(jsonFullPathname))
      using (JsonTextReader reader = new JsonTextReader(file))
      {
        var dataJsonObj = (JObject)JToken.ReadFrom(reader);

        var property = dataJsonObj.Property(propertyName);

        var propertyValue = (JArray) property.Value;

        // load the property values
        objectLoader.Invoke(propertyValue);
      }      
    }
  }
}
