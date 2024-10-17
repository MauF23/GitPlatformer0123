using DG.Tweening.Core.Easing;
using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUi : MonoBehaviour
{
    public GameObject heartIcon;
    public Transform anchor;
    private List<Image> iconList = new List<Image>();

    public static HeartUi instance; 
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
	}

    public void InstantiateHearts(int maxHp)
    {
		for (int i = 0; i < maxHp; i++)
		{
			Image heart = Instantiate(heartIcon, anchor).GetComponent<Image>();

            if (heart != null)
            {
                iconList.Add(heart);
            }
		}
	}


    public void ReflectHp(int currentHp)
    {
        for(int i = 0; i < iconList.Count; i++)
        {
            if(i < currentHp)
            {
                iconList[i].color = Color.red;
			}
            else
            {
				iconList[i].color = Color.black;
			}
        }
    }
}
