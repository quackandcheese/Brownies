using KitchenData;
using KitchenLib;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMods;
using KitchenLib.Event;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System;
using KitchenBrownies.Customs;
using KitchenLib.Customs;

// Namespace should have "Kitchen" in the beginning
namespace KitchenBrownies
{
    public class Mod : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "QuackAndCheese.PlateUp.Brownies";
        public const string MOD_NAME = "Brownies";
        public const string MOD_VERSION = "0.1.2";
        public const string MOD_AUTHOR = "QuackAndCheese";
        public const string MOD_GAMEVERSION = ">=1.1.3";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.3" current and all future
        // e.g. ">=1.1.3 <=1.2.3" for all from/until

        // Boolean constant whose value depends on whether you built with DEBUG or RELEASE mode, useful for testing
#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

        public static AssetBundle Bundle;

        // Vanilla Processes
        internal static Process Cook => GetExistingGDO<Process>(ProcessReferences.Cook);
        internal static Process Chop => GetExistingGDO<Process>(ProcessReferences.Chop);
        internal static Process Knead => GetExistingGDO<Process>(ProcessReferences.Knead);
        internal static Process RequireOven => GetExistingGDO<Process>(ProcessReferences.RequireOven);

        // Vanilla Items
        internal static Item Flour => GetExistingGDO<Item>(ItemReferences.Flour);
        internal static Item Egg => GetExistingGDO<Item>(ItemReferences.Egg);
        internal static Item CrackedEgg => GetExistingGDO<Item>(ItemReferences.EggCracked);
        internal static Item Burnt => GetExistingGDO<Item>(ItemReferences.BurnedFood);
        internal static Item Nuts => GetExistingGDO<Item>(ItemReferences.NutsIngredient);
        internal static Item ChoppedNuts => GetExistingGDO<Item>(ItemReferences.NutsChopped);
        internal static Item VanillaIceCream => GetExistingGDO<Item>(ItemReferences.IceCreamVanilla);

        // Modded Items
        public static Item Chocolate => Find<Item>(IngredientLib.References.GetIngredient("chocolate"));
        public static Item ChoppedChocolate => Find<Item>(IngredientLib.References.GetIngredient("chopped chocolate"));

        internal static Item BrowniePortion => GetModdedGDO<Item, BrowniePortion>();
        internal static ItemGroup BrownieBatter => GetModdedGDO<ItemGroup, BrownieBatter>();
        internal static Item CookedBrownie => GetModdedGDO<Item, CookedBrownie>();
        internal static ItemGroup BrownieALaMode => GetModdedGDO<ItemGroup, BrownieALaMode>();

        // Modded Dishes
        internal static Dish BrownieDish => GetModdedGDO<Dish, BrownieDish>();
        internal static Dish BrownieALaModeDish => GetModdedGDO<Dish, BrownieALaModeDish>();


        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {

            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            // Dishes
            AddGameDataObject<BrownieDish>();
            AddGameDataObject<BrownieALaModeDish>();

            // Items
            AddGameDataObject<BrownieBatter>();
            AddGameDataObject<BrowniePortion>();
            AddGameDataObject<CookedBrownie>();
            AddGameDataObject<BrownieALaMode>();
            

            LogInfo("Done loading game data.");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // TODO: Uncomment the following if you have an asset bundle.
            // TODO: Also, make sure to set EnableAssetBundleDeploy to 'true' in your ModName.csproj

            LogInfo("Attempting to load asset bundle...");
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).First();
            LogInfo("Done loading asset bundle.");

            // Register custom GDOs
            AddGameData();

            // Perform actions when game data is built
            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
                // Fix bug with card requirements
                Find<Unlock, BrownieALaModeDish>().BlockedBy.Clear();
            };
        }

        private static T1 GetModdedGDO<T1, T2>() where T1 : GameDataObject
        {
            return (T1)GDOUtils.GetCustomGameDataObject<T2>().GameDataObject;
        }
        private static T GetExistingGDO<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id);
        }
        internal static T Find<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id) ?? (T)GDOUtils.GetCustomGameDataObject(id)?.GameDataObject;
        }

        internal static T Find<T, C>() where T : GameDataObject where C : CustomGameDataObject
        {
            return GDOUtils.GetCastedGDO<T, C>();
        }

        internal static T Find<T>(string modName, string name) where T : GameDataObject
        {
            return GDOUtils.GetCastedGDO<T>(modName, name);
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
