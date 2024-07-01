using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : MonoBehaviour
{
    public static GameDataMgr Instance;

    public PlayerData player1;
    public PlayerData player2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        initData();
    }

    public void initData()
    {
        player1 = new PlayerData(1);
        player1.setKind(1);
        player2 = new PlayerData(2);
        player2.setKind(2);

        int roleId = GameManager.Instance.Player1ChooseHero;

        //创建主棋子（玩家一英雄）
        RoleData role = RoleDataMgr.Instance.createRoleData(roleId);
        role.x = 5;
        role.y = 8;
        role.tag = 1;
        player1.addRole(role);
        
        //创建主棋子（玩家二英雄）
        roleId = GameManager.Instance.Player2ChooseHero;
        role = RoleDataMgr.Instance.createRoleData(roleId);
        role.x = 21;
        role.y = 8;
        role.tag = 2;
        player2.addRole(role);
    }

    public PlayerData getPlayerData(int id = 1)
    {
        if (id == 2) return player2;
        return player1;
    }


    public void add_player1_hero()
    {
        int roleId = GameManager.Instance.Player1ChooseHero;

        RoleData role = RoleDataMgr.Instance.createRoleData(roleId);
        role.x = 5;
        role.y = 8;
        role.tag = 1;
        player1.addRole(role);
    }

    public void add_player2_hero()
    {
        int roleId = GameManager.Instance.Player1ChooseHero;
        RoleData role = RoleDataMgr.Instance.createRoleData(roleId);
        role.x = 21;
        role.y = 8;
        role.tag = 2;
        player2.addRole(role);

    }
}

public class PlayerData
{
    public List<RoleData> roleList = new List<RoleData>();
    public int tag = 0;

    public float hp;
    public int gold;
    public int kind;
    public int hero;
    public int hun_used;
    public int hun_num;

    //神庙数量
    public int templeCount = 0;
    

    public PlayerData(int tag = 0)
    {
        // Test
        this.tag = tag;
        this.kind = 0;
        this.hp = 50f;
        this.gold = 0;
        this.hun_used = 0;
        this.hun_num = 0;

    }

    public void setKind(int kind)
    {
        this.kind = kind;
    }

    public void addRole(RoleData role)
    {
        role.tag = tag;
        roleList.Add(role);
    }
}


public class RoleData
{
    public int id;
    public int x;
    public int y;
    public int tag;
    public float hp;

    public int moveTimes;
    public int attackTimes;
    public int backAttackTimes;
    public int skillTimes;

    public List<BuffBase> buffs = new List<BuffBase>();
    public List<SkillBase> skills = new List<SkillBase>();


    public RoleData(int id = 0)
    {
        this.id = id;
        this.x = 0;
        this.y = 0;
    }
}