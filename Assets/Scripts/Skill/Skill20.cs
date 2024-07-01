
using System.Collections.Generic;
using UnityEngine;

public class Skill20 : SkillBase
{
    public Skill20() : base()
    {
        id = 20;
        //被动
        type = 1;
        name = "1点";
        description = "攻击时至少造成1点伤害";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        enemy.addBuff(new Buff20(1));
    }
}
