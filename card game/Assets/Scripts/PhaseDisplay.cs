using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplay : MonoBehaviour
{
    public Text phaseText;
    // Start is called before the first frame update
    void Start()
    {
        Battlemanager1.Instance.phaseChangeEvent.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }
    void UpdateText()
    {
        phaseText.text = Battlemanager1.Instance.currentphase.ToString();
    }
}
