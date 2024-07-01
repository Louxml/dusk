
using System.Collections.Generic;
using UnityEngine;

public class Skill47 : SkillBase
{

    int distance;
    float damage;

    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    public Skill47() : base()
    {
        id = 47;
        //主动
        type = 2;
        controlType = 2;

        name = "生降";
        description = "对十字区域（5格）内敌人造成2伤害，-1防，无法反击。持续一回合（距离4）";

        list = new List<PathNode>();
        colors = new List<Color>();
    }

    public override void initData()
    {
        base.initData();

        distance = 4;
        damage = 2f;
    }

    //点击技能
    public override void onClickSkill()
    {
        role.getXY(out int x, out int y);
        area = MapDataMgr.Instance.getRadiusArea(x, y, distance);
        MapDataMgr.Instance.showChangeArea(area, new Color(1f, 1f, 0f, 0.5f));
    }


    public List<PathNode> getAreaList(int x, int y)
    {
        PathNode point = MapDataMgr.Instance.getPathNode(x, y);
        if (!area.Contains(point))
        {
            return null;
        }

        int[,] pos = { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
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

        foreach (PathNode node in list)
        {
            RoleControl enemy = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (enemy.getRoleTag() != playerTag)
            {
                CombatSystem.Instance.roleMagicEnemy(role, enemy, damage, null);
                enemy.addBuff(new Buff25(1));
            }
        }

        useSkill();
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
