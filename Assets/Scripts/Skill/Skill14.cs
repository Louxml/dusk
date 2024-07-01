
using System.Collections.Generic;
using UnityEngine;

public class Skill14 : SkillBase
{
    public Skill14() : base()
    {
        id = 14;
        //被动
        type = 1;
        name = "穿墙";
        description = "可无视障碍移动。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onEnableRole(RoleControl role)
    {
        role.roleModel.ignorePass = true;
    }
}
