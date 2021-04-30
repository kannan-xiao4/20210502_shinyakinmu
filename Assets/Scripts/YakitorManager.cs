using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class YakitorManager : MonoBehaviour
{
    [SerializeField] private List<Image> yakitoriImages;
    [SerializeField] private Transform parent;

    private void Start()
    {
        
        
        this.UpdateAsObservable()
            .Where(x => Input.anyKeyDown)
            .Throttle(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                var child = parent.GetChild(2);
                child.GetComponent<Image>().color = Random.ColorHSV();
                parent.GetChild(0).SetAsFirstSibling();

            }).AddTo(this);
    }
}