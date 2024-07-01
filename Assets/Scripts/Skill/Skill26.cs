
using System.Collections.Generic;
using UnityEngine;

public class Skill26 : SkillBase
{
    public Skill26() : base()
    {
        id = 26;
        //被动
        type = 1;
        name = "反击";
        description = "可多次反击";
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
        role.roleModel.roleData.backAttackTimes = 99;
    }
}
