using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneManagerDDR : MonoBehaviour
{
    AudioManager audioManager;

    enum ChunkType {
        ASKOne,
        ASKZero,
        FSKOne,
        FSKZero,
    }

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
    GameObject HUDGameOver;
    [SerializeField]
    TextMeshProUGUI HUDGameOverText;
    [SerializeField]
    GameObject HUDReplay;

    [SerializeField]
    GameObject GraphContainer;
    [SerializeField]
    GameObject FSKChunkPrefab;
    [SerializeField]
    GameObject ASKChunkPrefab;
    [SerializeField]
    Sprite FSKOneSprite;
    [SerializeField]
    Sprite FSKZeroSprite;

    [SerializeField]
    Sprite ASKOneEndSprite;
    [SerializeField]
    Sprite ASKZeroEndSprite;
    [SerializeField]
    Sprite ASKZeroZeroSprite;
    [SerializeField]
    Sprite ASKZeroOneSprite;
    [SerializeField]
    Sprite ASKOneOneSprite;
    [SerializeField]
    Sprite ASKOneZeroSprite;

    bool isPlaying = false;
    int currentScore = 0;

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

        GenerateGraphChunk();

        HUDGame.GetComponent<MoveNormal>().MoveDown();
        isPlaying = true;
    }

    void GenerateGraphChunk()
    {
        bool isASK = true;

        if (isASK)
        {
            int binaryVal = Random.Range(0, 2);
            ChunkType chunkType = binaryVal == 0 ? ChunkType.ASKZero : ChunkType.ASKOne;
            GameObject go = Instantiate(ASKChunkPrefab, new Vector3(500f, 0, 0), Quaternion.identity, GraphContainer.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.GetComponent<GraphChunk>().GraphImage1.sprite = binaryVal == 0 ? ASKZeroZeroSprite : ASKOneOneSprite;
            go.GetComponent<GraphChunk>().GraphImage2.sprite = binaryVal == 0 ? ASKZeroEndSprite : ASKOneEndSprite;
            string debugstring = binaryVal == 0 ? "ASK O" : "ASK 1";
            Debug.Log(debugstring);


            int binaryVal2 = Random.Range(0, 2);
            ChunkType chunkType2 = binaryVal2 == 0 ? ChunkType.ASKZero : ChunkType.ASKOne;
            GameObject go2 = Instantiate(ASKChunkPrefab, new Vector3(500f, 0, 0), Quaternion.identity, GraphContainer.transform);
            go2.transform.localPosition = new Vector3(300f, 0, 0);
            go2.GetComponent<GraphChunk>().GraphImage1.sprite = binaryVal2 == 0
                ? binaryVal == 0 ? ASKZeroZeroSprite : ASKOneZeroSprite
                : binaryVal == 0 ? ASKZeroOneSprite : ASKOneOneSprite;
            go2.GetComponent<GraphChunk>().GraphImage2.sprite = binaryVal2 == 0 ? ASKZeroEndSprite : ASKOneEndSprite;
            debugstring = binaryVal2 == 0 ? "ASK O" : "ASK 1";
            Debug.Log(debugstring);
        }
        else
        {
            int binaryVal = Random.Range(0, 2);
            ChunkType chunkType = binaryVal == 0 ? ChunkType.ASKZero : ChunkType.ASKOne;
            GameObject go = Instantiate(FSKChunkPrefab, new Vector3(500f, 0, 0), Quaternion.identity, GraphContainer.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.GetComponent<GraphChunk>().GraphImage1.sprite = binaryVal == 0 ? FSKZeroSprite : FSKOneSprite;
            string debugstring = binaryVal == 0 ? "FSK O" : "FSK 1";
            Debug.Log(debugstring);
        }
    }

    public void GameOver()
    {
        audioManager.PlayWrongSound();
        isPlaying = false;
        HUDGameOver.GetComponent<MoveNormal>().MoveDown();
        HUDReplay.GetComponent<MoveNormal>().MoveUp();
    }

    public void SelectHome()
    {
        audioManager.PlaySelectSound();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
        HUDGame.GetComponent<MoveNormal>().MoveUp();
    }
}
