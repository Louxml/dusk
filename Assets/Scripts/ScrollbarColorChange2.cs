using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ScrollbarColorChange2 : MonoBehaviour
{
    public Scrollbar scrollbar;
    public Image fillImage; // 你要改变颜色的部分，假设是背景的一个子对象

    private void Update()
    {
        if (scrollbar != null && fillImage != null)
        {
            fillImage.fillAmount = scrollbar.value;
        }
    }
}
