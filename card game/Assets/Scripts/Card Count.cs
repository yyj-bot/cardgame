using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    
public class CardCount : MonoBehaviour
{
    public Text Counter;
    private int counterN = 0;
    public bool SetCounter(int _value)
    {
        counterN += _value;
        OnCounterChange();
        if (counterN == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }
    private void OnCounterChange()
    {
       Counter.text = counterN.ToString();
    }
}
