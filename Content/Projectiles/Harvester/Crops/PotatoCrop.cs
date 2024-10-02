namespace Eclipse.Content.Projectiles.Harvester.Crops;

public class PotatoCrop : ModProjectile
{
    private int cropgrowth;

    public override void SetDefaults() {
        Projectile.sentry = true;
        Projectile.friendly = false;
        Projectile.tileCollide = true;
        Projectile.usesLocalNPCImmunity = true;

        Projectile.width = 38;
        Projectile.height = 36;

        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;

        Projectile.timeLeft = 1000;
    }

    public override bool OnTileCollide(Vector2 oldVelocity) {
        return false;
    }

    public override void AI() {
        cropgrowth += 1;
    }
}
