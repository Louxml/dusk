
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skill50 : SkillBase
{
    float addHp;
    int distance;
    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    List<PathNode> roleList;
    public Skill50() : base()
    {
        id = 50;
        //主动
        type = 2;
        controlType = 2;

        name = "治疗";
        description = "恢复目标单位2生命（距离4）";

        list = new List<PathNode>();
        colors = new List<Color>();
    }

    public override void initData()
    {
        base.initData();

        distance = 4;

        addHp = 2f;
    }

    //点击技能
    public override void onClickSkill()
    {
        role.getXY(out int x, out int y);
        area = MapDataMgr.Instance.getRadiusArea(x, y, distance);
        MapDataMgr.Instance.showChangeArea(area, new Color(1f, 1f, 0f, 0.5f));

        roleList = new List<PathNode>();

        List<RoleControl> list1 = RoleDataMgr.Instance.getRoleList(role.getRoleTag());
        foreach (RoleControl role1 in list1)
        {
            role.getXY(out int rx, out int ry);
            PathNode node = MapDataMgr.Instance.getPathNode(rx, ry);
            if (area.Contains(node))
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
        PathNode clickNode = MapDataMgr.Instance.getPathNode(x, y);
        if (!area.Contains(clickNode))
        {
            return;
        }
        int playerTag = role.getRoleTag();

        RoleControl role1 = RoleDataMgr.Instance.getRoleControl(x, y);
        if (role1.getRoleTag() != role.getRoleTag())
        {
            return;
        }

        role1.addHp(addHp, false);

        //进入cd
        useSkill();
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
