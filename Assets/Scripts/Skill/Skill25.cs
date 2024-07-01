
using System.Collections.Generic;
using UnityEngine;

public class Skill25 : SkillBase
{
    public Skill25() : base()
    {
        id = 25;
        //被动
        type = 1;
        name = "双攻";
        description = "可攻击2次";
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
        role.roleModel.roleData.attackTimes = 2;
    }
}
