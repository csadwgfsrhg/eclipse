using System.Runtime.CompilerServices;
using Terraria.GameContent;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLance(this Projectile projectile) {
        return projectile.type == ProjectileID.HallowJoustingLance
            || projectile.type == ProjectileID.JoustingLance
            || projectile.type == ProjectileID.ShadowJoustingLance;
    }

    /// <summary>
    ///     Gets the projectile's position as screen coordinates.
    /// </summary>
    /// <param name="projectile">The projectile to get the draw position from.</param>
    /// <param name="centered">Whether the draw position should be calculated using the projectile's center or not.</param>
    /// <returns>The projectile's position converted into screen cordinates.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetDrawPosition(this Projectile projectile, bool centered = true) {
        return (centered ? projectile.Center : projectile.position)
            - Main.screenPosition
            + new Vector2(0f, projectile.gfxOffY)
            + new Vector2(projectile.ModProjectile == null ? 0f : projectile.ModProjectile.DrawOffsetX, 0f);
    }

    /// <summary>
    ///     Gets the projectile's old position at given index as screen coordinates.
    /// </summary>
    /// <param name="projectile">The projectile to get the draw position from.</param>
    /// <param name="i">The index of the current old position.</param>
    /// <param name="centered">Whether the draw position should be calculated using the projectile's center or not.</param>
    /// <returns>The projectile's old draw position at given index converted into screen coordinates.</returns>
    public static Vector2 GetOldDrawPosition(this Projectile projectile, int i, bool centered = true) {
        return projectile.oldPos[i]
            + (centered ? projectile.Size / 2f : Vector2.Zero)
            - Main.screenPosition
            + new Vector2(0f, projectile.gfxOffY)
            + new Vector2(projectile.ModProjectile == null ? 0f : projectile.ModProjectile.DrawOffsetX, 0f);
    }

    /// <summary>
    ///     Gets the projectile's origin offset.
    /// </summary>
    /// <param name="projectile">The projectile to get the origin offset from.</param>
    /// <returns>The projectile's origin offset.</returns>
    public static Vector2 GetDrawOriginOffset(this Projectile projectile) {
        return projectile.ModProjectile == null
            ? Vector2.Zero
            : new Vector2(projectile.ModProjectile.DrawOriginOffsetX, projectile.ModProjectile.DrawOriginOffsetY);
    }

    /// <summary>
    ///     Gets the projectile's source rectangle.
    /// </summary>
    /// <param name="projectile">The projectile to get the source rectangle from.</param>
    /// <returns>The projectile's source rectangle.</returns>
    public static Rectangle GetDrawFrame(this Projectile projectile) {
        return TextureAssets.Projectile[projectile.type].Value.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
    }
}
