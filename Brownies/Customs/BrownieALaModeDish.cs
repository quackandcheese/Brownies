using KitchenBrownies.Registry;
using KitchenData;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenBrownies.Customs
{
    class BrownieALaModeDish : ModDish
    {
        public override string UniqueNameID => "Brownie A La Mode Dish";
        public override DishType Type => DishType.Dessert;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override CardType CardType => CardType.Default;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;

        public override List<Unlock> HardcodedRequirements => new()
        {
            Mod.BrownieDish
        };

        public override List<Dish.MenuItem> ResultingMenuItems => new List<Dish.MenuItem>
        {
            new Dish.MenuItem
            {
                Item = Mod.BrownieALaMode,
                Phase = MenuPhase.Dessert,
                Weight = 1
            }
        };
        public override HashSet<Item> MinimumIngredients => new HashSet<Item>
        {
            Mod.Chocolate,
            Mod.Flour,
            Mod.Egg,
            Mod.Nuts,
            Mod.VanillaIceCream
        };
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
            Mod.Cook,
            Mod.Chop,
            Mod.Knead,
            Mod.RequireOven
        };

        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Chop egg, chop chocolate, and combine with flour. Cook and portion. Combine with a scoop of vanilla ice cream and chopped nuts, then serve!" }
        };
        public override IDictionary<Locale, UnlockInfo> LocalisedInfo => new Dictionary<Locale, UnlockInfo>
        {
            { Locale.English, LocalisationUtils.CreateUnlockInfo("Brownies A La Mode", "Adds Brownies A La Mode as a Dessert", "Even more yummers.") }
        };
    }
}
