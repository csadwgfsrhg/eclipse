﻿
using Eclipse.Common.Items;

namespace Eclipse.Content.Items.Harvester.Scythes
{

        public class ShadewoodScythe : ScytheAI
        {
        public override void SetStaticDefaults()
        {

            Item.width = 42;
            Item.damage = 12;
            Item.height = 38;
            Item.rare = 0;
            Item.shoot = ModContent.ProjectileType<ShadewoodScytheProj>();
            Item.useTime = 40;
            Item.useAnimation = 40;

        }




        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shadewood, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
    public class ShadewoodScytheProj : ScytheProj
    {
        public override string Texture => "Eclipse/Content/Items/Harvester/Scythes/ShadewoodScythe";
        public override void SetStaticDefaults()
        {
     
            Projectile.width = 42;
            Projectile.height = 38;
            Projectile.timeLeft = 40;


        }
       

    }
}