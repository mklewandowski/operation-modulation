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
    GameObject HUDTutorial2;
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
    GameObject HUDLevelCompleteMessage;
    [SerializeField]
    GameObject HUDLevelComplete;
    [SerializeField]
    TextMeshProUGUI HUDLevelCompletePercent;
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
    bool levelComplete = false;
    int currentLevel = 0;
    int currentHighlightChar = 0;
    List<int> userChars = new List<int>();
    float speed = 350f;
    float startSpeed = 350f;
    float levelCompleteTimer = 0f;
    float levelCompleteTimerMax = 4f;
    float visibleHighlightPos = 520f;
    float visibleHighlightPosMin = -650f;
    bool waitToHighlight = false;

    List<GameObject> graphChunks = new List<GameObject>();
    int prevBinaryVal = 0;

    // int currentHighlightValue = -1;
    // bool haveGuessedThisHighlight = false;

    [SerializeField]
    GameObject HUDLevelStart;
    [SerializeField]
    Image HUDLevelStartImage;
    [SerializeField]
    TextMeshProUGUI HUDLevelStartTitleText;
    [SerializeField]
    TextMeshProUGUI HUDLevelStartText;
    [SerializeField]
    TextMeshProUGUI HUDLevelButtonText;
    [SerializeField]
    Sprite ASKintroSprite;
    [SerializeField]
    Sprite FSKintroSprite;
    [SerializeField]
    GameObject HUDbinaryImagePanel;
    [SerializeField]
    GameObject HUDbinaryImagePanelLevelComplete;
    [SerializeField]
    GameObject HUDbinaryImagePanelLevelCompleteUser;
    int MaxSquares = 64;
    BinaryImage[] binaryImages;
    [SerializeField]
    GameObject BinaryImageSquarePrefab;
    GameObject[] BinaryImageSquares;
    GameObject[] BinaryImageSquaresLevelComplete;
    GameObject[] BinaryImageSquaresLevelCompleteUser;
    [SerializeField]
    TextMeshProUGUI HUDUserText;

    [SerializeField]
    ParticleSystem EndParticle;
    float endParticleTimer = 0;
    float endParticleTimerMax = 1.5f;

    [SerializeField]
    Image AudioImage;
    [SerializeField]
    Sprite AudioOnSprite;
    [SerializeField]
    Sprite AudioOffSprite;

    [SerializeField]
    TextMeshProUGUI LanguageText;

    // Start is called before the first frame update
    void Start()
    {
        Globals.LoadUserSettings();
        SelectLanguage(Globals.CurrentLanguage);

        audioManager = this.GetComponent<AudioManager>();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();

        InitBinaryImages();
        InitIntroAndLevelCompletePanels();
    }

    void InitBinaryImages()
    {
        binaryImages = new BinaryImage[6];
        for (int x = 0; x < binaryImages.Length; x++)
        {
            binaryImages[x] = new BinaryImage();
        }
        binaryImages[0].Title = "smile emoji";
        binaryImages[0].TitleSP = "smile emoji";
        binaryImages[0].isASK = true;
        binaryImages[0].Bits = new int[64] {0,0,1,1,1,1,0,0,0,1,0,1,1,0,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,0,1,1,1,0,0,0,0,1,1,0,1,1,1,1,1,1,0,0,0,1,1,1,1,0,0};
        binaryImages[1].Title = "heart";
        binaryImages[1].TitleSP = "heart";
        binaryImages[1].isASK = false;
        binaryImages[1].Bits = new int[64] {0,1,1,0,0,1,1,0,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,1,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,0,1,0,0,1,0,0,0,0,0,1,1,0,0,0};
        binaryImages[2].Title = "cat";
        binaryImages[2].TitleSP = "cat";
        binaryImages[2].isASK = true;
        binaryImages[2].Bits = new int[64] {1,0,0,1,0,0,0,1,1,1,1,1,0,0,1,0,1,1,1,1,0,0,1,0,0,1,1,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0};
        binaryImages[3].Title = "robot";
        binaryImages[3].TitleSP = "robot";
        binaryImages[3].isASK = false;
        binaryImages[3].Bits = new int[64] {0,1,0,0,0,0,1,0,0,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,0,1,0,1,1,0,1,0,1,1,1,1,1,1,1,1,0,1,1,0,0,1,1,0,0,0,1,0,0,1,0,0,0,0,1,1,1,1,0,0};
        binaryImages[4].Title = "space ship";
        binaryImages[4].TitleSP = "space ship";
        binaryImages[4].isASK = false;
        binaryImages[4].Bits = new int[64] {0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,0,1,1,0,0,1};
        binaryImages[5].Title = "ghost";
        binaryImages[5].TitleSP = "ghost";
        binaryImages[5].isASK = false;
        binaryImages[5].Bits = new int[64] {0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,0};
    }

    void InitIntroAndLevelCompletePanels()
    {
        BinaryImageSquares = new GameObject[MaxSquares];

        for (int x = 0; x < MaxSquares; x++)
        {
            BinaryImageSquares[x] = Instantiate(BinaryImageSquarePrefab, HUDbinaryImagePanel.transform);
        }

        BinaryImageSquaresLevelComplete = new GameObject[MaxSquares];

        for (int x = 0; x < MaxSquares; x++)
        {
            BinaryImageSquaresLevelComplete[x] = Instantiate(BinaryImageSquarePrefab, HUDbinaryImagePanelLevelComplete.transform);
        }


        BinaryImageSquaresLevelCompleteUser = new GameObject[MaxSquares];

        for (int x = 0; x < MaxSquares; x++)
        {
            BinaryImageSquaresLevelCompleteUser[x] = Instantiate(BinaryImageSquarePrefab, HUDbinaryImagePanelLevelCompleteUser.transform);
        }
    }

    void UpdateIntroPanel()
    {
        HUDLevelStartImage.sprite = binaryImages[currentLevel].isASK ? ASKintroSprite : FSKintroSprite;
        string introText = "A radio wave carrying the digital information of a " + binaryImages[currentLevel].Title + " is coming your way!\n\n";
        introText = "";
        if (Globals.CurrentLanguage == Globals.Language.English)
        {
            introText = "A radio wave carrying the digital information of a " + binaryImages[currentLevel].Title + " is coming your way!\n\n";
            introText = introText + "Now YOU are the computer and must decode the digitally modulated carrier signal correctly!\n\n";
            if (binaryImages[currentLevel].isASK)
            {
                introText = introText + "As the radio wave using amplitude key shifting comes in, decide if the waveform shows a 0 or a 1 based on its amplitude. White squares are represented as a 1. Black squares are represented as a 0. At the end we will compare your decoded image to the one sent!";
            }
            else
            {
                introText = introText + "As the radio wave using frequency key shifting comes in, decide if the waveform shows a 0 or a 1 based on its frequency. White squares are represented as a 1. Black squares are represented as a 0. At the end we will compare your decoded image to the one sent!";
            } 
        }
        else
        {
            introText = "A radio wave carrying the digital information of a " + binaryImages[currentLevel].TitleSP + " is coming your way!\n\n";
            introText = introText + "Now YOU are the computer and must decode the digitally modulated carrier signal correctly!\n\n";
            if (binaryImages[currentLevel].isASK)
            {
                introText = introText + "As the radio wave using amplitude key shifting comes in, decide if the waveform shows a 0 or a 1 based on its amplitude. White squares are represented as a 1. Black squares are represented as a 0. At the end we will compare your decoded image to the one sent!";
            }
            else
            {
                introText = introText + "As the radio wave using frequency key shifting comes in, decide if the waveform shows a 0 or a 1 based on its frequency. White squares are represented as a 1. Black squares are represented as a 0. At the end we will compare your decoded image to the one sent!";
            } 
        }
        HUDLevelStartText.text = introText;
        if (Globals.CurrentLanguage == Globals.Language.English)
            HUDLevelStartTitleText.text = "LEVEL " + (currentLevel + 1).ToString();
        else
            HUDLevelStartTitleText.text = "LEVEL " + (currentLevel + 1).ToString();

        for (int x = 0; x < MaxSquares; x++)
        {
            int squareVal = binaryImages[currentLevel].Bits[x];
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

    void UpdateLevelCompletePanels()
    {
        for (int x = 0; x < MaxSquares; x++)
        {
            int squareVal = binaryImages[currentLevel].Bits[x];
            if (squareVal == 1)
            {
                BinaryImageSquaresLevelComplete[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.On;
                BinaryImageSquaresLevelComplete[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.white;
            }
            else
            {
                BinaryImageSquaresLevelComplete[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.Off;
                BinaryImageSquaresLevelComplete[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.black;
            }
        }

        int numCorrect = 0;
        for (int x = 0; x < MaxSquares; x++)
        {
            int squareVal = userChars[x];
            int correctSquareVal = binaryImages[currentLevel].Bits[x];
            if (squareVal == correctSquareVal)
                numCorrect++;
            if (squareVal == 1)
            {
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().QuestionText.SetActive(false);
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.On;
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.white;
            }
            else if (squareVal == 0)
            {
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().QuestionText.SetActive(false);
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.Off;
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.black;
            }
            else
            {
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().QuestionText.SetActive(true);
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().On = Globals.BinaryImageSquareStates.Unknown;
                BinaryImageSquaresLevelCompleteUser[x].GetComponent<BinaryImageSquare>().SquareImage.color = Color.black;
            }
        }
        if (Globals.CurrentLanguage == Globals.Language.English)
        {
            HUDLevelCompletePercent.text = ((int)((float)numCorrect / 64f * 100f)).ToString() + "% accurate!";
            HUDLevelButtonText.text = (currentLevel < 4 ? "NEXT LEVEL" : "HOME");
        }
        else
        {
            HUDLevelCompletePercent.text = ((int)((float)numCorrect / 64f * 100f)).ToString() + "% accurate!";
            HUDLevelButtonText.text = (currentLevel < 4 ? "NEXT LEVEL" : "HOME"); 
        }
    }

    void CreateScrollingGraph()
    {
        int currentBinaryVal = binaryImages[currentLevel].Bits[0];
        prevBinaryVal = currentBinaryVal;
        for (int x = 0; x < MaxSquares; x++)
        {
            currentBinaryVal = binaryImages[currentLevel].Bits[x];
            GenerateGraphChunk(currentBinaryVal, prevBinaryVal, 750f + 300f * x, binaryImages[currentLevel].isASK);
            prevBinaryVal = currentBinaryVal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            foreach (GameObject go in graphChunks)
            {
                go.transform.localPosition = new Vector3(go.transform.localPosition.x - speed * Time.deltaTime, go.transform.localPosition.y, go.transform.localPosition.z);
            }

            if (waitToHighlight)
            {
                if (graphChunks[currentHighlightChar].transform.localPosition.x <= visibleHighlightPos)
                {
                    UpdateCurrentHighlight();
                }
            }

            if (!levelComplete && graphChunks[currentHighlightChar].transform.localPosition.x <= visibleHighlightPosMin)
            {
                userChars.Add(-1);
                HUDUserText.text = HUDUserText.text + "?";
                audioManager.PlayMissSound();
                IncrementGraphHighlight();
            }

            if (Input.GetKeyDown("0"))
                GuessZero();
            else if (Input.GetKeyDown("1"))
                GuessOne();

            if (levelCompleteTimer > 0 && levelComplete)
            {
                levelCompleteTimer -= Time.deltaTime;
                if (levelCompleteTimer <= 0)
                {
                    ShowLevelComplete();
                }
            }
        }
        if (endParticleTimer > 0)
        {
            endParticleTimer -= Time.deltaTime;
            if (endParticleTimer <= 0)
            {
                EndParticle.Stop();
            }
        }
    }

    void UpdateCurrentHighlight()
    {
        if (currentHighlightChar > 0)
        {
            graphChunks[currentHighlightChar - 1].GetComponent<GraphChunk>().BackImage.SetActive(false);
        }
        if (currentHighlightChar < MaxSquares)
        {
            if (graphChunks[currentHighlightChar].transform.localPosition.x > visibleHighlightPos)
            {
                waitToHighlight = true;
            }
            else
            {
                graphChunks[currentHighlightChar].GetComponent<GraphChunk>().BackImage.transform.localScale = new Vector3(.1f, .1f, .1f);
                graphChunks[currentHighlightChar].GetComponent<GraphChunk>().BackImage.SetActive(true);
                graphChunks[currentHighlightChar].GetComponent<GraphChunk>().BackImage.GetComponent<GrowAndShrink>().StartEffect();
                waitToHighlight = false;
            }
        }
    }

    public void StartGameIntro()
    {
        audioManager.PlaySelectSound();
        currentLevel = 0;
        UpdateIntroPanel();
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();
        HUDLevelStart.GetComponent<MoveNormal>().MoveUp();
    }

    public void StartGame()
    {
        audioManager.PlaySelectSound();
        HUDLevelStart.GetComponent<MoveNormal>().MoveDown();

        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
        HUDTutorial2.GetComponent<MoveNormal>().MoveDown();
        HUDLevelComplete.GetComponent<MoveNormal>().MoveUp();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDReplay.GetComponent<MoveNormal>().MoveDown();

        speed = startSpeed + (currentLevel * 15f);
        currentHighlightChar = 0;
        HUDUserText.text = "";
        userChars.Clear();
        ClearGraphChunks();
        CreateScrollingGraph();
        UpdateCurrentHighlight();

        HUDGame.GetComponent<MoveNormal>().MoveDown();
        levelComplete = false;
        isPlaying = true;
    }

    public void ShowLevelComplete()
    {
        HUDLevelCompleteMessage.GetComponent<MoveNormal>().MoveUp();
        HUDLevelComplete.GetComponent<MoveNormal>().MoveDown();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDReplay.GetComponent<MoveNormal>().MoveDown();
        HUDGame.GetComponent<MoveNormal>().MoveUp();
        isPlaying = false;
    }

    public void ShowNextLevel()
    {
        if (currentLevel >= 5)
        {
            HUDLevelComplete.GetComponent<MoveNormal>().MoveUp();
            SelectHome();
        }
        else
        {
            audioManager.PlaySelectSound();
            HUDLevelComplete.GetComponent<MoveNormal>().MoveUp();
            currentLevel++;
            UpdateIntroPanel();
            HUDLevelStart.GetComponent<MoveNormal>().MoveUp();
        }
    }

    void ClearGraphChunks()
    {
        for (int x = 0; x < graphChunks.Count; x++)
        {
            Destroy(graphChunks[x]);
        }
        graphChunks.Clear();
    }

    void GenerateGraphChunk(int currVal, int prevVal, float xPos, bool isASK)
    {
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
            graphChunks.Add(go);
        }
        else
        {
            ChunkType chunkType = currVal == 0 ? ChunkType.ASKZero : ChunkType.ASKOne;
            GameObject go = Instantiate(FSKChunkPrefab, new Vector3(xPos, 0, 0), Quaternion.identity, GraphContainer.transform);
            go.transform.localPosition = new Vector3(xPos, 0, 0);
            go.GetComponent<GraphChunk>().GraphImage1.sprite = currVal == 0 ? FSKZeroSprite : FSKOneSprite;
            go.GetComponent<GraphChunk>().BinaryVal = currVal;
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

    public void LevelComplete()
    {
        levelComplete = true;
        HUDLevelCompleteMessage.GetComponent<MoveNormal>().MoveDown();
        levelCompleteTimer = levelCompleteTimerMax;
        UpdateLevelCompletePanels();
        EndParticle.Play();
        endParticleTimer = endParticleTimerMax;
        audioManager.PlaySuccessSound();
    }

    public void SelectAbout()
    {
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();
        HUDTutorial.GetComponent<MoveNormal>().MoveUp();
        audioManager.PlaySelectSound();
    }
    public void SelectAbout2()
    {
        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
        HUDTutorial2.GetComponent<MoveNormal>().MoveUp();
        audioManager.PlaySelectSound();
    }

    public void SelectBack()
    {
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
        HUDTutorial2.GetComponent<MoveNormal>().MoveDown();
        audioManager.PlaySelectSound();
    }

    public void GuessOne()
    {
        if (levelComplete)
            return;
        if (waitToHighlight)
        {
            audioManager.PlayInvalidSound();
            return;
        }
        audioManager.PlaySelectSound();
        userChars.Add(1);
        HUDUserText.text = HUDUserText.text + "1";
        IncrementGraphHighlight();
    }

    public void GuessZero()
    {
        if (levelComplete)
            return;
        if (waitToHighlight)
        {
            audioManager.PlayInvalidSound();
            return;
        }
        audioManager.PlaySelectSound();
        userChars.Add(0);
        HUDUserText.text = HUDUserText.text + "0";
        IncrementGraphHighlight();
    }

    void IncrementGraphHighlight()
    {
        currentHighlightChar++;
        if (currentHighlightChar % 10 == 0)
            speed = speed + 30f;
        UpdateCurrentHighlight();
        if (currentHighlightChar >= MaxSquares)
            LevelComplete();
    }

    public void SelectHome()
    {
        isPlaying = false;
        audioManager.PlaySelectSound();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
        HUDGame.GetComponent<MoveNormal>().MoveUp();
        HUDLevelCompleteMessage.GetComponent<MoveNormal>().MoveUp();
    }

    public void ToggleAudio()
    {
        Globals.AudioOn = !Globals.AudioOn;
        if (Globals.AudioOn)
        {
            audioManager.StartMusic();
            AudioImage.sprite = AudioOnSprite;
        }
        else
        {
            audioManager.StopMusic();
            AudioImage.sprite = AudioOffSprite;
        }
    }


    public void ToggleLanguage()
    {
        audioManager.PlaySelectSound();
        if (Globals.CurrentLanguage == Globals.Language.English)
            SelectLanguage(Globals.Language.Spanish);
        else
            SelectLanguage(Globals.Language.English);
    }

    public void SelectLanguage(Globals.Language newLang)
    {
        Globals.CurrentLanguage = newLang;
        if (Globals.CurrentLanguage == Globals.Language.English)
            LanguageText.text = "ESPAÑOL";
        else
            LanguageText.text = "ENGLISH";

        TranslateText[] textObjects = GameObject.FindObjectsOfType<TranslateText>(true);
        for (int i = 0; i < textObjects.Length; i++)
        {
            textObjects[i].UpdateText();
        }

        TranslateImage[] imageObjects = GameObject.FindObjectsOfType<TranslateImage>(true);
        for (int i = 0; i < imageObjects.Length; i++)
        {
            imageObjects[i].UpdateImage();
        }

        Globals.SaveIntToPlayerPrefs(Globals.LanguageStorageKey, (int)newLang);
    }

}
