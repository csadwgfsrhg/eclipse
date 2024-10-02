namespace Eclipse.Content.Items.Armor.Vile;

[AutoloadEquip(EquipType.Head)]
public class VileHood : ModItem
{
    public override void SetStaticDefaults() {
        ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
    }

    public override void SetDefaults() {
        Item.width = 26;
        Item.height = 28;

        Item.defense = 2;

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 1);
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ItemID.VilePowder, 5)
            .AddIngredient(ItemID.RottenChunk, 3)
            .AddIngredient(ItemID.Silk, 4)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
