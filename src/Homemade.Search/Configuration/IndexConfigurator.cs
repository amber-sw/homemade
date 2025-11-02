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
            ? Path.Combine("index", "default")
            : Path.Combine("index", name);

        options.Directory = new MMapDirectory(path);
    }
}