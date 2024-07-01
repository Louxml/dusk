

using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Buff22 : BuffBase
{
    int addDef;
    public Buff22(int type) : base(type)
    {
        addDef = 2;
    }

    public override void initData()
    {
        base.initData();
        id = 22;
        name = "周围友方单位获得+2防的技能Buff";
    }

    void enableBuff(RoleControl role)
    {
        role.addDefValue(addDef);
    }

    void disableBuff(RoleControl role)
    {
        role.addDefValue(-addDef);
    }

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

        List<RoleControl> list = getNeighbourRoleList(node, tag);

        bool isExist = false;
        foreach (RoleControl role1 in list)
        {
            if (role1.getSkillById(32) != null)
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
