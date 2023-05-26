using Kitchen;
using KitchenData;
using KitchenLib.Colorblind;
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
    class BrownieALaMode : CustomItemGroup<BrownieALaModeItemGroupView>
    {
        public override string UniqueNameID => "Brownie A La Mode";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("BrownieALaMode");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Medium;

        public override List<ItemGroup.ItemSet> Sets => new List<ItemGroup.ItemSet>()
        {
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new List<Item>()
                {
                    Mod.BrowniePortion
                }
            },
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new List<Item>()
                {
                    Mod.VanillaIceCream
                }
            },
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new List<Item>()
                {
                    Mod.ChoppedNuts
                }
            }
        };

        private bool GameDataBuilt = false;
        public override void OnRegister(GameDataObject gameDataObject)
        {
            var materials = new Material[2];

            materials[0] = MaterialUtils.GetExistingMaterial("Chocolate");
            MaterialUtils.ApplyMaterial(Prefab, "BrowniePortion", materials);

            materials[0] = MaterialUtils.GetExistingMaterial("Vanilla");
            MaterialUtils.ApplyMaterial(Prefab, "Ice Cream", materials);

            materials[0] = MaterialUtils.GetExistingMaterial("Cashew");
            MaterialUtils.ApplyMaterial(Prefab, "Nuts", materials);

            materials[0] = MaterialUtils.GetExistingMaterial("Plate");
            materials[1] = MaterialUtils.GetExistingMaterial("Metal");
            MaterialUtils.ApplyMaterial(Prefab, "Plate", materials);

            Prefab.GetComponent<BrownieALaModeItemGroupView>()?.Setup(Prefab);

            if (GameDataBuilt)
            {
                return;
            }

            if (Prefab.TryGetComponent<ItemGroupView>(out var itemGroupView))
            {
                GameObject clonedColourBlind = ColorblindUtils.cloneColourBlindObjectAndAddToItem(GameDataObject as ItemGroup);
                ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(itemGroupView, clonedColourBlind);
            }

            GameDataBuilt = true;
        }
    }
    public class BrownieALaModeItemGroupView : ItemGroupView
    {
        internal void Setup(GameObject prefab)
        {
            // This tells which sub-object of the prefab corresponds to each component of the ItemGroup
            // All of these sub-objects are hidden unless the item is present
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = GameObjectUtils.GetChildObject(prefab, "BrowniePortion"),
                    Item = Mod.BrowniePortion
                },
                new()
                {
                    GameObject = GameObjectUtils.GetChildObject(prefab, "Ice Cream"),
                    Item = Mod.VanillaIceCream
                },
                new()
                {
                    GameObject = GameObjectUtils.GetChildObject(prefab, "Nuts"),
                    Item = Mod.ChoppedNuts
                }
            };

            ComponentLabels = new()
            {
                new ()
                {
                    Text = "B",
                    Item = Mod.BrowniePortion
                },
                new ()
                {
                    Text = "V",
                    Item = Mod.VanillaIceCream
                },
                new ()
                {
                    Text = "N",
                    Item = Mod.ChoppedNuts
                }
            };
        }
    }   
}
