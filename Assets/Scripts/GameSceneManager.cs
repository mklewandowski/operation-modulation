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

    bool isPlaying = false;
    int currentScore = 0;
    float correctTimer = 0f;
    float correctTimerMax = 1f;

    int tutorialNum = 0;
    string[] tutorialStrings = {
        "Computers store data as binary numbers.\n\nBinary numbers are made up of 0s and 1s.\n\nThey looks like this:\n\n00101\n10110\n11001",
        "Computers send data back and forth across the world. Sometimes this data gets corrupted.\n\nHow does a computer tell if data is good or bad? How does a computer tell if data is corrupted?\n\nOne method is a parity bit.",
        "A parity bit is a 0 or 1 added to the end of a binary number. For example, using even parity, a 0 or 1 is added to the end of a binary number so that ALL of the digits add up to an EVEN number.\n\n0001 becomes 00011\n0011 becomes 00110",
        "When a computer receives data, it checks each binary number. If the digits add up to an even number, the data is likely still good. If the digits add up to an odd number, the data is bad.\n\n00011 is GOOD because...\n0+0+1+1=2 which is EVEN\n\n0010 is BAD because...\n0+0+1+0=1 which is ODD",
        "Now you try! A parity bit has been added to our binary numbers. The sum of the digits should be an even number. Act fast and decide if the information is good or bad.\n\nReady?"
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

    // public void GenerateNumber()
    // {
    //     string binaryNum = "";
    //     int binarySum = 0;
    //     for (int x = 0; x < numLength; x++)
    //     {
    //         int val = Random.Range(0, 2);
    //         if (val == 0)
    //         {
    //             binaryNum = binaryNum + "0";
    //         }
    //         else
    //         {
    //             binaryNum = binaryNum + "1";
    //             binarySum++;
    //         }
    //     }
    //     currentIsGood = binarySum % 2 == 0;
    // }
    public void GenerateNumber()
    {

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
        audioManager.PlaySelectSound();
    }

    // public void SelectGood()
    // {
    //     if (currentIsGood)
    //     {
    //         CorrectAnswer();
    //     }
    //     else
    //     {
    //         HUDGameOverText.text = "WRONG!";
    //         GameOver();
    //     }
    //     HUDScore.text = currentScore.ToString();
    // }
    // public void SelectBad()
    // {
    //     if (!currentIsGood)
    //     {
    //         CorrectAnswer();
    //     }
    //     else
    //     {
    //         HUDGameOverText.text = "WRONG!";
    //         GameOver();
    //     }
    //     HUDScore.text = currentScore.ToString();
    // }

    public void CorrectAnswer()
    {
        audioManager.PlayCorrectSound();
        currentScore++;
        HUDCorrect.transform.localScale = new Vector3(.1f, .1f, .1f);
        HUDCorrect.SetActive(true);
        HUDCorrect.GetComponent<GrowAndShrink>().StartEffect();
        correctTimer = correctTimerMax;
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
}
