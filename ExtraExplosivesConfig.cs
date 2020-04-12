using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using ExtraExplosives.Projectiles;
using ExtraExplosives.NPCs;

namespace ExtraExplosives
{
	public class ExtraExplosivesConfig : ModConfig
	{

        public override ConfigScope Mode => ConfigScope.ServerSide; //Change to client to make it only applicable to the client side

        [Header("Explosives Wall Settings")]

        [Label("Toggle Wall Breaking")]
        public bool CanBreakWalls;

        [Header("Explosives Block Settings")]
        [Label("Toggle Block Breaking for the C4, Da Bomb, and Captain Explosive")]
        public bool CanBreakTiles;

        public override void OnChanged()
        {

            TheLevelerProjectile.CanBreakWalls = CanBreakWalls;
            SmallExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            ArenaBuilderProjectile.CanBreakWalls = CanBreakWalls;
            PhaseBombProjectile.CanBreakWalls = CanBreakWalls;
            NPCSpawnerProjectile.CanBreakWalls = CanBreakWalls;
            MegaExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            MediumExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            LargeExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            HeavyBombProjectile.CanBreakWalls = CanBreakWalls;
            GiganticExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            DaBombProjectile.CanBreakWalls = CanBreakWalls;
            ClusterBombChildProjectile.CanBreakWalls = CanBreakWalls;
            ClusterBombProjectile.CanBreakWalls = CanBreakWalls;
            C4Projectile.CanBreakWalls = CanBreakWalls;
            BigBouncyDynamiteProjectile.CanBreakWalls = CanBreakWalls;
            BasicExplosiveProjectile.CanBreakWalls = CanBreakWalls;
            C4Projectile.CanBreakTiles = CanBreakTiles;
            DaBombProjectile.CanBreakTiles = CanBreakTiles;
            CaptainExplosive.CanBreakTiles = CanBreakTiles;
            CaptainExplosive.CanBreakWalls = CanBreakWalls;
        }
    }
}