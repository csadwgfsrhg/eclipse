namespace Eclipse.Content.Items.Accessories;

public class BouncyTip : ModItem
{
    public override void SetDefaults() {
        Item.accessory = true;

        Item.width = 40;
        Item.height = 40;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) {
        if (!player.TryGetModPlayer(out BouncyTipPlayer modPlayer)) {
            return;
        }

        modPlayer.Enabled = true;
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ItemID.PinkGel, 12)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
