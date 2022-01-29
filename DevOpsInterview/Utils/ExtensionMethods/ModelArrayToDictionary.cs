using System.Collections.Generic;
using System.Linq;
using DevOpsInterview.Models;

namespace DevOpsInterview.Utils.ExtensionMethods
{
    public static class ModelArrayToDictionary
    {
        public static IDictionary<int, T> ToIdDictionary<T>(this IEnumerable<T> list) where T : IModel
        {
            return list.Aggregate(
                new Dictionary<int, T>(),
                (dictionary, model) =>
                {
                    dictionary[model.Id] = model;
                    return dictionary;
                });

        }
    }
}