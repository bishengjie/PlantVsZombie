using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverPanel : MonoBehaviour
{
    private Image _image;
    private Image _panel;

    void Start()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _panel = transform.Find("Panel").GetComponent<Image>();
        _image.gameObject.SetActive(false);
        _panel.gameObject.SetActive(false);
        _panel.color = new Color(0, 0, 0, 0);
    }


    public void Over()
    {
        // 显示图片
        _image.gameObject.SetActive(true);
        // 让Panel渐变成黑色
        StartCoroutine(PanelColorEF());
    }

    IEnumerator PanelColorEF()
    {
        _panel.gameObject.SetActive(true);
        float a = 0;
        while (a < 1)
        {
            a += 0.02f;
            _panel.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.05f);
        }

        // 到这个位置意味着已经纯黑
        yield return new WaitForSeconds(2f);
        // 回到主场景
        DoBackMainScene();
    }


    private void DoBackMainScene()
    {
        SceneManager.LoadScene("Start");
    }
}
