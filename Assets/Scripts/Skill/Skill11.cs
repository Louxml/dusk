
using System.Collections.Generic;
using UnityEngine;

public class Skill11 : SkillBase
{
    public Skill11() : base()
    {
        id = 11;
        //被动
        type = 1;
        name = "不受反击";
        description = "不受反击";
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
        role.roleModel.isCanBack = false;
    }
}
