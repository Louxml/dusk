

using System.Collections.Generic;
using UnityEngine;

public class Buff14 : BuffBase
{

    public Buff14(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 14;
        name = "魔族天赋给周围敌人添加虚弱DeBuff";
    }

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

        foreach (RoleControl enemy in list)
        {
            if (enemy.getBuffById(15) == null)
            {
                enemy.addBuff(new Buff15(1));
            }
        }
    }


    public override void buffStart(RoleControl role)
    {
        checkBuff(role);
    }

    public override void roleMapChange(RoleControl role, int playerTag)
    {
        checkBuff(role);
    }

    public override void buffOver(RoleControl role)
    {

    }

}
