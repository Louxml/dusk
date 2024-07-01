
using System.Collections.Generic;
using UnityEngine;

public class Skill41 : SkillBase
{
    public Skill41() : base()
    {
        id = 41;
        //被动
        type = 1;
        name = "四攻二移";
        description = "一回合可攻击4次、移动2次";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onRoundStart()
    {
        base.onRoundStart();
        role.roleModel.roleData.attackTimes = 4;
        role.roleModel.roleData.moveTimes = 2;
    }
}
