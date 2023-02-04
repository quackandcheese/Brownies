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
    class BrowniePortion : CustomItem
    {
        public override string UniqueNameID => "Brownie Portion";
        public override GameObject Prefab => Mod.bundle.LoadAsset<GameObject>("BrowniePortion");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override string ColourBlindTag => "B";

        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 7,
                Process = Mod.Cook,
                IsBad = true,
                Result = Mod.Burnt
            }
        };

        public override void OnRegister(GameDataObject gameDataObject)
        {
            var materials = new Material[2];

            materials[0] = MaterialUtils.GetExistingMaterial("Chocolate");
            MaterialUtils.ApplyMaterial(Prefab, "BrowniePortion", materials);

            materials[0] = MaterialUtils.GetExistingMaterial("Plate");
            materials[1] = MaterialUtils.GetExistingMaterial("Metal");
            MaterialUtils.ApplyMaterial(Prefab, "Plate", materials);
        }
    }
}
