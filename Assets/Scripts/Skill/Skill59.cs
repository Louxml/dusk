
using System.Collections.Generic;
using UnityEngine;

public class Skill59 : SkillBase
{

    int distance;
    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    List<PathNode> roleList;
    public Skill59() : base()
    {
        id = 59;
        //主动
        type = 2;
        controlType = 2;

        name = "瘴谒";
        description = "让一个单位一回合无法行动或反击。（距离2）";

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

        distance = 2;
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
            if (role1 != null && role1.getRoleTag() != role.getRoleTag())
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

        if (!area.Contains(clickNode) || !roleList.Contains(clickNode))
        {
            return;
        }

        RoleControl enemy = RoleDataMgr.Instance.getRoleControl(x, y);
        enemy.addBuff(new Buff18(1));

        //进入cd
        useSkill();
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
