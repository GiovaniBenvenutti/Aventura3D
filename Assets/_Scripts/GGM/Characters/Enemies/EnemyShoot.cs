using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase3D
    {
        public GunBase_Enemy gunBase;

        protected override void Init()
        {
            base.Init();
            if (gunBase != null) gunBase.StartShoot();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }
    }
}