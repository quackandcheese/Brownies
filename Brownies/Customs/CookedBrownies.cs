using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace KitchenBrownies.Customs
{
    public class CookedBrownieItemView : ObjectsSplittableView
    {
        internal void Setup(GameObject prefab)
        {
            var fObjects = ReflectionUtils.GetField<ObjectsSplittableView>("Objects");
            fObjects.SetValue(this, new List<GameObject>()
            { 
                //prefab.GetChild("BrownieSlice1"),
                prefab.GetChild("BrownieSlice2"),
                prefab.GetChild("BrownieSlice3"),
                prefab.GetChild("BrownieSlice4"),
                prefab.GetChild("BrownieSlice5"),
                prefab.GetChild("BrownieSlice6")
            }); 
        }
    }

    public class CookedBrownie : CustomItem
    {
        public override string UniqueNameID => "Cooked Brownie";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("CookedBrownie");
        public override bool AllowSplitMerging => false;
        public override float SplitSpeed => 0.75f;
        public override int SplitCount => 5;
        public override Item SplitSubItem => Mod.BrowniePortion;
        public override List<Item> SplitDepletedItems => new() { Mod.BrowniePortion };
        public override bool PreventExplicitSplit => false;

        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 10,
                Process = Mod.Cook,
                IsBad = true,
                Result = Mod.Burnt
            }
        };


        public override void OnRegister(GameDataObject gameDataObject)
        {
            var materials = new Material[]
            {
                  MaterialUtils.GetExistingMaterial("Chocolate"),
            };
            MaterialUtils.ApplyMaterial(Prefab, "BrownieSlice1", materials);
            MaterialUtils.ApplyMaterial(Prefab, "BrownieSlice2", materials);
            MaterialUtils.ApplyMaterial(Prefab, "BrownieSlice3", materials);
            MaterialUtils.ApplyMaterial(Prefab, "BrownieSlice4", materials);
            MaterialUtils.ApplyMaterial(Prefab, "BrownieSlice5", materials);
            MaterialUtils.ApplyMaterial(Prefab, "BrownieSlice6", materials);


            if (!Prefab.HasComponent<CookedBrownieItemView>())
            {
                var view = Prefab.AddComponent<CookedBrownieItemView>();
                view.Setup(Prefab);
            }
        }
    }
}
