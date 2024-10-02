using Eclipse.Content.Classes;
using Eclipse.Content.Projectiles.Harvester.Crops;

namespace Eclipse.Content.Items.Harvester.Trowels;

public class WoodenTrowel : ModItem
{
    private int charge;

    public override void SetDefaults() {
        Item.autoReuse = true;
        Item.noMelee = true;

        Item.DamageType = DamageClass.Melee;
        Item.damage = 15;
        Item.knockBack = 6f;

        Item.width = 40;
        Item.height = 40;

        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<PotatoCrop>();

        Item.value = Item.sellPrice(gold: 1);
    }

    public override void HoldItem(Player player) {
        if (!player.TryGetModPlayer(out HarvestDamagePlayer modPlayer)) {
            return;
        }

        charge += modPlayer.Cropgrowth / 100;

        /*
        if (charge >= 150) {
            var position = new Vector2(player.position.X + Main.rand.Next(-180, 180), player.position.Y + Main.rand.Next(-180, 180));
            Projectile.NewProjectile(position, );

            charge = 0;
        }
        */
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.Wood, 20)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
