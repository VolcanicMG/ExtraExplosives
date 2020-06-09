using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives
{
    /// <summary>
    /// This class contains global variables and functions that are used by all other classes
    /// </summary>
    class GlobalMethods
    {

        //========================| List of Mods for Mod Integration |========================\\

        /// <summary>
        /// Used Clamaity Mod Integration
        /// </summary>
        public static Mod CalamityMod = ModLoader.GetMod("CalamityMod");

        /// <summary>
        /// Thorium Mod Integration
        /// </summary>
        public static Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        //====================================================================================\\





        //========================| Config Variables |========================\\

        /// <summary>
        /// Can be changed in mod config editor - This determines if bombs break walls
        /// </summary>
        public static bool CanBreakWalls;

        /// <summary>
        /// Can be changed in mod config editor - This determines if bombs break tiles
        /// </summary>
        public static bool CanBreakTiles;

        /// <summary>
        /// Can be changed in mod config editor - This determines how much dust is allowed to spawn
        /// </summary>
        public static float DustAmount;

        //====================================================================\\





        //========================| Lists of Unbreakable Tiles |========================\\

        /// <summary>
        /// Holds the Unbreakable Tiles in Vanilla Terraria
        /// </summary>
        public static ushort[] Vanilla_UnbreakableTiles = new ushort[21];

        /// <summary>
        /// Holds the Unbreakable Tiles in Calamity Mod
        /// </summary>
        public static int[] CalamityMod_UnbreakableTiles = new int[17]; //5, 14, 16 not working

        /// <summary>
        /// Holds the Unbreakable Tiles in Thorium Mod
        /// </summary>
        public static int[] ThoriumMod_UnbreakableTiles = new int[5];

        /// <summary>
        /// This initializes the lists of unbreakable tiles
        /// </summary>
        /// <remarks>
        /// Currently checking the following:
        /// Vanilla - Calamity - Thorium
        /// </remarks>
        public static void SetupListsOfUnbreakableTiles()
        {
            //Setup Vanilla UnbreakableTiles

            Vanilla_UnbreakableTiles = new ushort[21];
            Vanilla_UnbreakableTiles[0] = TileID.LihzahrdBrick;
            Vanilla_UnbreakableTiles[1] = TileID.LihzahrdAltar;
            Vanilla_UnbreakableTiles[2] = TileID.LihzahrdFurnace;
            Vanilla_UnbreakableTiles[3] = TileID.DesertFossil;
            Vanilla_UnbreakableTiles[4] = TileID.BlueDungeonBrick;
            Vanilla_UnbreakableTiles[5] = TileID.GreenDungeonBrick;
            Vanilla_UnbreakableTiles[6] = TileID.PinkDungeonBrick;
            Vanilla_UnbreakableTiles[7] = TileID.Cobalt;
            Vanilla_UnbreakableTiles[8] = TileID.Palladium;
            Vanilla_UnbreakableTiles[9] = TileID.Mythril;
            Vanilla_UnbreakableTiles[10] = TileID.Orichalcum;
            Vanilla_UnbreakableTiles[11] = TileID.Adamantite;
            Vanilla_UnbreakableTiles[12] = TileID.Titanium;
            Vanilla_UnbreakableTiles[13] = TileID.Chlorophyte;
            Vanilla_UnbreakableTiles[14] = TileID.DefendersForge;
            Vanilla_UnbreakableTiles[15] = TileID.DemonAltar;
            Vanilla_UnbreakableTiles[16] = TileID.Containers;
            Vanilla_UnbreakableTiles[17] = TileID.Containers2;
            Vanilla_UnbreakableTiles[18] = TileID.FakeContainers;
            Vanilla_UnbreakableTiles[19] = TileID.TrashCan;
            Vanilla_UnbreakableTiles[20] = TileID.Dressers;



            //Setup Calamity Unbreakable Tiles
            if (CalamityMod != null)
            {
                CalamityMod_UnbreakableTiles = new int[17];
                CalamityMod_UnbreakableTiles[0] = CalamityMod.TileType("SeaPrism");
                CalamityMod_UnbreakableTiles[1] = CalamityMod.TileType("AerialiteOre");
                CalamityMod_UnbreakableTiles[2] = CalamityMod.TileType("CryonicOre");
                CalamityMod_UnbreakableTiles[3] = CalamityMod.TileType("CharredOre");
                CalamityMod_UnbreakableTiles[4] = CalamityMod.TileType("PerennialOre");
                CalamityMod_UnbreakableTiles[5] = CalamityMod.TileType("ChaoticOre");
                CalamityMod_UnbreakableTiles[6] = CalamityMod.TileType("AstralOre");
                CalamityMod_UnbreakableTiles[7] = CalamityMod.TileType("ExodiumOre");
                CalamityMod_UnbreakableTiles[8] = CalamityMod.TileType("UelibloomOre");
                CalamityMod_UnbreakableTiles[9] = CalamityMod.TileType("AuricOre");
                CalamityMod_UnbreakableTiles[10] = CalamityMod.TileType("AbyssGravel");
                CalamityMod_UnbreakableTiles[11] = CalamityMod.TileType("Voidstone");
                CalamityMod_UnbreakableTiles[12] = CalamityMod.TileType("PlantyMush");
                CalamityMod_UnbreakableTiles[13] = CalamityMod.TileType("Tenebris");
                CalamityMod_UnbreakableTiles[14] = CalamityMod.TileType("ArenaTile");
                CalamityMod_UnbreakableTiles[15] = CalamityMod.TileType("Cinderplate");
                CalamityMod_UnbreakableTiles[16] = CalamityMod.TileType("ExodiumOre");
            }

            //Setup Thorium Unbreakable Tiles
            if (ThoriumMod != null)
            {
                ThoriumMod_UnbreakableTiles = new int[5];
                ThoriumMod_UnbreakableTiles[0] = ThoriumMod.TileType("AquaiteBare");
                ThoriumMod_UnbreakableTiles[1] = ThoriumMod.TileType("LodeStone");
                ThoriumMod_UnbreakableTiles[2] = ThoriumMod.TileType("ValadiumChunk");
                ThoriumMod_UnbreakableTiles[3] = ThoriumMod.TileType("IllumiteChunk");
                ThoriumMod_UnbreakableTiles[4] = ThoriumMod.TileType("PearlStone");
            }

        }

        //==============================================================================\\





        //========================| List of Global Functions |========================\\

        /// <summary>
        /// This function spawns in mini-projectiles that are used to "damage" entites when an explosion happens
        /// </summary>
        /// <param name="DamageRadius"> Determines the radius of the damage projectiles explosion </param>
        /// <param name="DamagePosition"> Determines the center point of the damage projectiles explosion </param>
        /// <param name="Damage"> Stores the damage projectiles damage amount </param>
        /// <param name="Knockback"> Stores the damage projectiles knockback amount </param>
        /// <param name="ProjectileOwner"> Stores the owner who called the damage projectile </param>
        public static void ExplosionDamage(float DamageRadius, Vector2 DamagePosition, int Damage, float Knockback, int ProjectileOwner)
        {
            ExplosionDamageProjectile.DamageRadius = DamageRadius; //Sets the radius of the explosion
            Projectile.NewProjectile(DamagePosition, Vector2.Zero, ProjectileType<ExplosionDamageProjectile>(), Damage, Knockback, ProjectileOwner, 0.0f, 0); //Spawns the damage projectile
        }


        /// <summary>
        /// This function is used to check for unbreakable tiles - Returns [true] if tile is unbreakable
        /// </summary>
        /// <param name="Tile"> This is the tile currently being checked - Try: Main.tile[xPosition, yPosition].type </param>
        /// <param name="posX"> This is the tiles X Position - Try: xPosition </param>
        /// <param name="posY"> This is the tiles Y Position - Try: yPosition </param>
        /// <returns> Returns [true] if the tile is unbreakable </returns>
        public static Boolean CheckForUnbreakableTiles(int Tile)
        {
            Boolean flag = false; //Used to check if a tile is unbreakable - If true, then the tile is unbreakable
            int LargestListNumber = 70; //Used to limit the UnbreakableTileLoop, number must be larger then the largest list of unbreakable tiles

            //Tests If Tile Is OutOfBounds
            //if (posX < 0 || posY < 0 || posX > Main.maxTilesX || posY > Main.maxTilesY)
            //    return true;

            for (int i = 0; i < LargestListNumber; i++) //Loop runs through all lists of unbrakable tiles and throws a flag if an unbreakable tile is found
            {
                //Checks For Vanilla Unbreakable Tiles
                if ((true) && (i < Vanilla_UnbreakableTiles.Length))
                    if (Tile == Vanilla_UnbreakableTiles[i])
                        flag = true;

                //Checks For Calamity Unbreakable Tiles
                if ((CalamityMod != null) && (i < CalamityMod_UnbreakableTiles.Length))
                    if (Tile == CalamityMod_UnbreakableTiles[i])
                        flag = true;

                //Checks For Thorium Unbreakable Tiles
                if ((ThoriumMod != null) && (i < ThoriumMod_UnbreakableTiles.Length))
                    if (Tile == ThoriumMod_UnbreakableTiles[i])
                        flag = true;

                //Breaks if flag is triggered
                if (flag)
                    break;

            }
            return flag; //Returns flag
        }

        //from CosmivengeonMod:
        public static void SpawnProjectileSynced(Vector2 position, Vector2 velocity, int type, int damage, float knockback, float ai0 = 0f, float ai1 = 0f, int owner = 255)
        {
            if (owner == 255)
                owner = Main.myPlayer;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int proj = Projectile.NewProjectile(position, velocity, type, damage, knockback, owner, ai0, ai1);

                NetMessage.SendData(MessageID.SyncProjectile, number: proj);
            }
        }

        //============================================================================\\





        //========================| Code Snippets Ready For Copy/Paste |========================\\

        //Here you will find one commented copy of a function that explains code functionality,
        //and then a second function below it that can be used for copy/paste

        //======================================================================================\\

    }
}
