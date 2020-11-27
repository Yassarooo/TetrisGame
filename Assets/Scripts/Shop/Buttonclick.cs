using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class Buttonclick
{
     public static void AddEvent<T>(this Button button,T index,Action<T> OnClick)
    {
        button.onClick.AddListener(delegate (){
                OnClick(index);

        });
    }    
}
