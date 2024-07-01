using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 在GameManager中定义需要持久化的变量
    public int Player1ChooseKind; //玩家选的种族
    public int Player2ChooseKind;

    public int Player1ChooseHero; //玩家选的英雄
    public int Player2ChooseHero;

    public int Player1golden=0; //玩家金币数
    public int Player2golden=0;

    public GameObject selected;

    public int nowplayer=1;

    public static int purchasedPrefab; // todo

    public static int buy_role_cost;

    public static int gamestate = 0;
    // gamestate 是游戏当前状态，0是普通状态，1是点击了购买 todo

    public int buyqizi_kind = 0;


    public int maiqizidejine;

    public int hero1_relive_cd = 0;
    public int hero2_relive_cd = 0;

    public int paotai1_cd = 0;
    public int paotai2_cd = 0;





    void Start()
    {

    }

    public static void SetPurchasedPrefab(int prefab)
    {
        purchasedPrefab = prefab;
    }


    public static int GetPurchasedPrefab()
    {
        return purchasedPrefab;
    }

    public static void SetBuyRoleCost(int cost)
    {
        buy_role_cost = cost;
    }


    public static int GetBuyRoleCost()
    {
        return buy_role_cost;
    }


    private void Awake()
    {
        // 确保只有一个GameManager实例存在
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

}
