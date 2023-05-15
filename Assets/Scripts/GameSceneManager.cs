using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    GameObject HUDTitle;
    [SerializeField]
    GameObject HUDIntroAndStart;
    [SerializeField]
    GameObject HUDTutorial;
    [SerializeField]
    TextMeshProUGUI HUDTutorialText;
    [SerializeField]
    TextMeshProUGUI HUDTutorialButtonText;

    [SerializeField]
    GameObject HUDGame;
    [SerializeField]
    TextMeshProUGUI HUDScore;
    [SerializeField]
    GameObject HUDCorrect;
    [SerializeField]
    GameObject HUDGameOver;
    [SerializeField]
    TextMeshProUGUI HUDGameOverText;
    [SerializeField]
    GameObject HUDReplay;
    [SerializeField]
    GameObject HUDPlayButtons;

    [SerializeField]
    TextMeshProUGUI[] Digits;
    [SerializeField]
    GameObject[] GraphPoints;
    string binaryNum;

    [SerializeField]
    GameObject Graph;

    bool isPlaying = false;
    int currentScore = 0;
    float correctTimer = 0f;
    float correctTimerMax = 2f;

    int tutorialNum = 0;
    string[] tutorialStrings = {
        "Modulation is used to transmit binary numbers (0s and 1s) over radio waves.",
        "The shape of the graph determines if a 0 or a 1 is being sent.",
        "Now you try! Amplitude Shift Keying has been used to send binary numbers over a radio wave. Try to decode each binary number.\n\nReady?"
    };


    // Start is called before the first frame update
    void Start()
    {
        audioManager = this.GetComponent<AudioManager>();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (correctTimer > 0)
        {
            correctTimer -= Time.deltaTime;
            if (correctTimer < 0)
            {
                HUDCorrect.SetActive(false);
                Graph.SetActive(true);
            }
        }
    }

    public void StartTutorial()
    {
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();
        HUDTutorial.GetComponent<MoveNormal>().MoveUp();
        audioManager.PlaySelectSound();
    }

    public void AdvanceTutorial()
    {
        tutorialNum++;
        if (tutorialNum >= tutorialStrings.Length)
        {
            // start game
            StartGame();
        }
        else
        {
            audioManager.PlayMenuSound();
            // next tutorial
            HUDTutorialText.text = tutorialStrings[tutorialNum];
            if (tutorialNum == tutorialStrings.Length - 1)
                HUDTutorialButtonText.text = "BEGIN";
        }
    }

    public void StartGame()
    {
        audioManager.PlaySelectSound();
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();

        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDReplay.GetComponent<MoveNormal>().MoveDown();
        currentScore = 0;
        HUDScore.text = currentScore.ToString();
        GenerateNumber();
        HUDGame.GetComponent<MoveNormal>().MoveDown();
        HUDPlayButtons.GetComponent<MoveNormal>().MoveUp();
        isPlaying = true;
    }

    public void GenerateNumber()
    {
        foreach (TextMeshProUGUI digit in Digits)
        {
            digit.text = "?";
        }

        binaryNum = "";
        for (int x = 0; x < 4; x++)
        {
            int val = Random.Range(0, 2);
            if (val == 0)
            {
                binaryNum = binaryNum + "0";
                GraphPoints[x * 2 + 1].transform.localPosition = new Vector3(GraphPoints[x * 2 + 1].transform.localPosition.x, .5f, 0);
                GraphPoints[x * 2 + 2].transform.localPosition = new Vector3(GraphPoints[x * 2 + 2].transform.localPosition.x, -.5f, 0);
            }
            else
            {
                binaryNum = binaryNum + "1";
                GraphPoints[x * 2 + 1].transform.localPosition = new Vector3(GraphPoints[x * 2 + 1].transform.localPosition.x, 2f, 0);
                GraphPoints[x * 2 + 2].transform.localPosition = new Vector3(GraphPoints[x * 2 + 2].transform.localPosition.x, -2f, 0);
            }
        }
        Debug.Log(binaryNum);
    }

    public void SetDigit(int digitNum)
    {
        audioManager.PlayMenuSound();
        string currDigit = Digits[digitNum].text;
        if (currDigit == "?" || currDigit == "1")
            Digits[digitNum].text = "0";
        else
            Digits[digitNum].text = "1";
    }

    public void SubmitAnswer()
    {
        if (correctTimer > 0)
            return;
        audioManager.PlaySelectSound();
        string guess = Digits[0].text + Digits[1].text + Digits[2].text + Digits[3].text;
        if (guess == binaryNum)
        {
            CorrectAnswer();
        }
        else
        {
            HUDGameOverText.text = "WRONG! The number is " + binaryNum + ".";
            GameOver();
        }
        HUDScore.text = currentScore.ToString();
    }

    public void CorrectAnswer()
    {
        audioManager.PlayCorrectSound();
        currentScore++;
        HUDCorrect.transform.localScale = new Vector3(.1f, .1f, .1f);
        HUDCorrect.SetActive(true);
        HUDCorrect.GetComponent<GrowAndShrink>().StartEffect();
        correctTimer = correctTimerMax;
        Graph.SetActive(false);
        GenerateNumber();
    }

    public void GameOver()
    {
        audioManager.PlayWrongSound();
        isPlaying = false;
        HUDGameOver.GetComponent<MoveNormal>().MoveDown();
        HUDReplay.GetComponent<MoveNormal>().MoveUp();
        HUDPlayButtons.GetComponent<MoveNormal>().MoveDown();
    }

    public void SelectHome()
    {
        audioManager.PlaySelectSound();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
        HUDGame.GetComponent<MoveNormal>().MoveUp();
        HUDPlayButtons.GetComponent<MoveNormal>().MoveUp();
    }
}
