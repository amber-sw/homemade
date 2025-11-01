using Microsoft.EntityFrameworkCore;

namespace Homemade.Database;

/// <summary>
/// The <see cref="DbContext" /> for the Homemade database.
/// </summary>
public sealed class HomemadeContext(
    DbContextOptions<HomemadeContext> options
) : DbContext(options);