
using System.Collections.Generic;
using UnityEngine;

public class Skill6 : SkillBase
{
    public Skill6() : base()
    {
        id = 6;
        //被动
        type = 1;
        name = "破防";
        description = "造成伤害时，降低目标3点护甲，持续1回合";
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
        BuffBase buff = enemy.getBuffById(17);
        if (buff == null)
        {
            enemy.addBuff(new Buff17(1));
        }
    }
}
