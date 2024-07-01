
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : SkillBase
{

    int distance;
    List<PathNode> list;
    List<Color> colors;
    List<PathNode> area;
    List<PathNode> roleList;
    public Skill1() : base()
    {
        id = 1;
        //主动
        type = 2;
        controlType = 2;

        name = "复活";
        description = "复活一个此回合内阵亡的友方非英雄单位 （距离4）";

        list = new List<PathNode>();
        colors = new List<Color>();
    }

    public override void initData()
    {
        base.initData();
        
        distance = 4;
    }

    //点击技能
    public override void onClickSkill()
    {
        role.getXY(out int x, out int y);
        area = MapDataMgr.Instance.getRadiusArea(x, y, distance);
        MapDataMgr.Instance.showChangeArea(area, new Color(1f, 1f, 0f, 0.5f));

        roleList = new List<PathNode>();

        List<RoleData> deathList = RoleDataMgr.Instance.getDeathRoleList(role.getRoleTag());
        foreach (RoleData death in deathList)
        {
            PathNode node = MapDataMgr.Instance.getPathNode(death.x, death.y);
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

        int[,] pos = { { 0, 0 }};
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
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        PathNode clickNode = MapDataMgr.Instance.getPathNode(x, y);
        List<RoleData> deathList = RoleDataMgr.Instance.getDeathRoleList(playerTag);

        bool isuse = false;
        foreach (RoleData death in deathList)
        {
            PathNode node = MapDataMgr.Instance.getPathNode(death.x, death.y);
            if (node.isThisNode(clickNode) && roleList.Contains(clickNode))
            {
                RoleData newRole = RoleDataMgr.Instance.createRoleData(death.id);
                newRole.x = x;
                newRole.y = y;
                player.addRole(newRole);
                RoleDataMgr.Instance.placeRole(newRole);

                isuse = true;
                
            }
        }
        if (isuse)
        {
            //进入cd
            useSkill();
        }
    }

    public override void cancelSkill()
    {
        OnEvent.Instance.emit("onHideChangeArea");
    }
}
