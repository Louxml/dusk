

using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Buff15 : BuffBase
{
    //城墙伤害
    int addBackDamage;
    int addDef;
    int addDef2;
    int active;
    public Buff15(int type) : base(type)
    {
        addBackDamage = -2;
        addDef = -2;
        addDef2 = -98;
        active = 0;
    }

    public override void initData()
    {
        base.initData();
        id = 15;
        name = "魔族天赋给周围敌人添加的虚弱DeBuff";
    }

    void enableBuff(RoleControl role)
    {
        if (active >= 1) return;
        active = 1;

        role.addDefValue(addDef);
        role.addBackDamageValue(addBackDamage);

        role.getDebugInfo("Enable1");
    }

    void disableBuff(RoleControl role)
    {
        if (active < 1) return;
        active = 0;

        role.addDefValue(-addDef);
        role.addBackDamageValue(-addBackDamage);


        role.getDebugInfo("Disable1");
    }

    void enableBuff2(RoleControl role)
    {
        if (active >= 2) return;
        active = 2;

        role.addDefValue(addDef2);

        role.getDebugInfo("Enable2");
    }

    void disableBuff2(RoleControl role)
    {
        if (active < 2) return;
        active = 1;

        role.addDefValue(-addDef2);

        role.getDebugInfo("Disable2");
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

    public override void roleMapChange(RoleControl role, int playerTag)
    {
        int tag = role.getRoleTag();
        if (tag == playerTag) return;

        
        role.getXY(out int x, out int y);
        PathNode node = MapDataMgr.Instance.getPathNode(x, y);

        List<RoleControl> list = getNeighbourEnemyRoleList(node, tag);

        if (list.Count >= 2)
        {
            enableBuff(role);
        }
        else
        {
            disableBuff(role);
        }

        if (list.Count >= 3)
        {
            enableBuff2(role);
        }
        else
        {
            disableBuff2(role);
        }
    }

}
