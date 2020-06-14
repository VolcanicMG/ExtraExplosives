using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraExplosives;

namespace ExtraExplosives
{
    public class ForeignModParsing
    {
        
        //private string version = Main.versionNumber;
        // Uses layered ternary operations to get the current final item id of vanilla then supplements it with the additional tml ids
        private static int finalVanillaID = 3965;//((version.Contains("1.3.5")) ? 3929 : (version != "1.4.0.5") ? 5042 : 5044) + 36;
        private static int finalVanillaProj = 713;//((version.Contains("1.3.5")) ? 713 : (version != "1.4.0.5") ? 948 : 949);
        private static ArrayList modAmmo = new ArrayList();
        private static ArrayList modProj = new ArrayList();

        public static void PostLoad()
        {
            parseModdedItems();
            reactToModdedItems();
            Unload();
        }

        private static void parseModdedItems()
        {
            // Handles registering usable ammo into holding array
            // NOTE IT WOULD BE GOOD TO UNLOAD ALL THIS ONCE THE LOADING PROCESS IS FINISHED
            // DONT HOLD UP UNNEEDED MEMORY
            // c# will probably already do this but its good to be careful
            int skipped = 0;
            // Info on what the mod is currently doing, disabled because my references got messed up and it threw errors
            //ModLoader.GetMod("ExtraExplosives").Logger.Info("Parsing modded items, may be a moment");
            for (int i = finalVanillaID;
                i + skipped <= ItemLoader.ItemCount;
                i++) // Dont worry this wont run for every int possible (promise)
            {
                if (ItemLoader.GetItem(i) == null)
                {
                    i--;
                    skipped++;
                }
                else
                {
                    //Debug info ignore this, its here so i dont have to retype it each time i need debug info
                    //ModLoader.GetMod("ExtraExplosives").Logger.InfoFormat("Found item {0} with ID {1} from mod {2}, recognized as ammo", ItemLoader.GetItem(i).DisplayName.GetDefault(), ItemLoader.GetItem(i).item.ammo, ItemLoader.GetItem(i).mod.DisplayName);
                    ModItem ammo = ItemLoader.GetItem(i);
                    if (ammo.DisplayName.GetDefault().ToLower().Contains("bullet") && ammo.mod.DisplayName != ModLoader.GetMod("ExtraExplosives").DisplayName)    // possible additional check, left out due to possible inconsitent naming conventions
                    {                                        // && ammo.item.ammo == AmmoID.Bullet
                       modAmmo.Add(ammo);
                    }
                }
            }

            skipped = 0;
            for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
            {
                if (ProjectileLoader.GetProjectile(i) == null) continue;
                ModProjectile ammo = ProjectileLoader.GetProjectile(i);
                if (ammo.mod.DisplayName != ModLoader.GetMod("ExtraExplosives").DisplayName &&
                    ammo.DisplayName.GetDefault().ToLower().Contains("bullet"))
                {
                    modProj.Add(ammo);
                }
            }
        }

        private static void reactToModdedItems()
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            foreach (ModItem ammo in modAmmo)
            {
                int final = ammo.DisplayName.GetDefault().LastIndexOf('.') + 1;
                
                sb = new StringBuilder(ammo.DisplayName.GetDefault().Substring(final));
               
                if (sb.ToString().Contains("bullet") && sb.ToString().Contains("item")) sb.Replace("item", "proj");
                else if (sb.ToString().Contains("Bullet") && sb.ToString().Contains("Item")) sb.Replace("Item", "Proj");
                else {sb.Append((sb.ToString().Contains("bullet"))?"proj":"Proj");}

                res = (ammo.mod.GetProjectile(sb.ToString()) != null) ? sb.ToString() : "";
                ModProjectile proj = ammo.mod.GetProjectile(res);
                /*if (res != "")
                {
                    ModLoader.GetMod("ExtraExplosives").Logger.InfoFormat(
                        "Item {0} detected as linked to projectile {1}",
                        ammo.DisplayName.GetDefault(), proj.DisplayName.GetDefault());
                }*/ // Disabled because of reference error while loading (not an issue with the code just my pc)

                if (proj != null)
                {
                    NewBulletBoomItem tempItem = new NewBulletBoomItem(ammo.item.type, ammo.DisplayName.GetDefault(), ammo.Name.Replace("Bullet", ""));
                    NewBulletBoomProjectile tempProj = new NewBulletBoomProjectile(proj.projectile.type, proj.Name);
                    ExtraExplosives.NewRegister(tempItem, tempProj);
                    
                    
                }
            }
            sb.Clear();    // Clears the stringbuilder, just in case c# misses it
        }

        private static void Unload()    // Unloads everything to avoid any accidental memory leaking
        {                        // This should be done by c# and tml, but always be cautious
            modProj.Clear();
            modAmmo.Clear();
        }
        

    }
}