using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ChatManager = Terraria.UI.Chat.ChatManager;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ExtraExplosives.Items.Explosives
{
    // TODO Make this not terrible to look at
    public class BulletBoomItem : ExplosiveItem
    {
        private int _projectileID;
        public int overStack = 0;
        public string bulletType = "";
        private ModProjectile projectile;

        protected override bool CloneNewInstances => true;    // DONT CHANGE
        public override string Texture => "ExtraExplosives/Items/Explosives/BulletBoomItem";    // texture

        /*public TestItem(int itemID)
		{
			this.SetDefaults();
			int projectileId;
			if (itemID == -1)
			{
				projectileId = ProjectileID.Bullet;
				
			}
			else if(ItemLoader.GetItem(itemID) != null)
			{
				projectileId = ItemLoader.GetItem(itemID).item.shoot;
			}
			else
			{
				Item tmp = new Item();
				tmp.CloneDefaults(itemID);
				projectileId = tmp.shoot;
			}
			projectile = new TestProjectile().Clone();
			projectile.projectile.localAI[0] = projectileId;
			_projectileID = projectileId;
		}*/

        public override ModItem Clone(Item item)
        {
            ModItem tmp = base.Clone(item);
            // tmp.DisplayName.SetDefault("Base Item");
            tmp.Item.shoot = item.shoot;
            return tmp;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var overStackTooltip = new TooltipLine(Mod, "overStackTooltip", $"It has {overStack} uses left");
            overStackTooltip.OverrideColor = Color.Chartreuse;
            tooltips.Add(overStackTooltip);
            var bulletType = new TooltipLine(Mod, "bulletTypeTooltip", $"Its filled with {this.bulletType}s");
            bulletType.OverrideColor = Color.YellowGreen;
            tooltips.Add(bulletType);
        }

        public void SetProjectile(int projectileID)
        {
            _projectileID = projectileID;
        }

        public int GetProjectile() => _projectileID;

        public override void SafeSetDefaults()
        {
            Item.damage = 30;    //The damage stat for the Weapoon
            Item.width = 20;    //sprite width
            Item.height = 20;   //sprite height
            Item.maxStack = 1;   //This defines the items max stack
            Item.consumable = false;  //Tells the game that this should be used up once fired
            Item.useStyle = ItemUseStyleID.Swing;   //The way your item will be used, 1 is the regular sword swing for example
            Item.rare = ItemRarityID.Yellow;   //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 35;  //How long the item is used for.
            Item.useTime = 35;   //How fast the item is used.
            Item.value = Item.buyPrice(0, 3, 0, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.shoot = _projectileID; //This defines what type of projectile this item will shoot
            Item.shootSpeed = 15f; //This defines the projectile speed when shot
                                   //item.createTile = mod.TileType("ExplosiveTile");
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            //ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, overStack + "", position - new Vector2(5f, -13f), Color.Chartreuse, 0f, Vector2.Zero, new Vector2(0.7f, 0.7f), -1f, 2f);
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (overStack > 1) overStack--;
            else { Item.stack--; }
            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<BulletBoomProjectile>(),
                damage, knockBack,
                player.whoAmI);
            Main.projectile[proj].knockBack = Item.shoot;
            Mod.Logger.Debug(Item.damage);
            return false;
        }*/
        public override void SaveData(TagCompound tag)
        {
                tag.Add(nameof(overStack), overStack);
                tag.Add(nameof(Item.Name), Item.Name);
                tag.Add(nameof(Item.shoot),Item.shoot);
                tag.Add(nameof(Item.damage), Item.damage);
                tag.Add(nameof(bulletType), bulletType);
        }

        public override void LoadData(TagCompound tag)
        {
            overStack = tag.GetInt(nameof(overStack));
            Item.SetNameOverride(tag.GetString(nameof(Item.Name)));
            Item.shoot = tag.GetInt(nameof(Item.shoot));
            Item.damage = tag.GetInt(nameof(Item.damage));
            bulletType = tag.GetString(nameof(bulletType));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(overStack);
            writer.Write(Item.Name);
            writer.Write(Item.shoot);
            writer.Write(Item.damage);
            writer.Write(bulletType);
        }

        public override void NetReceive(BinaryReader reader)
        {
            overStack = reader.ReadInt32();
            Item.SetNameOverride(reader.ReadString());
            Item.shoot = reader.ReadInt32();
            Item.damage = reader.ReadInt32();
            bulletType = reader.ReadString();
        }

    }
}