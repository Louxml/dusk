
using System.Collections.Generic;
using UnityEngine;

public class Skill15 : SkillBase
{
    public Skill15() : base()
    {
        id = 15;
        //被动
        type = 1;
        name = "飞行";
        description = "自身不会成为移动障碍。";
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
        role.getXY(out int x, out int y);
        MapDataMgr.Instance.setGridIsPass(x, y, true);
        role.roleModel.isUpdatePass = false;
    }
}
