using System.IO;
using Eclipse.Content.Projectiles.Harvester.Fish;
using Terraria.Audio;
using Terraria.ModLoader.IO;

namespace Eclipse.Common.Projectiles;

/// <summary>
///     Handles the behavior of fishing rod bobbers, which should latch onto enemies and spawn phantom
///     fishes accordingly.
/// </summary>
public sealed class BobberGlobalProjectile : GlobalProjectile
{
    public const float IdleState = 0f;
    public const float StickingToNPCState = 1f;
    public const float StickingToFishState = 2f;

    public float Intensity { get; private set; }

    public float State { get; private set; }

    /// <summary>
    ///     The index of the NPC target of the projectile attached to this global.
    /// </summary>
    public int TargetIndex { get; private set; } = -1;

    /// <summary>
    ///     The index of the phantom fish of the projectile attached to this global.
    /// </summary>
    public int ChildIndex { get; private set; } = -1;

    public override bool InstancePerEntity { get; } = true;

    private Vector2 offset;

    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) {
        return entity.bobber;
    }

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter) {
        binaryWriter.Write(State);
        binaryWriter.Write(Intensity);

        binaryWriter.Write(TargetIndex);
        binaryWriter.Write(ChildIndex);

        binaryWriter.WriteVector2(offset);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader) {
        State = binaryReader.ReadSingle();
        Intensity = binaryReader.ReadSingle();

        TargetIndex = binaryReader.Read7BitEncodedInt();
        ChildIndex = binaryReader.Read7BitEncodedInt();

        offset = binaryReader.ReadVector2();
    }

    public override void AI(Projectile projectile) {
        var isReturning = projectile.ai[0] >= 1f;

        switch (State) {
            case IdleState:
                if (!isReturning) {
                    UpdateTarget(projectile);
                }
                break;
            case StickingToNPCState:
                if (!isReturning) {
                    UpdateNPCStick(projectile);
                    UpdateIntensity(projectile);
                }

                UpdateReeling(projectile);
                break;
            case StickingToFishState:
                if (!isReturning) {
                    UpdateFishStick(projectile);
                }
                break;
        }
    }

    private void UpdateNPCStick(Projectile projectile) {
        if (TargetIndex == -1) {
            return;
        }

        var target = Main.npc[TargetIndex];

        if (target.active) {
            projectile.position.X = target.position.X;
            projectile.position.Y = MathHelper.Lerp(projectile.position.Y, target.Top.Y - projectile.height / 2f, 0.2f);

            projectile.gfxOffY = target.gfxOffY;

            projectile.rotation = target.rotation;
        }
        else {
            projectile.ai[0] = 1f;
        }

        projectile.tileCollide = false;

        projectile.velocity *= 0.25f;
    }

    private void UpdateFishStick(Projectile projectile) {
        if (ChildIndex == -1) {
            return;
        }

        var child = Main.projectile[ChildIndex];

        if (child.active) {
            projectile.Center = child.Center;
        }
        else {
            projectile.ai[0] = 1f;
        }

        projectile.tileCollide = false;

        projectile.velocity *= 0.25f;
    }

    private void UpdateReeling(Projectile projectile) {
        if (Intensity <= 660f) {
            return;
        }

        if (Main.rand.NextBool(50)) {
            projectile.position.Y += 4f;

            for (var i = 0; i < 100; i++) {
                var dust = Dust.NewDustDirect(
                    new Vector2(projectile.position.X - 6f, projectile.position.Y - 10f),
                    projectile.width + 12,
                    24,
                    Dust.dustWater()
                );
                
                dust.velocity.Y -= 4f;
                dust.velocity.X *= 2.5f;
                dust.scale = 0.8f;
                dust.alpha = 100;
                dust.noGravity = true;
            }

            SoundEngine.PlaySound(in SoundID.SplashWeak, projectile.Center);
        }

        if (Main.mouseLeft && Main.mouseLeftRelease) {
            var child = Projectile.NewProjectileDirect(
                projectile.GetSource_FromAI(),
                projectile.Center,
                projectile.velocity,
                ModContent.ProjectileType<SpiritAngler>(),
                10,
                2f,
                projectile.owner
            );

            child.localAI[0] = projectile.whoAmI;

            State = StickingToFishState;
            Intensity = 0f;

            ChildIndex = child.whoAmI;

            projectile.ai[0] = 0f;

            projectile.netUpdate = true;
        }
    }

    private void UpdateIntensity(Projectile projectile) {
        var owner = Main.player[projectile.owner];
        var level = owner.GetFishingConditions().FinalFishingLevel;

        if (Main.rand.Next(300) < level) {
            Intensity += Main.rand.Next(1, 3);
        }

        Intensity += level / 30f;
        Intensity += Main.rand.Next(1, 3);

        if (Main.rand.NextBool(60)) {
            Intensity += 60f;
        }
    }

    private void UpdateTarget(Projectile projectile) {
        foreach (var target in Main.ActiveNPCs) {
            if (!target.Hitbox.Intersects(projectile.Hitbox)) {
                continue;
            }

            State = StickingToNPCState;

            TargetIndex = target.whoAmI;

            offset = (target.Center - projectile.Center) * 0.75f;

            projectile.netUpdate = true;

            break;
        }
    }
}
