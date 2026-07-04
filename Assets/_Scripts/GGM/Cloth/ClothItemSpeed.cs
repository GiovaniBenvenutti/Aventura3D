using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float targetSpeed = 5f;
        public override void Collect()
        {
            base.Collect();
            //ClothManager.Instance.GetClothByType(ClothType.SPEED);
            Player.Instance.ChangeSpeed(targetSpeed, duration);
        }
    }
}
