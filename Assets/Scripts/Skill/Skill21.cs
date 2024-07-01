
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Skill21 : SkillBase
{
    public Skill21() : base()
    {
        id = 21;
        //被动
        type = 1;
        name = "复活";
        description = "仅有一次机会，死亡后可复活一次";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 9999;
        cd = 0;
    }

    public override void onAffectAfter(RoleControl enemy, float damage)
    {
        if (isUse() && !role.isLife())
        {
            int id = role.getRoleID();
            role.getXY(out int x, out int y);
            int tag = role.getRoleTag();

            useSkill();

            // 受击动画2秒后执行反击
            Sequence seq = DOTween.Sequence();
            seq.PrependInterval(2f).AppendCallback(() =>
            {
                createRole(id, tag, x, y);
            });
        }
    }

    public void createRole(int id, int tag, int x, int y)
    {
        RoleData role = RoleDataMgr.Instance.createRoleData(id);
        role.x = x;
        role.y = y;
        PlayerData player = GameDataMgr.Instance.getPlayerData(tag);

        player.addRole(role);

        RoleDataMgr.Instance.placeRole(role);
    }
}
