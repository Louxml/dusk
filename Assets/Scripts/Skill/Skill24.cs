
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Skill24 : SkillBase
{
    public Skill24() : base()
    {
        id = 24;
        //被动
        type = 1;
        name = "吸血";
        description = "造成伤害时，会为自身恢复等量数值的生命。";
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
        role.addHp(damage, false);
    }
}
