using Terraria.DataStructures;

namespace Eclipse.Content.Classes;

public sealed class HarvestDamagePlayer : ModPlayer
{
    public int Cropgrowth = 100;
    public int Hunger;

    public int HungerMax = 50;

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
        if (Hunger > HungerMax) {
            Hunger = HungerMax;
        }

        // TODO: Draw meter under player when harvesting weapon is held
    }
}
