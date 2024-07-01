
using System.Collections.Generic;
using UnityEngine;

public class Skill33 : SkillBase
{
    public Skill33() : base()
    {
        id = 33;
        //被动
        type = 1;
        name = "削弱";
        description = "周围敌方单位-1攻、-1防";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    //获取周围敌方单位
    List<RoleControl> getNeighbourEnemyRoleList(PathNode node, int playerTag)
    {
        List<RoleControl> res = new List<RoleControl>();
        List<PathNode> list = MapDataMgr.Instance.getNeighbourList(node);

        foreach (PathNode n in list)
        {
            RoleControl role = RoleDataMgr.Instance.getRoleControl(n.x, n.y);
            if (role != null && role.getRoleTag() != playerTag)
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

        List<RoleControl> list = getNeighbourEnemyRoleList(node, playerTag);

        foreach (RoleControl role1 in list)
        {
            if (role1.getBuffById(23) == null)
            {
                role1.addBuff(new Buff23(1));
            }
        }
    }

    public override void onMapRoleChange(int playTag)
    {
        checkBuff(role);
    }
}
