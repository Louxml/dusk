

using UnityEngine;

public class Buff16 : BuffBase
{
    public Buff16(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 16;
        name = "亡灵天赋死亡生成出生点";
    }

    public override void buffStart(RoleControl role)
    {
        Debug.Log("16 挂载");
    }

    public override void roleDeath(RoleControl role, RoleControl enemy)
    {
        int playerTag = role.getRoleTag();
        role.getXY(out int x, out int y);
        PathNode node = MapDataMgr.Instance.getPathNode(x, y);
        MapDataMgr.Instance.addBothNode(node, playerTag, 1);
    }

}
