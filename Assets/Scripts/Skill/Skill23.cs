
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Skill23 : SkillBase
{
    public Skill23() : base()
    {
        id = 23;
        //被动
        type = 1;
        name = "降临";
        description = "主动攻击击杀单位后，会强制移动到该单位位置";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        enemy.getXY(out int x, out int y);
        if (!enemy.isLife())
        {

            // 受击动画0.6秒后执行反击
            Sequence seq = DOTween.Sequence();
            seq.PrependInterval(1.4f).AppendCallback(() =>
            {
                CombatSystem.Instance.roleForceMove(role, x, y);
            });
        }
    }
}
