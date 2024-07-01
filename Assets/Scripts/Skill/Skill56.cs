
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skill56 : SkillBase
{

    int distance;
    float damage;
    float damage2;

    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    public Skill56() : base()
    {
        id = 56;
        //主动
        type = 2;
        controlType = 2;

        name = "幻刺";
        description = "向前方一纵行（最多4格）冲锋，对沿途所有敌人造成3点伤害，路径上的最后一个敌人额外造成3点伤害，若路径末端存在敌人且未被杀死，则光灵回到初始位置。（不可越过敌方城墙）";

        list = new List<PathNode>();
        colors = new List<Color>();
    }

    public override void initData()
    {
        base.initData();

        distance = 1;
        damage = 3f;
        damage2 = 3f;
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

        role.getXY(out int rx, out int ry);
        int dx = x - rx;
        int dy = y - ry;
        int playerTag = role.getRoleTag();

        for (int i = 1; i <= 4; i++)
        {
            int tx = rx + dx * i;
            int ty = ry + dy * i;
            PathNode node = MapDataMgr.Instance.getPathNode(tx, ty);
            int tileType = MapDataMgr.Instance.getGridType(tx, ty);
            if (node == null || (playerTag == 1 && tileType == 311) || (playerTag == 2 && tileType == 301))
            {
                break;
            }

            Color color = MapDataMgr.Instance.getAreaColor(tx, ty);
            list.Add(node);
            colors.Add(color);
        }

        return list;
    }

    public override void onHoverMapGrid(int x, int y)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //颜色不一样
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

        bool isMove = false;
        for (int i = 0; i < list.Count; i++)
        {
            PathNode node = list[i];
            RoleControl enemy = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (enemy && enemy.getRoleTag() != playerTag)
            {
                float d = damage;
                if (i == list.Count - 1)
                {
                    d += damage2;
                }
                CombatSystem.Instance.roleMagicEnemy(role, enemy, d, null);
            }
        }
        int tx = list[list.Count - 1].x;
        int ty = list[list.Count - 1].y;
        RoleControl enemy1 = RoleDataMgr.Instance.getRoleControl(tx, ty);
        if (enemy1 == null || (enemy1.getRoleTag() != playerTag && !enemy1.isLife()))
        {
            isMove = true;
        }

        if (isMove)
        {
            CombatSystem.Instance.roleForceMove(role, tx, ty);
        }

        useSkill();
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
