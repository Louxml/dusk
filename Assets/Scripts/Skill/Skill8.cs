
using System.Collections.Generic;
using UnityEngine;

public class Skill8 : SkillBase
{
    public Skill8() : base()
    {
        id = 8;
        //被动
        type = 1;
        name = "无法反击";
        description = "无法反击";
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
        role.roleModel.roleData.backAttackTimes = 0;
    }
}
