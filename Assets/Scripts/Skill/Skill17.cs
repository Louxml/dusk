
using System.Collections.Generic;
using UnityEngine;

public class Skill17 : SkillBase
{
    bool isEnable;
    public Skill17() : base()
    {
        id = 17;
        //被动
        type = 1;
        name = "攻击";
        description = "主动攻击时，仅在伤害判定结束前攻击力+3";
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
        if (!isBackAttack)
        {
            isEnable = true;
            role.addAtkValue(3);
        }
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (isEnable)
        {
            isEnable = false;
            role.addAtkValue(-3);
        }
    }
}
