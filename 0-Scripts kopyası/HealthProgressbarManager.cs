using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthProgressbarManager : MonoBehaviour
{
   public Image progressbar;
    //public Slider progressbar;
    public void SetProgressBar(float aRate)
    {
        //bi yolu da gecen ders sadullahin bahsettigi gibi direk progresbar;
        progressbar.transform.localScale = new Vector3(aRate, 1, 1);//daha zahmetsiz ama setupumuzda d'kkat etmem'z gereken progresbar image nin anchorlarinin
    }
}
