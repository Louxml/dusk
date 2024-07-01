using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController2 : MonoBehaviour
{
    public static CannonController2 Instance;

    public float attackDuration = 1f; // 攻击动画的持续时间
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }



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

    // Update is called once per frame
    void Update()
    {
        RoleControl role = RoleDataMgr.Instance.getRoleControl(21, 8);
        if (role == null)
        {
            animator.SetBool("activity", false);
        }
        if (role && GameManager.Instance.paotai2_cd == 0)
        {
            animator.SetBool("activity", true);
        }
        
    }


    public void StartAttack()
    {
        animator.SetBool("attack", true); // 触发攻击动画
        StartCoroutine(AttackAndCooldown());
    }

    private IEnumerator AttackAndCooldown()
    {
        yield return new WaitForSeconds(attackDuration); // 等待攻击动画完成
        animator.SetBool("attack", false); // 设置冷却动画
        animator.SetBool("activity", false);
    }


}
