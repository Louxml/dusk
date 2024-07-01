

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class RoleModel : MonoBehaviour
{
    public RoleConfig roleConfig;

    public RoleData roleData;

    //附加的移动距离
    int addMove;
    //附加的攻击距离
    int addRange;
    //附加的攻击力
    int addAtk;
    //附加的防御力
    int addDef;

    //附加城墙伤害
    float addWallDamage;
    //城墙伤害倍率
    float wallDamageFactor;
    //附加的伤害
    float addDamage;
    //附加的反击伤害
    float addBackDamage;

    //无视敌方护甲
    public float subEnemyDef;

    //移动次数cd
    public int moveTCD;
    //攻击次数cd
    public int attackTCD;
    //反击次数cd
    public int backAttackTCD;

    //是否承受反击
    public bool isCanBack;

    //是否死亡反击
    public bool isDeathBack;

    //是否忽略障碍
    public bool ignorePass;

    //是否占领格子
    public bool isUpdatePass;

    public int moveCD;
    public int attackCD;
    public int backAttackCD;

    void Start()
    {

    }

    public void initData()
    {
        addMove = 0;
        addRange = 0;
        addAtk = 0;
        addDef = 0;
        addWallDamage = 0;
        wallDamageFactor = 1;
        addDamage = 0;
        addBackDamage = 0;
        subEnemyDef = 0;
        moveTCD = 1;
        attackTCD = 1;
        backAttackTCD = 1;

        moveCD = 0;
        attackCD = 0;
        backAttackCD = 0;
        //默认承受反击
        isCanBack = true;
        //默认没有死亡反击
        isDeathBack = false;
        //默认不忽略
        ignorePass = false;
        // 默认占领
        isUpdatePass = true;

        roleData.hp = roleConfig.hp;
        roleData.moveTimes = roleConfig.moveTimes;
        roleData.attackTimes = roleConfig.attackTimes;
        roleData.backAttackTimes = roleConfig.backAttackTimes;
        roleData.skillTimes = 1;
    }

    //设置位置
    public void setXY(int x, int y)
    {
        roleData.x = x;
        roleData.y = y;
    }

    public void setRoleData(RoleData data)
    {
        roleData = data;
    }

    public (int x, int y) getXY()
    {
        return (roleData.x, roleData.y);
    }

    public int getRoleID()
    {
        return roleData.id;
    }

    public int getRoleType()
    {
        return roleConfig.roleType;
    }
    
    //获取棋子阵营
    public int getRoleTag()
    {
        return roleData.tag;
    }
    //获取棋子大小（一般为1，大型棋子为2）
    public int getRoleSize()
    {
        return roleConfig.size;
    }

    //获取移动次数
    public int getMoveTimes()
    {
        return roleData.moveTimes;
    }

    //获取移动距离
    public int getMoveDistance()
    {
        int d = roleConfig.move + addMove;
        return roleData.moveTimes > 0 ? d : 0;
    }

    //移动次数减一
    public void moveOneTimes()
    {
        roleData.moveTimes--;
    }
    
    //获取攻击次数
    public int getAttackTimes()
    {
        return roleData.attackTimes;
    }

    //刷新棋子数据（次数）
    public void updateRoleData()
    {
        moveCD = Math.Max(0, --moveCD);
        attackCD = Math.Max(0, --attackCD);
        backAttackCD = Math.Max(0, --backAttackCD);

        Debug.Log("CD:" + moveCD + "," + attackCD + "," + backAttackCD);
        if (moveCD <= 0)
        {
            roleData.moveTimes = roleConfig.moveTimes;
        }
        if (attackCD <= 0)
        {
            roleData.attackTimes = roleConfig.attackTimes;
        }
        if (backAttackCD == 0)
        {
            roleData.backAttackTimes = roleConfig.backAttackTimes;
        }
        roleData.skillTimes = 1;
    }


    //获取攻击距离
    public int getAttackDistance()
    {
        int d = roleConfig.range + addRange;
        return roleData.attackTimes > 0 ? d : 0;
    }

    //攻击次数减一
    public void attackOneTimes()
    {
        roleData.attackTimes--;
    }
    
   //获取反击次数
    public int getBackAttackTimes()
    {
        return roleData.backAttackTimes;
    }
    //获取反击距离
    public int getBackAttackDistance()
    {
        return roleData.backAttackTimes > 0 ? roleConfig.range : 0;
    }
    public void backAttackOneTimes()
    {
        roleData.backAttackTimes--;
    }
    //获取技能次数
    public int getSkillTimes()
    {
        return roleData.skillTimes;
    }

    //扣除生命值
    public void calcDamage(float damage)
    {
        roleData.hp -= damage;
    }
    
    public void addHp(float hp, bool isMore)
    {
        if (!isMore)
        {
            float max = roleConfig.hp - roleData.hp;
            hp = Mathf.Min(hp, max);
        }
        
        roleData.hp += hp;
    }

    //是否存活
    public bool isLife()
    {
        return roleData.hp > 0;
    }

    //获得攻击力
    public int getAtk()
    {
        int atk = roleConfig.atk + addAtk;
        return Mathf.Max(atk, 0);
    }
    
    //获取防御力
    public int getDef()
    {
        int def = roleConfig.def + addDef;
        return Mathf.Max(def, 0);
    }

    //添加buff
    public void addBuff(BuffBase buff)
    {
        if (buff == null) return;
        roleData.buffs.Add(buff);
    }

    //添加移动距离（附加属性）
    public void addMoveDistance(int value)
    {
        Debug.Log("addMove, update");
        addMove += value;
    }

    //添加攻击距离（附加属性）
    public void addRangeDistance(int value)
    {
        addRange += value;
    }

    //添加攻击力（附加属性）
    public void addAtkValue(int value)
    {
        addAtk += value;
    }

    //添加防御力（附加属性）
    public void addDefValue(int value)
    {
        addDef += value;
    }

    //添加城墙伤害（附加属性）
    public void addWallDamageFactor(int value)
    {
        wallDamageFactor += value;
    }

    //添加移动次数（附加属性）
    public void addMoveTimes(int value)
    {
        roleData.moveTimes += value;
    }

    //添加攻击次数（附加属性）
    public void addAttackTimes(int value)
    {
        roleData.attackTimes += value;
    }

    //添加伤害（附加数值）
    public void addDamageValue(float value)
    {
        addDamage += value;
    }

    //添加反击伤害（附加数值）
    public void addBackDamageValue(float value)
    {
        addBackDamage += value;
    }

    //添加敌方护甲削减

    public void removeBuff(BuffBase buff)
    {
        if (buff == null || !roleData.buffs.Contains(buff)) return;
        roleData.buffs.Remove(buff);
    }

    //TODO 可优化成列表深拷贝，不用直接引用，中途删除会出问题
    public List<BuffBase> getBuffList()
    {
        return roleData.buffs;
    }

    //获取附加的属性（移动距离）
    public int getAddMove()
    {
        return addMove;
    }

    //获取附加的属性（攻击距离）
    public int getAddRange()
    {
        return addRange;
    }

    //获取附加的属性（攻击力）
    public int getAddAtk()
    {
        return addAtk;
    }

    //获取附加的属性（防御力）
    public int getAddDef()
    {
        return addDef;
    }

    //获取附加属性（城墙伤害）
    public float getWallDamage()
    {
        float damage = roleConfig.atk;
        return (damage + addWallDamage) * wallDamageFactor;
    }

    //获取附加属性（附加伤害）
    public float getAddDamage()
    {
        return addDamage;
    }

    //获取附加属性（附加反击伤害）
    public float getAddBackDamage()
    {
        return addBackDamage;
    }

    public List<SkillBase> getSkillList()
    {
        return roleData.skills.ToList();
    }

    public Dictionary<string, float> getDebugInfo()
    {

        Dictionary<string, float> data = new Dictionary<string, float>();
        data["addMove"] = addMove;
        data["addRange"] = addRange;
        data["addAtk"] = addAtk;
        data["addDef"] = addDef;
        data["addWallDamage"] = addWallDamage;
        data["wallDamageFactor"] = wallDamageFactor;
        data["addDamage"] = addDamage;
        data["addBackDamage"] = addBackDamage;
        data["subEnemyDef"] = subEnemyDef;

        return data;
    }
}
