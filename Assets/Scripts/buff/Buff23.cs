

using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Buff23 : BuffBase
{
    int addAtk;
    int addDef;
    public Buff23(int type) : base(type)
    {
        addAtk = -1;
        addDef = -1;
    }

    public override void initData()
    {
        base.initData();
        id = 23;
        name = "周围敌方单位获得-1攻、-1防的技能Buff";
    }

    void enableBuff(RoleControl role)
    {
        role.addAtkValue(addAtk);
        role.addDefValue(addDef);
    }

    void disableBuff(RoleControl role)
    {
        role.addAtkValue(-addAtk);
        role.addDefValue(-addDef);
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

    public override void buffStart(RoleControl role)
    {
        enableBuff(role);
    }
    public override void roleMapChange(RoleControl role, int playerTag)
    {
        int tag = role.getRoleTag();
        if (tag == playerTag) return;


        role.getXY(out int x, out int y);
        PathNode node = MapDataMgr.Instance.getPathNode(x, y);

        List<RoleControl> list = getNeighbourEnemyRoleList(node, tag);

        bool isExist = false;
        foreach (RoleControl role1 in list)
        {
            if (role1.getSkillById(33) != null)
            {
                isExist = true;
                break;
            }
        }
        if (!isExist)
        {
            disableBuff(role);
        }

    }

}
