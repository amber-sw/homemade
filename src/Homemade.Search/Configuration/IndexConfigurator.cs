using Homemade.Database.Entities;

using Lucene.Net.Facet;
using Lucene.Net.Store;

using Lucent.Configuration;

using Microsoft.Extensions.Options;

namespace Homemade.Search.Configuration;

/// <summary>
/// Configures the index file path for the <see cref="IndexConfiguration" />.
/// </summary>
public sealed class IndexConfigurator : IConfigureNamedOptions<IndexConfiguration>
{
    /// <inheritdoc />
    public void Configure(IndexConfiguration options)
        => Configure(null, options);

    /// <inheritdoc />
    public void Configure(string? name, IndexConfiguration options)
    {
        var path = string.IsNullOrWhiteSpace(name)
            ? Path.Combine("index", "default", "documents")
            : Path.Combine("index", name, "documents");
        var facetsPath = string.IsNullOrWhiteSpace(name)
            ? Path.Combine("index", "default", "facets")
            : Path.Combine("index", name, "facets");

        options.Directory = new MMapDirectory(path);
        options.FacetsDirectory = new MMapDirectory(facetsPath);

        options.FacetsConfig = new FacetsConfig();
        options.FacetsConfig.SetMultiValued(nameof(Recipe.Tags), true);
        options.FacetsConfig.SetMultiValued(nameof(Recipe.Ingredients), true);
    }
}