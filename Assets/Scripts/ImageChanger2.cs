using UnityEngine;
using UnityEngine.UI;

public class ImageChanger2 : MonoBehaviour
{
    public Scrollbar scrollbar; // Scrollbar 组件的引用
    public Image targetImage; // 要改变图片的 Image 组件的引用
    public Sprite imageForZero; // Scrollbar 值为 0 时的图片
    public Sprite imageForNonZero; // Scrollbar 值不为 0 时的图片
    public Vector2 sizeForZero; // Scrollbar 值为 0 时的图片大小
    public Vector2 sizeForNonZero; // Scrollbar 值不为 0 时的图片大小

    void Start()
    {
        // 注册滚动条值变化时的回调函数
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
    }

    // 滚动条值变化时调用的函数
    private void OnScrollbarValueChange(float value)
    {
        // 如果滚动条的值为 0，设置图片为 imageForZero
        // 否则，设置图片为 imageForNonZero
        targetImage.sprite = Mathf.Approximately(value, 0.0f) ? imageForZero : imageForNonZero;
        targetImage.rectTransform.sizeDelta = Mathf.Approximately(value, 0.0f) ? sizeForZero : sizeForNonZero;
    }

    void OnDestroy()
    {
        // 移除监听器，避免潜在的内存泄漏
        scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
    }
}