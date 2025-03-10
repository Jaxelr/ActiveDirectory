﻿using System.Collections.Generic;
using System.Linq;

namespace ActiveDirectory.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Since the mapping from the DI returns an instantiated class as based on the appsettings.json
    /// config file, we must check if the only element included is not empty, then we can define it as empty.
    /// </summary>
    /// <param name="collection">Determine if the collecton is empty.</param>
    internal static bool Empty(this IEnumerable<string> collection) =>
        collection.Count() <= 1 && string.IsNullOrEmpty(collection.FirstOrDefault());
}
