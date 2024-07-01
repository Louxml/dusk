

using System.Collections.Generic;
using UnityEngine;

public class Buff13 : BuffBase
{
    //附加属性
    int atk;
    int def;
    int activeCount;
    int count;
    bool isActive;
    public Buff13(int type) : base(type)
    {
        atk = 2;
        def = 1;
        activeCount = 3;
        count = 0;
        isActive = false;
    }

    public override void initData()
    {
        base.initData();
        id = 13;
        name = "人族天赋三个及以上单位相连时加属性";
    }

    void enableBuff(RoleControl role)
    {
        if (isActive) return;
        isActive = true;

        Debug.Log("Enable");
        role.addAtkValue(atk);
        role.addDefValue(def);
    }

    void disableBuff(RoleControl role)
    {
        if (!isActive) return;
        isActive = false;
        Debug.Log("Disable");
        role.addAtkValue(-atk);
        role.addDefValue(-def);
    }

    List<PathNode> getNeighbourRoleList(PathNode node, int playerTag)
    {
        List<PathNode> list = MapDataMgr.Instance.getNeighbourList(node);
        List<PathNode> res = new List<PathNode>();
        foreach (PathNode n in list)
        {
            RoleControl role = RoleDataMgr.Instance.getRoleControl(n.x, n.y);
            if (role?.getRoleTag() == playerTag)
            {
                res.Add(n);
            }
        }

        return res;
    }

    int getUnionCount(RoleControl role)
    {
        List<PathNode> res = new List<PathNode>();
        List<PathNode> list = new List<PathNode>();

        int playerTag = role.getRoleTag();

        role.getXY(out int x, out int y);
        PathNode node = MapDataMgr.Instance.getPathNode(x, y);

        list.Add(node);
        res.Add(node);

        while (list.Count > 0)
        {
            PathNode current = list[0];
            list.Remove(current);

            foreach (PathNode n in getNeighbourRoleList(current, playerTag))
            {
                if (!res.Contains(n) && !list.Contains(n))
                {
                    list.Add(n);
                    res.Add(n);
                }
            }
        }

        return res.Count;
    }

    public override void roleMapChange(RoleControl role, int playerTag)
    {
        if (role.getRoleTag() != playerTag) return;
        count = getUnionCount(role);
        if (count >= activeCount)
        {
            enableBuff(role);
            role.selectRole();
        }
        else
        {
            role.unSelectRole();
            disableBuff(role);
        }
    }

}
