using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;
using DG.Tweening;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }
    
    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartInitAnimation();
            // Debug.Log("BossStateInit");
            boss.StartWalk(EndInit);
        }

        private void EndInit()
        {
            boss.SwitchState(BossAction.WALK);
        }

    }
    
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);
           // Debug.Log("BossStateWalk");
            boss.animator.SetTrigger("Run");
        }

        private void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttack);
        }

        private void EndAttack()
        {
            boss.SwitchState(BossAction.WALK);
        }
    }
    
    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("BossStateDeath");
            boss.transform.DOScale(Vector3.one * 0.8f, 1f);
            boss.StartDeathAnimation();
        }
    }

}
