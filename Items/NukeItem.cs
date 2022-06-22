using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class NukeItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("First Strike Controller");
            Tooltip.SetDefault("'OK, buddy, now you've gone too far...'\n" +
                "First strike capabilites\n" +
                "Summons a nuke\n" +
                "[c/AB40FF:Can destroy dungeon bricks, desert fossil, and lihzahrd temple blocks]");
        }

        public override void SetDefaults()
        {
            Item.damage = 0;     //The damage stat for the Weapon.
            Item.width = 20;    //sprite width
            Item.height = 20;   //sprite height
            Item.maxStack = 1;   //This defines the items max stack
            Item.consumable = true;  //Tells the game that this should be used up once fired
            Item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            Item.rare = 11;  //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 20;  //How long the item is used for.
                                     // item.useTime = 20;	 //How fast the item is used.
            Item.value = Item.buyPrice(3, 25, 0, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.shoot = ModContent.ProjectileType<NukeProjectilePhase2>(); //This defines what type of projectile this item will shoot
            Item.shootSpeed = 5f; //This defines the projectile speed when shot
                                  //item.createTile = mod.TileType("ExplosiveTile");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GiganticExplosiveItem>(), 10);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 25);
            recipe.AddIngredient(ItemID.Wire, 50);
            recipe.AddIngredient(ItemID.ShroomiteBar, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (ExtraExplosives.NukeActive == false && ExtraExplosives.NukeActivated == false)
            {
                int vel;
                int pos;

                //Main.NewText(player.position.X / 16);
                //Main.NewText((Main.maxTilesX * 16));

                if (player.position.X / 16 >= Main.maxTilesX / 2)
                {
                    pos = (Main.maxTilesX * 16) - 700;
                    vel = -50;

                    //Main.NewText("Right");
                }
                else
                {
                    pos = (int)(Main.maxTilesX / 16.0f);
                    vel = 50;

                    //Main.NewText("Left");
                }
                //int yPosition = (int)(y + position.Y / 16.0f);

                ExtraExplosives.NukeActivated = true;

                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    ModPacket myPacket = Mod.GetPacket();
                    myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkNukeActivated);
                    myPacket.Send();
                }

                //SpawnProjectileSynced(new Vector2(xPosition, 1500), new Vector2(30, 0), type, 0, 0, player.whoAmI);
                Projectile.NewProjectile(pos, 1500, vel, 0, type, damage, knockBack, player.whoAmI);

                Item.consumable = true;
            }
            else
            {
                Item.consumable = false;
            }
            return false;
        }
    }
}