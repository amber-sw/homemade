using Microsoft.EntityFrameworkCore;

namespace Homemade.Database;

public sealed class HomemadeContext(
    DbContextOptions<HomemadeContext> options
) : DbContext(options);