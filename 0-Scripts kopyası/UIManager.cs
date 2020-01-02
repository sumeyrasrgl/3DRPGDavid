using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text messageText;
    public Transform saveLoadPanel;
    public Button saveButton, loadButton, benjaminButton;
    public Image healthPbContainerImage, healthPbImage;
    public Color pbUsualColor, pbLowColor;
    public Gradient healthPbColor;
    public Animator progressBarAnimator;

    private bool coroutineBlocker;
    private float maxPbWidth;
    private float pbLowThreshold = 0.3f;
    private float pbLowAnimationThreshold = 0.2f;
    
    
    

    private List<MessageToPlayer> messages = new List<MessageToPlayer>();

    private void Start() {
        saveButton.onClick.AddListener(SaveButtonFunction);
        loadButton.onClick.AddListener(LoadButtonFunction);
        benjaminButton.onClick.AddListener(BenjaminButtonFunction);
        
        maxPbWidth = healthPbContainerImage.GetComponent<RectTransform>().rect.width;
        HealthManager hm = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        float currentRate = hm.health / hm.maxHealth;
        SetHealthProgressbar(currentRate);
        

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (!saveLoadPanel.gameObject.activeSelf) {
                Utilities.utilities.UnlockMouse();

            } else {
                Utilities.utilities.LockMouse();
                
            }
            BenjaminButtonFunction();
        }
    }
    public void SetHealthProgressbar(float aRate) {
        healthPbImage.GetComponent<RectTransform>().sizeDelta = new Vector2(
            maxPbWidth * aRate, 
            healthPbImage.GetComponent<RectTransform>().sizeDelta.y
        );
        if (aRate<pbLowAnimationThreshold) {
            progressBarAnimator.enabled=true;
            progressBarAnimator.SetTrigger("warnByColorChange");
        } else {
            progressBarAnimator.SetTrigger("endWarningByColor");
            progressBarAnimator.enabled=false;
            healthPbImage.color =  aRate<pbLowThreshold ? pbLowColor : pbUsualColor;   
            //healthPbImage.color = healthPbColor.Evaluate(Mathf.Abs(1-aRate));
        }  
    }

    private void SaveButtonFunction() {
        DataManager.dataManager.Save();
    }
    private void LoadButtonFunction() {
        DataManager.dataManager.Load();
    }
    private void BenjaminButtonFunction() {
        saveLoadPanel.gameObject.SetActive(!saveLoadPanel.gameObject.activeSelf);
    }
    
    public void SetMessageText(string aMessage, float duration) {
        MessageToPlayer m = new MessageToPlayer(aMessage, duration);
        messages.Add(m);
        if (!coroutineBlocker) {
            coroutineBlocker=true;
            StartCoroutine(EndMessages());
        }
    }

    private IEnumerator EndMessages() {
        messageText.text = messages[0].messaage;
        yield return new WaitForSeconds(messages[0].duration);
        messageText.text = "";
        messages.RemoveAt(0);
        if (messages.Count==0) {
            StopCoroutine(EndMessages());
            coroutineBlocker=false;
        } else {
            yield return EndMessages();
        } 
    }


}

public class MessageToPlayer {
    public string messaage;
    public float duration;

    public MessageToPlayer (string aMessage, float aDuration) {
        this.messaage = aMessage;
        this.duration = aDuration;
    }
}
