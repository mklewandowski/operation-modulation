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
    int currentLevel = 0;
    int currentHighlightChar = 0;
    List<int> userChars = new List<int>();
    float speed = 250f;

    List<GameObject> graphChunks = new List<GameObject>();
    int prevBinaryVal = 0;

    // int currentHighlightValue = -1;
    // bool haveGuessedThisHighlight = false;

    int tutorialNum = 0;
    string[] tutorialStrings = {
        "Modulation is used to transmit binary numbers (0s and 1s) over radio waves.",
        "The shape of the graph determines if a 0 or a 1 is being sent.",
        "Now you try! Amplitude Shift Keying has been used to send binary numbers over a radio wave. Try to decode each binary number.\n\nReady?"
    };

    [SerializeField]
    GameObject HUDLevelStart;
    [SerializeField]
    Image HUDLevelStartImage;
    [SerializeField]
    TextMeshProUGUI HUDLevelStartText;
    [SerializeField]
    Sprite ASKintroSprite;
    [SerializeField]
    Sprite FSKintroSprite;
    [SerializeField]
    GameObject HUDbinaryImagePanel;
    int MaxSquares = 100;
    BinaryImage[] binaryImages;
    [SerializeField]
    GameObject BinaryImageSquarePrefab;
    GameObject[] BinaryImageSquares;
    [SerializeField]
    TextMeshProUGUI HUDUserText;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = this.GetComponent<AudioManager>();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();

        InitBinaryImages();
        InitIntroPanel();
    }

    void InitBinaryImages()
    {
        binaryImages = new BinaryImage[8];
        for (int x = 0; x < binaryImages.Length; x++)
        {
            binaryImages[x] = new BinaryImage();
        }
        binaryImages[0].Title = "smiley emoji";
        binaryImages[0].isASK = true;
        binaryImages[0].Bits = new int[100] {0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,0,1,1,1,0,1,1,0,1,1,0,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,1,1,1,0,1,1,1,1,0,1,1,0,1,1,0,0,0,0,1,1,0,0,0,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,0,0,0};
        binaryImages[1].Title = "heart";
        binaryImages[1].isASK = false;
        binaryImages[1].Bits = new int[100] {0,0,1,1,0,0,1,1,0,0,0,1,1,1,1,1,1,1,1,0,1,1,0,0,1,1,0,0,1,1,1,1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1,1,0,1,1,0,0,0,0,1,1,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0};
        binaryImages[2].Title = "alien";
        binaryImages[2].isASK = true;
        binaryImages[2].Bits = new int[100] {0,0,1,0,0,0,0,1,0,0,1,0,0,1,0,0,0,1,0,1,1,0,1,1,1,1,1,1,0,1,1,1,0,0,0,0,0,0,1,1,1,1,0,1,0,0,1,0,1,1,0,1,0,1,0,0,1,0,1,0,0,1,0,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,1,0,0,1,0,1,0,0,1,0,1,0,1,1,0,0,0,0,0,0,1,1};
        binaryImages[3].Title = "cat";
        binaryImages[3].isASK = false;
        binaryImages[3].Bits = new int[100] {0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,0,0,0,1,0,1,1,1,1,1,0,0,0,1,0,1,0,1,0,1,0,0,0,0,1,1,1,1,1,1,0,0,0,0,1,0,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,0,0,1,0,1,0,0,0,0,1,0,0,1,0,1,0,0,0,0,1,0};
        binaryImages[4].Title = "robot";
        binaryImages[4].isASK = true;
        binaryImages[4].Bits = new int[100] {0,1,0,0,0,0,0,0,1,0,0,1,1,0,0,0,0,1,1,0,0,1,1,1,1,1,1,1,1,0,1,1,0,0,1,1,0,0,1,1,0,1,0,0,1,1,0,0,1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,0,0,0,1,1,0,0,1,1,0,0,0,0,1,1,0,0,0,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,0,0,0};
        binaryImages[5].Title = "space ship";
        binaryImages[5].isASK = false;
        binaryImages[5].Bits = new int[100] {0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,0,0,1};
        binaryImages[6].Title = "ghost";
        binaryImages[6].isASK = true;
        binaryImages[6].Bits = new int[100] {0,0,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,0,0,1,1,0,0,1,0,0,1,1,0,1,1,0,1,1,0,1,1,1,0,1,1,0,0,1,0,0,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0};
    }

    void InitIntroPanel()
    {
        BinaryImageSquares = new GameObject[MaxSquares];

        for (int x = 0; x < MaxSquares; x++)
        {
            BinaryImageSquares[x] = Instantiate(BinaryImageSquarePrefab, HUDbinaryImagePanel.transform);
        }
    }

    void UpdateIntroPanel(int index)
    {
        HUDLevelStartImage.sprite = binaryImages[index].isASK ? ASKintroSprite : FSKintroSprite;
        string introText = "A " + binaryImages[index].Title + " image is being sent using radio waves and you must decode it!\n\n";
        introText = introText + "First we convert the squares to binary numbers. White squares are represented as a 1. Black squares are represented as a 0.\n\n";
        introText = introText + "Then we turn the binary numbers into a radio signal that uses " + (binaryImages[index].isASK ? "amplitude shift keying. " : "frequency shift keying. ");
        introText = introText + (binaryImages[index].isASK ? "0's are represented with a low amplitude signal, 1's are represented with a high amplitude signal." : "0's are represented with a low frequency signal, 1's are represented with a high frequency signal.");
        HUDLevelStartText.text = introText;

        for (int x = 0; x < MaxSquares; x++)
        {
            int squareVal = binaryImages[index].Bits[x];
            if (squareVal == 1)
            {
                BinaryImageSquares[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.On;
                BinaryImageSquares[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.white;
            }
            else
            {
                BinaryImageSquares[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.Off;
                BinaryImageSquares[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.black;
            }
        }
    }

    void CreateScrollingGraph()
    {
        int currentBinaryVal = binaryImages[currentLevel].Bits[0];
        prevBinaryVal = currentBinaryVal;
        for (int x = 0; x < MaxSquares; x++)
        {
            currentBinaryVal = binaryImages[currentLevel].Bits[x];
            GenerateGraphChunk(currentBinaryVal, prevBinaryVal, 700f + 300f * x);
            prevBinaryVal = currentBinaryVal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            // bool removeFirst = false;
            // bool addNew = false;
            // float xPos = 0;
            // foreach (GameObject go in graphChunks)
            // {
            //     GraphChunk graphChunk = go.GetComponent<GraphChunk>();
            //     go.transform.localPosition = new Vector3(go.transform.localPosition.x - speed * Time.deltaTime, go.transform.localPosition.y, go.transform.localPosition.z);
            //     if (go.transform.localPosition.x < -362f)
            //     {
            //         removeFirst = true;
            //     }
            //     else if (go.transform.localPosition.x < 500f && !graphChunk.HasTriggeredNext)
            //     {
            //         go.GetComponent<GraphChunk>().HasTriggeredNext = true;
            //         xPos = go.transform.localPosition.x + 300f;
            //         addNew = true;
            //     }
            //     else if (go.transform.localPosition.x <= -62f && !graphChunk.IsActive) // set chunk active
            //     {
            //         graphChunk.SetColor(new Color (55f/255f, 173f/255f, 168f/255f));
            //         graphChunk.IsActive = true;
            //         currentHighlightValue = graphChunk.BinaryVal;
            //         haveGuessedThisHighlight = false;
            //     }
            //     // else if (go.transform.localPosition.x < -362f && graphChunk.IsActive)
            //     // {
            //     //     currentHighlightValue = -1;
            //     //     graphChunk.SetColor(new Color (108f/255f, 92f/255f, 124f/255f));
            //     //     graphChunk.IsActive = false;
            //     //     graphChunk.BackImage.SetActive(false);
            //     // }
            // }
            // if (removeFirst)
            // {
            //     Destroy(graphChunks[0]);
            //     graphChunks.RemoveAt(0);
            // }
            // if (addNew)
            // {
            //     currentChar++;
            //     if (currentChar < binaryImages[currentLevel].Bits.Length)
            //     {
            //         int currentBinaryVal = currentBinaryVal = binaryImages[currentLevel].Bits[currentChar];
            //         GenerateGraphChunk(currentBinaryVal, prevBinaryVal, xPos);
            //         prevBinaryVal = currentBinaryVal;
            //     }
            // }

            foreach (GameObject go in graphChunks)
            {
                go.transform.localPosition = new Vector3(go.transform.localPosition.x - speed * Time.deltaTime, go.transform.localPosition.y, go.transform.localPosition.z);
            }

            if (Input.GetKeyDown("0"))
                GuessZero();
            else if (Input.GetKeyDown("1"))
                GuessOne();
        }
    }

    void UpdateCurrentHighlight()
    {
        if (currentHighlightChar > 0)
        {
            graphChunks[currentHighlightChar - 1].GetComponent<GraphChunk>().BackImage.SetActive(false);
        }
        graphChunks[currentHighlightChar].GetComponent<GraphChunk>().BackImage.SetActive(true);
                    //     else if (go.transform.localPosition.x <= -62f && !graphChunk.IsActive) // set chunk active
            //     {
            //         graphChunk.SetColor(new Color (55f/255f, 173f/255f, 168f/255f));
            //         graphChunk.IsActive = true;
            //         currentHighlightValue = graphChunk.BinaryVal;
            //         haveGuessedThisHighlight = false;
            //     }
            //     // else if (go.transform.localPosition.x < -362f && graphChunk.IsActive)
            //     // {
            //     //     currentHighlightValue = -1;
            //     //     graphChunk.SetColor(new Color (108f/255f, 92f/255f, 124f/255f));
            //     //     graphChunk.IsActive = false;
            //     //     graphChunk.BackImage.SetActive(false);
            //     // }
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

    public void StartGameIntro()
    {
        audioManager.PlaySelectSound();
        currentLevel = 0;
        UpdateIntroPanel(currentLevel);
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();
        HUDLevelStart.GetComponent<MoveNormal>().MoveUp();
    }

    public void StartGame()
    {
        audioManager.PlaySelectSound();
        HUDLevelStart.GetComponent<MoveNormal>().MoveDown();

        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDReplay.GetComponent<MoveNormal>().MoveDown();

        currentHighlightChar = 0;
        CreateScrollingGraph();
        UpdateCurrentHighlight();

        HUDGame.GetComponent<MoveNormal>().MoveDown();
        isPlaying = true;
    }

    void GenerateGraphChunk(int currVal, int prevVal, float xPos)
    {
        bool isASK = true;

        if (isASK)
        {
            ChunkType chunkType2 = currVal == 0 ? ChunkType.ASKZero : ChunkType.ASKOne;
            GameObject go = Instantiate(ASKChunkPrefab, new Vector3(xPos, 0, 0), Quaternion.identity, GraphContainer.transform);
            go.transform.localPosition = new Vector3(xPos, 0, 0);
            go.GetComponent<GraphChunk>().GraphImage1.sprite = currVal == 0
                ? prevVal == 0 ? ASKZeroZeroSprite : ASKOneZeroSprite
                : prevVal == 0 ? ASKZeroOneSprite : ASKOneOneSprite;
            go.GetComponent<GraphChunk>().GraphImage2.sprite = currVal == 0 ? ASKZeroEndSprite : ASKOneEndSprite;
            go.GetComponent<GraphChunk>().BinaryVal = currVal;
            string debugstring = currVal == 0 ? "ASK O" : "ASK 1";
            Debug.Log(debugstring);
            graphChunks.Add(go);
        }
        else
        {
            ChunkType chunkType = currVal == 0 ? ChunkType.ASKZero : ChunkType.ASKOne;
            GameObject go = Instantiate(FSKChunkPrefab, new Vector3(xPos, 0, 0), Quaternion.identity, GraphContainer.transform);
            go.transform.localPosition = new Vector3(xPos, 0, 0);
            go.GetComponent<GraphChunk>().GraphImage1.sprite = currVal == 0 ? FSKZeroSprite : FSKOneSprite;
            go.GetComponent<GraphChunk>().BinaryVal = currVal;
            string debugstring = currVal == 0 ? "FSK O" : "FSK 1";
            Debug.Log(debugstring);
            graphChunks.Add(go);
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

    public void GuessOne()
    {
        // if (haveGuessedThisHighlight)
        // {
        //     Strike();
        //     return;
        // }
        // else
        // {
        //     if (currentHighlightValue == 1)
        //     {
        //         Correct();
        //     }
        //     else
        //     {
        //         Strike();
        //     }
        // }
        // haveGuessedThisHighlight = true;

        userChars.Add(1);
        currentHighlightChar++;
        HUDUserText.text = HUDUserText.text + "1";
        UpdateCurrentHighlight();
    }

    public void GuessZero()
    {
        // if (haveGuessedThisHighlight)
        // {
        //     Strike();
        //     return;
        // }
        // else
        // {
        //     if (currentHighlightValue == 0)
        //     {
        //         Correct();
        //     }
        //     else
        //     {
        //         Strike();
        //     }
        // }
        userChars.Add(0);
        currentHighlightChar++;
        HUDUserText.text = HUDUserText.text + "0";
        UpdateCurrentHighlight();
    }

    // void Correct()
    // {
    //     Destroy(graphChunks[0]);
    //     graphChunks.RemoveAt(0);
    // }

    // void Strike()
    // {

    // }

}
