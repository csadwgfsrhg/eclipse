namespace Eclipse.Utilities.Extensions;

/// <summary>
///     Provides <see cref="Projectile"/> extensions.
/// </summary>
public static class ProjectileExtensions
{
    /// <summary>
    ///     Checks whether a projectile is a lance or not.
    /// </summary>
    /// <param name="projectile">The projectile to check.</param>
    /// <returns><c>true</c> if the projectile is a lance; otherwise, <c>false</c>.</returns>
    public static bool IsLance(this Projectile projectile) {
        return projectile.type == ProjectileID.HallowJoustingLance
            || projectile.type == ProjectileID.JoustingLance
            || projectile.type == ProjectileID.ShadowJoustingLance;
    }
}
