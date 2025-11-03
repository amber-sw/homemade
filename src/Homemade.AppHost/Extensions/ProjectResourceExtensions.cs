using Aspire.Hosting.Docker.Resources.ServiceNodes;

namespace Homemade.AppHost.Extensions;

/// <summary>
/// Adds extensions for `AddProject` resources.
/// </summary>
public static class ProjectResourceExtensions
{
    /// <summary>
    /// Adds the necessary configuration to add a project mount path.
    /// </summary>
    public static IResourceBuilder<ProjectResource> WithProjectMount(
        this IResourceBuilder<ProjectResource> builder,
        string name,
        string path,
        string host,
        string? description = null,
        bool isReadOnly = false
    )
    {
        var fullHostPath = Path.GetFullPath(host, builder.ApplicationBuilder.AppHostDirectory);
        builder.WithEnvironment($"ConnectionStrings__{name}", fullHostPath);

        if (builder.ApplicationBuilder.ExecutionContext.IsPublishMode)
        {
            var mount = builder.ApplicationBuilder.AddParameter(name)
                .WithDescription(description ?? string.Empty)
                .WithParentRelationship(builder);

            builder
                .PublishAsDockerComposeService((resource, service) =>
                {
                    service.AddEnvironmentalVariable($"ConnectionStrings__{name}", path);
                    service.AddVolume(new Volume
                    {
                        Name = name,
                        Type = "bind",
                        Target = path,
                        Source = mount.AsEnvironmentPlaceholder(resource),
                        ReadOnly = isReadOnly
                    });
                });
        }

        return builder;
    }
}