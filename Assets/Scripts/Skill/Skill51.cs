
using System.Collections.Generic;
using UnityEngine;

public class Skill51 : SkillBase
{

    int distance;
    float damage;

    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    public Skill51() : base()
    {
        id = 51;
        //主动
        type = 2;
        controlType = 2;

        name = "光破";
        description = "对一个单位造成3点伤害，且该单位一回合内无法反击，护甲归0。（距离2）";

        list = new List<PathNode>();
        colors = new List<Color>();
    }

    public override void initData()
    {
        base.initData();

        distance = 2;
        damage = 3f;
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

        foreach (PathNode node in list)
        {
            RoleControl enemy = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (enemy.getRoleTag() != playerTag)
            {
                CombatSystem.Instance.roleMagicEnemy(role, enemy, damage, null);
                enemy.addBuff(new Buff26(1));
            }
        }

        useSkill();
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
