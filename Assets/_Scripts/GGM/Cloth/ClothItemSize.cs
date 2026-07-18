using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Cloth
{
    public class ClothItemSize : ClothItemBase
    {
      //  public new int clothId = 3;

        public float scaleMultiply = 2f;
        public override void Collect()
        {
            base.Collect();
            //ClothManager.Instance.GetClothByType(ClothType.SPEED);
            Player.Instance.ChangeSize(scaleMultiply * Vector3.one, duration);
        }
    }
}
