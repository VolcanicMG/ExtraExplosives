using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ChatManager = Terraria.UI.Chat.ChatManager;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ExtraExplosives.Items.Explosives
{
    public class BulletBoomItem : ExplosiveItem
    {
	    private int _projectileID;
	    public int overStack = 0;
	    public string bulletType = "";
	    private ModProjectile projectile;

	    public override bool CloneNewInstances => true;    // DONT CHANGE
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
			ModItem tmp =  base.Clone(item);
			tmp.DisplayName.SetDefault("Base Item");
			tmp.item.shoot = item.shoot;
			return tmp;
		}
		
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var overStackTooltip = new TooltipLine(mod, "overStackTooltip", $"It has {overStack} uses left");
			overStackTooltip.overrideColor = Color.Chartreuse;
			tooltips.Add(overStackTooltip);
			var bulletType = new TooltipLine(mod, "bulletTypeTooltip", $"Its filled with {this.bulletType}s");
			bulletType.overrideColor = Color.YellowGreen;
			tooltips.Add(bulletType);
		}

		public void SetProjectile(int projectileID)
		{
			_projectileID = projectileID;
		}

		public int GetProjectile() => _projectileID;
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(" Bullet Boom");    // Name
			
			Tooltip.SetDefault("Who said a gun is the only thing that can shoot a bullet? \n" +
			                   "Blows up upon touching a block.");
		}

		public override void SafeSetDefaults()
		{
			item.damage = 30;	 //The damage stat for the Weapoon
			item.width = 20;	//sprite width
			item.height = 20;   //sprite height
			item.maxStack = 1;   //This defines the items max stack
			item.consumable = false;  //Tells the game that this should be used up once fired
			item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
			item.rare = 8;	 //The color the title of your item when hovering over it ingame
			item.UseSound = SoundID.Item1; //The sound played when using this item
			item.useAnimation = 35;  //How long the item is used for.
			item.useTime = 35;	 //How fast the item is used.
			item.value = Item.buyPrice(0, 3, 0, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
			item.noUseGraphic = true;
			item.noMelee = true;	  //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
			item.shoot = _projectileID; //This defines what type of projectile this item will shoot
			item.shootSpeed = 15f; //This defines the projectile speed when shot
			//item.createTile = mod.TileType("ExplosiveTile");
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
			Vector2 origin, float scale)
		{
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, overStack+"", position - new Vector2(5f,-13f), Color.Chartreuse, 0f, Vector2.Zero, new Vector2(0.7f,0.7f), -1f, 2f);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
			ref float knockBack)
		{
			if (overStack > 1) overStack--;
			else{item.stack--;}
			int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<BulletBoomProjectile>(),
				damage, knockBack,
				player.whoAmI);
			Main.projectile[proj].knockBack = item.shoot;
			mod.Logger.Debug(item.damage);
			return false;
		}
		public override TagCompound Save()
		{
			return new TagCompound
			{
				[nameof(overStack)] = overStack,
				[nameof(item.Name)] = item.Name,
				[nameof(item.shoot)] = item.shoot,
				[nameof(item.damage)] = item.damage,
				[nameof(bulletType)] = bulletType,
			};
		}

		public override void Load(TagCompound tag)
		{
			overStack = tag.GetInt(nameof(overStack));
			item.SetNameOverride(tag.GetString(nameof(item.Name)));
			item.shoot = tag.GetInt(nameof(item.shoot));
			item.damage = tag.GetInt(nameof(item.damage));
			bulletType = tag.GetString(nameof(bulletType));
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(overStack);
			writer.Write(item.Name);
			writer.Write(item.shoot);
			writer.Write(item.damage);
			writer.Write(bulletType);
		}

		public override void NetRecieve(BinaryReader reader)
		{
			overStack = reader.ReadInt32();
			item.SetNameOverride(reader.ReadString());
			item.shoot = reader.ReadInt32();
			item.damage = reader.ReadInt32();
			bulletType = reader.ReadString();
		}

    }
}