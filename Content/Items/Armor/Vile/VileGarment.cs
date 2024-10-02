namespace Eclipse.Content.Items.Armor.Vile;

[AutoloadEquip(EquipType.Body)]
public class VileGarment : ModItem
{
    public override void SetDefaults() {
        Item.width = 30;
        Item.height = 20;

        Item.defense = 3;

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 1);
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ItemID.Silk, 6)
            .AddIngredient(ItemID.VilePowder, 15)
            .AddIngredient(ItemID.RottenChunk, 7)
            .AddIngredient(ItemID.Leather)
            .AddIngredient(ItemID.WormTooth, 2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
