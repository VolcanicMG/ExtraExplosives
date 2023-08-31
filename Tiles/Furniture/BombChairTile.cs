using System.Collections.Generic;
using ExtraExplosives.Dusts;
using ExtraExplosives.Items.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    // TODO alternate does not drop its item, no clue
    public class BombChairTile : ModTile
    {
        
        public const int NextStyleHeight = 42;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.CanBeSatOnForPlayers[Type] = true;
            TileID.Sets.CanBeSatOnForNPCs[Type] = true;
            
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);

            DustType = ModContent.DustType<DebrisDust>();
            AdjTiles = new int[] { TileID.Chairs };

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Chair"));
            
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 20 };
            TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight; //allows me to place example chairs facing the same way as the player
            TileObjectData.addAlternate(1); //facing right will use the second texture style
            
            // Some weird issue where alternate tile wont drop when broken, this fixes it, no clue why it happens
            RegisterItemDrop(ModContent.ItemType<BombChairItem>(), TileObjectData.newAlternate.StyleMultiplier);
            
            TileObjectData.addTile(Type);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
            return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); // Avoid being able to trigger it from long range
        }

        public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) {
            // It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
            Tile tile = Framing.GetTileSafely(i, j);

            //info.directionOffset = info.restingEntity is Player ? 6 : 2; // Default to 6 for players, 2 for NPCs
            //info.visualOffset = Vector2.Zero; // Defaults to (0,0)

            info.TargetDirection = -1;
            if (tile.TileFrameX != 0) {
                info.TargetDirection = 1; // Facing right if sat down on the right alternate (added through addAlternate in SetStaticDefaults earlier)
            }

            // The anchor represents the bottom-most tile of the chair. This is used to align the entity hitbox
            // Since i and j may be from any coordinate of the chair, we need to adjust the anchor based on that
            info.AnchorTilePosition.X = i; // Our chair is only 1 wide, so nothing special required
            info.AnchorTilePosition.Y = j;

            if (tile.TileFrameY % NextStyleHeight == 0) {
                info.AnchorTilePosition.Y++; // Here, since our chair is only 2 tiles high, we can just check if the tile is the top-most one, then move it 1 down
            }
        }
        
        public override bool RightClick(int i, int j) {
            Player player = Main.LocalPlayer;

            if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance)) { // Avoid being able to trigger it from long range
                player.GamepadEnableGrappleCooldown();
                player.sitting.SitDown(player, i, j);
            }

            return true;
        }
        
        public override void MouseOver(int i, int j) {
            Player player = Main.LocalPlayer;

            if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance)) { // Match condition in RightClick. Interaction should only show if clicking it does something
                return;
            }

            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Items.Tiles.Furniture.BombChairItem>();

            if (Main.tile[i, j].TileFrameX / 18 < 1) {
                player.cursorItemIconReversed = true;
            }
        }
    }
}