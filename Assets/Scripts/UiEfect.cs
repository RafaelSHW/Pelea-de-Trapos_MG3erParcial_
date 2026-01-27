using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UiEfect : MonoBehaviour
{
    public void UiEffectCursorEnter(GameObject go)
    {
        go.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    public void UiEffectCursorExit(GameObject go)
    {
        go.transform.localScale = Vector3.one;
    }
}
