
using System.Collections.Generic;
using UnityEngine;

public class Skill10 : SkillBase
{
    public Skill10() : base()
    {
        id = 10;
        //被动
        type = 1;
        name = "威慑";
        description = "攻击、反击目标，会使目标无法行动、反击一回合";
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
        enemy.addBuff(new Buff18(1));
    }
}
