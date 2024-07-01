
using System.Collections.Generic;
using UnityEngine;

public class Skill19 : SkillBase
{
    public Skill19() : base()
    {
        id = 19;
        //被动
        type = 1;
        name = "连斩";
        description = "击杀敌方单位时，可再进行一次攻击。";
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
        if (!enemy.isLife())
        {
            role.addAttackTimes(1);
        }
    }
}
