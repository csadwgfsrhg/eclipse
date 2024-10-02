using Eclipse.Utilities.Extensions;

namespace Eclipse.Content.Items.Accessories;

public sealed class BouncyTipPlayer : ModPlayer
{
    /// <summary>
    ///     Whether this player has the effects from <see cref="BouncyTip"/> enabled or not.
    /// </summary>
    public bool Enabled;

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) {
        if (Enabled & proj.IsLance()) {
            Player.velocity = -Player.velocity;
        }

        Enabled = false;
    }
}
