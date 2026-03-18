using System.Collections;
using System.Collections.Generic;
using Battle;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupText : MonoBehaviour
{
    public Text TextNormalAD;
    public Text TextNormalAP;
    public Text TextCritAD;
    public Text TextCritAP;
    public Text TextHeal;
    public float floatTime = 0.5f;

    Vector3 originPosition;

    string assetName;

    internal void InitPopup(DamageType damageType, float damage, bool isCrit, string assetName)
    {
        if (originPosition == Vector3.zero)
        {
            originPosition = this.transform.position;
        }
        else
        {
            this.transform.position = originPosition;
        }
        this.assetName = assetName;
        string text = damage.ToString("0");
        switch (damageType)
        {
            case DamageType.AD:
                if (damage > 0) return;
                if (isCrit)
                {
                    TextCritAD.text = string.Format("{0}!", text);
                    TextCritAD.enabled = true;
                }
                else
                {
                    TextNormalAD.text = text;
                    TextNormalAD.gameObject.SetActive(true);
                }
                break;
            case DamageType.AP:
                if (damage > 0) return;
                if (isCrit)
                {
                    TextCritAP.text = string.Format("{0}!", text);
                    TextCritAP.enabled = true;
                }
                else
                {
                    TextNormalAP.text = text;
                    TextNormalAP.enabled = true;
                }
                break;
            case DamageType.Real:
                if (damage < 0) return;
                TextHeal.text = text;
                TextHeal.enabled = true;
                break;
            case DamageType.Heal:
                break;
        }
        float time = Random.Range(0f, 0.5f) + floatTime;

        float height = Random.Range(10f, 20f);
        float disperse = Random.Range(-5f, 5f);
        disperse += Mathf.Sign(disperse) * 2f;

        LeanTween.moveX(this.gameObject, this.transform.position.x + disperse, time);
        LeanTween.moveZ(this.gameObject, this.transform.position.z + disperse, time);
        LeanTween.moveY(this.gameObject, this.transform.position.y + height, time).setEaseOutBack().setOnComplete(Close);
    }

    public void Close()
    {
        Manager.Pool.UnSpawn(AppConfig.UIPool, this.assetName, this.gameObject);
    }
}
