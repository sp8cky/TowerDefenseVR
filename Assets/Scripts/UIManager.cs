using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    private GameObject buildUI;
    private GameObject playerUI;
    private bool isPlayerUIOpen = true;
    private bool isBuildUIOpen = false;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text roundText;
    public TMP_Text stateText;
    public TMP_Text timerText;
    public TMP_Text baseHealthText;
    public TMP_Text playerHealthText;
    public TMP_Text playerCoinsText;
    public Button readyButton;
   
    
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        playerUI = GameObject.Find("PlayerUI");
        buildUI = GameObject.Find("BuildUI");
        if (buildUI == null || playerUI == null) Debug.Log("UIs not found");
        buildUI.SetActive(isBuildUIOpen);
        playerUI.SetActive(isPlayerUIOpen);

        scoreText.text = "Score: " + "0".ToString();
        roundText.text = "Round: " + "1".ToString();
        stateText.text = " - Preparation".ToString();
        baseHealthText.text = "Base Health: " + "20".ToString();
        playerHealthText.text = "Player Health: " + "100".ToString();
        playerCoinsText.text = "Player Coins: " + "100".ToString();
    } 

    // toggles player and build UI
    public void ToggleUIs() {
        if (isBuildUIOpen) {
            isBuildUIOpen = false;
            isPlayerUIOpen = true;
            buildUI.SetActive(false);
            ToggleUIComponents("Infos", true);
        } else if (isPlayerUIOpen) {
            isBuildUIOpen = true;
            isPlayerUIOpen = false;
            buildUI.SetActive(true);
            ToggleUIComponents("Infos", false);
        }
    }

    // helps to just toggle specific UI components
    void ToggleUIComponents(string name, bool show) {
        Transform objectsToHide = playerUI.transform.Find(name);
        if (objectsToHide != null) {
            foreach (Transform child in objectsToHide) child.gameObject.SetActive(show); 
        } else {
            Debug.LogError("objectsToHide nicht gefunden!");
        }
    }

    // change visibility of ready button
    public void ToggleReadyButton(bool isVisible) { readyButton.gameObject.SetActive(isVisible); }

    // update ui texts
    public void UpdatePlayerHealthText(int playerHealth) { playerHealthText.text = "Player Health: " + playerHealth.ToString(); }
    public void UpdatePlayerCoinsText(int playerCoins) { playerCoinsText.text = "Player Coins: " + playerCoins.ToString(); }
    public void UpdateBaseHealthText(int baseHealth) { baseHealthText.text = "Base Health: " + baseHealth.ToString(); }
    public void UpdatePlayerScoreText(int score) { scoreText.text = "Score: " + score.ToString(); }
    public void UpdatePlayerHighScoreText(int highScore) { highScoreText.text = "Highscore: " + highScore.ToString(); }
    public void UpdateRound(int round) { roundText.text = "Round: " + round.ToString(); }
    public void UpdateGameState(string state) { stateText.text = " - " + state; }
    public void UpdateTimerText(float time) {
        if (time <= 0) {
            timerText.text = "Timer: 0";
            GameManager.instance.isTimerRunning = false;
            GameManager.instance.ChangeGameState(); // start next round
        } else {
            timerText.text = "Timer: " + Mathf.RoundToInt(time).ToString();
        }
    }

    
}
