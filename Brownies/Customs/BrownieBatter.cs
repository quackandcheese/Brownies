using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenBrownies.Customs
{
    class BrownieBatter : CustomItemGroup<BrownieBatterItemGroupView>
    {
        public override string UniqueNameID => "Brownie Batter";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("BrownieBatter");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override string ColourBlindTag => "BB";

        public override List<ItemGroup.ItemSet> Sets => new List<ItemGroup.ItemSet>()
        {
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new List<Item>()
                {
                    Mod.Flour
                }
            },
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new List<Item>()
                {
                    Mod.CrackedEgg
                }
            },
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new List<Item>()
                {
                    Mod.ChoppedChocolate,
                    Mod.ChocolateShavings
                }
            }
        };
        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 20,
                Process = Mod.Cook,
                Result = Mod.CookedBrownie
            }
        };


        public override void OnRegister(GameDataObject gameDataObject)
        {
            var materials = new Material[3];
            
            materials[0] = MaterialUtils.GetExistingMaterial("Flour");
            materials[1] = MaterialUtils.GetExistingMaterial("Metal Dark");
            MaterialUtils.ApplyMaterial(Prefab, "Flour", materials);

            materials[0] = MaterialUtils.GetExistingMaterial("Egg - White");
            materials[1] = MaterialUtils.GetExistingMaterial("Egg - Yolk");
            MaterialUtils.ApplyMaterial(Prefab, "Egg", materials);

            materials[0] = CustomMaterials.CustomMaterialsIndex["IngredientLib - \"Chocolate\""];
            materials[1] = CustomMaterials.CustomMaterialsIndex["IngredientLib - \"Chocolate Dark\""];
            materials[2] = CustomMaterials.CustomMaterialsIndex["IngredientLib - \"Chocolate Light\""];
            MaterialUtils.ApplyMaterial(Prefab, "Chocolate", materials);

            Prefab.GetComponent<BrownieBatterItemGroupView>()?.Setup(Prefab);
        }
    }
    public class BrownieBatterItemGroupView : ItemGroupView
    {
        internal void Setup(GameObject prefab) =>
            // This tells which sub-object of the prefab corresponds to each component of the ItemGroup
            // All of these sub-objects are hidden unless the item is present
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = GameObjectUtils.GetChildObject(prefab, "Flour"),
                    Item = Mod.Flour
                },
                new()
                {
                    GameObject = GameObjectUtils.GetChildObject(prefab, "Egg"),
                    Item = Mod.CrackedEgg
                },
                new()
                {
                    GameObject = GameObjectUtils.GetChildObject(prefab, "Chocolate"),
                    Item = Mod.ChoppedChocolate
                }
            };
    }
}
