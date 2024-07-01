
using System.Collections.Generic;
using UnityEngine;

public class Skill57 : SkillBase
{
    bool isAddBuff;
    int distance;
    int distance2;
    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    List<PathNode> roleList;

    RoleControl roleControl;
    public Skill57() : base()
    {
        id = 57;
        //主动
        type = 2;
        controlType = 3;

        name = "制裁";
        description = "将一个单位拉向自己周围4格的其中一个位置（可选择落点），若该单位是敌人时，被拉取的单位此回合不可攻击、反击（距离3）。";

        list = new List<PathNode>();
        colors = new List<Color>();
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;

        distance = 3;
        distance2 = 1;
    }

    //点击技能
    public override void onClickSkill()
    {
        role.getXY(out int x, out int y);
        area = MapDataMgr.Instance.getRadiusArea(x, y, distance);
        MapDataMgr.Instance.showChangeArea(area, new Color(1f, 1f, 0f, 0.5f));

        roleList = new List<PathNode>();
        foreach (PathNode node in area)
        {
            RoleControl role1 = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (role1 != null)
            {
                roleList.Add(node);
            }
        }

        MapDataMgr.Instance.showChangeArea(roleList, new Color(0.1f, 1f, 0.1f, 0.5f));
    }

    public List<PathNode> getAreaList(int x, int y)
    {
        PathNode point = MapDataMgr.Instance.getPathNode(x, y);
        if (!area.Contains(point))
        {
            return null;
        }

        int[,] pos = { { 0, 0 } };
        for (int i = 0; i < pos.GetLength(0); i++)
        {
            int tx = x + pos[i, 0];
            int ty = y + pos[i, 1];
            PathNode node = MapDataMgr.Instance.getPathNode(tx, ty);
            if (node != null)
            {
                Color color = MapDataMgr.Instance.getAreaColor(tx, ty);
                list.Add(node);
                colors.Add(color);
            }
        }

        return list;
    }

    public override void onHoverMapGrid(int x, int y)
    {
        for (int i = 0; i < list.Count; i++)
        {
            MapDataMgr.Instance.showChangeArea(new List<PathNode>() { list[i] }, colors[i]);
        }

        list.Clear();
        colors.Clear();

        getAreaList(x, y);

        MapDataMgr.Instance.showChangeArea(list, new Color(1f, 1f, 1f, 0.5f));
    }

    //施放技能
    public override void fireRole(int x, int y)
    {
        int playerTag = role.getRoleTag();
        PathNode clickNode = MapDataMgr.Instance.getPathNode(x, y);

        if (!roleList.Contains(clickNode))
        {
            return;
        }

        roleControl = RoleDataMgr.Instance.getRoleControl(x, y);
        isAddBuff = playerTag != roleControl.getRoleTag();
    }


    //点击技能
    public override void onClickSkill2()
    {
        role.getXY(out int x, out int y);
        area = MapDataMgr.Instance.getRadiusArea(x, y, distance2);
        MapDataMgr.Instance.showChangeArea(area, new Color(1f, 1f, 0f, 0.5f));

        roleList = new List<PathNode>();
        foreach (PathNode node in area)
        {
            RoleControl role1 = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (role1 != null)
            {
                roleList.Add(node);
            }
        }

        MapDataMgr.Instance.showChangeArea(roleList, new Color(1f, 0.1f, 0.1f, 0.5f));
    }


    public override void onHoverMapGrid2(int x, int y)
    {
        for (int i = 0; i < list.Count; i++)
        {
            MapDataMgr.Instance.showChangeArea(new List<PathNode>() { list[i] }, colors[i]);
        }

        list.Clear();
        colors.Clear();

        getAreaList(x, y);

        MapDataMgr.Instance.showChangeArea(list, new Color(1f, 1f, 1f, 0.5f));
    }

    public override void fireRole2(int x, int y)
    {
        int playerTag = role.getRoleTag();
        PathNode clickNode = MapDataMgr.Instance.getPathNode(x, y);

        if (!area.Contains(clickNode) || roleList.Contains(clickNode))
        {
            return;
        }

        CombatSystem.Instance.roleForceMove(roleControl, x, y);
        if (isAddBuff)
        {
            roleControl.addBuff(new Buff27(1));
        }
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
