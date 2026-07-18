using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        //public new int clothId = 2;

        public float damageMultiply = .5f;
        public override void Collect()
        {
            base.Collect();
            //ClothManager.Instance.GetClothByType(ClothType.SPEED);
            Player.Instance.health.ChangeDamageMultiply(damageMultiply, duration);
        }
    }
}
