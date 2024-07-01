
using System.Collections.Generic;
using UnityEngine;

public class Skill31 : SkillBase
{
    public Skill31() : base()
    {
        id = 31;
        //被动
        type = 1;
        name = "振奋";
        description = "周围友方单位获得+1攻";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    //获取周围友方单位
    List<RoleControl> getNeighbourRoleList(PathNode node, int playerTag)
    {
        List<RoleControl> res = new List<RoleControl>();
        List<PathNode> list = MapDataMgr.Instance.getNeighbourList(node);

        foreach (PathNode n in list)
        {
            RoleControl role = RoleDataMgr.Instance.getRoleControl(n.x, n.y);
            if (role != null && role.getRoleTag() == playerTag)
            {
                res.Add(role);
            }
        }

        return res;
    }

    void checkBuff(RoleControl role)
    {
        int playerTag = role.getRoleTag();
        role.getXY(out int x, out int y);
        PathNode node = MapDataMgr.Instance.getPathNode(x, y);

        List<RoleControl> list = getNeighbourRoleList(node, playerTag);

        foreach (RoleControl role1 in list)
        {
            if (role1.getBuffById(21) == null)
            {
                role1.addBuff(new Buff21(1));
            }
        }
    }

    public override void onMapRoleChange(int playTag)
    {
        checkBuff(role);
    }
}
