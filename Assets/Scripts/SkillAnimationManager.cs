using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationManager : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
    }

    // 调用这个方法来播放特定技能动画
    public void PlaySkillAnimation(string skillName, Vector3 position)
    {
        // 移动SkillAnimations对象到指定位置
        transform.position = position;

        // 播放对应的动画
        animator.Play(skillName);
    }
}
