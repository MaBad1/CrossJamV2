using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{


    [SerializeField] public List<Material> MaterialList = new List<Material>();
    [SerializeField] public List<Material> MaterialListMedium = new List<Material>();
    [SerializeField] public List<Material> MaterialListHard = new List<Material>();
    [SerializeField] public List<GameObject> AreaList = new List<GameObject>();

    
    [SerializeField] GameObject startBack;
    [SerializeField] GameObject TitleBack;
    [SerializeField] Text score;
    [SerializeField] Text bestScore;
    [SerializeField] Text livesMedium;
    [SerializeField] Text textLivesMedium;
    [SerializeField] Text lifeHard;
    [SerializeField] GameObject shape;
    [SerializeField] GameObject shapeMedium;
    [SerializeField] GameObject shapeHard;
    [SerializeField] GameObject easyB;
    [SerializeField] GameObject mediumB;
    [SerializeField] GameObject hardB;
    [SerializeField] GameObject pointPlus;
    [SerializeField] GameObject pointMoins;
    [SerializeField] GameObject win;
    [SerializeField] GameObject loose;
    [SerializeField] GameObject muteB;
    [SerializeField] GameObject muteBText;
    [SerializeField] GameObject music;

    public float timer;
    private int timerInt;
    public Text textTimer;
    int scoreCurrent = 0;
    int bestScoreCurrent;
    bool easyClick = false;
    bool mediumClick = false;
    bool hardClick = false;
    bool isMuted = false;
    private AudioSource _pointPlus;
    private AudioSource _pointMoins;
    private AudioSource _win;
    private AudioSource _loose;
    private AudioSource _music;
    private TextMeshProUGUI mutebTextMP;

    public enum State
    {
        Begining,
        InGameEasy,
        InGameMedium,
        InGameHard,
        GameOver
    }

    public State gameState = State.Begining;
    // Start is called before the first frame update
    void Start()
    {
        mutebTextMP = muteBText.GetComponent<TextMeshProUGUI>();
        _pointPlus = pointPlus.GetComponent<AudioSource>();
        _pointMoins = pointMoins.GetComponent<AudioSource>();
        _win = win.GetComponent<AudioSource>();
        _loose = loose.GetComponent<AudioSource>();
        _music = music.GetComponent<AudioSource>();
        _music.Play();
        shape.gameObject.SetActive(false);
        shapeMedium.gameObject.SetActive(false);
        shapeHard.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        livesMedium.gameObject.SetActive(false);
        textLivesMedium.gameObject.SetActive(false);
        lifeHard.gameObject.SetActive(false);
        bestScore.gameObject.SetActive(true);
        bestScore.text = "High score :" + PlayerPrefs.GetInt("bestScoreCurrent").ToString();
    }

    // Update is called once per frame
    void Update()
    {

        easyB.GetComponent<Button>().onClick.AddListener(EasyStart);
        mediumB.GetComponent<Button>().onClick.AddListener(MediumStart);
        hardB.GetComponent<Button>().onClick.AddListener(HardStart);
        muteB.GetComponent<Button>().onClick.AddListener(SwitchSound);

        

        if (easyClick == true && gameState == State.Begining)
        {
            StartGameEasy();
            easyClick = false;
        }

        if (mediumClick == true && gameState == State.Begining)
        {
            StartGameMedium();
            mediumClick = false;
        }

        if (hardClick == true && gameState == State.Begining)
        {
            StartGameHard();
            hardClick = false;
        }

        if (gameState == State.GameOver)
        {
            bestScore.gameObject.SetActive(true);
            score.gameObject.SetActive(false);
            bestScore.text = "High score :" + PlayerPrefs.GetInt("bestScoreCurrent").ToString();
        }
    }

    void FixedUpdate()
    {
        if(gameState == State.InGameEasy)
        {
            
            score.text = scoreCurrent.ToString();
            if (timer > 5)
            {
                timer -= Time.fixedDeltaTime;
                timerInt = (int)timer;
                textTimer.text = timerInt.ToString();
            }
            else if ( timer <= 5 && timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                timerInt = (int)timer;
                textTimer.text = timerInt.ToString();
                textTimer.GetComponent<Text>().color = Color.red;
                textTimer.fontSize = 155;
            }
            else
            {
                GameOver();
                if (isMuted == false)
                {
                    _win.Play();
                }
            }
        }
        if (gameState == State.InGameMedium)
        {

            livesMedium.text = FindObjectOfType<ShapeMedium>().lives.ToString();
            score.text = scoreCurrent.ToString();
            if (timer > 5 && FindObjectOfType<ShapeMedium>().lives > 0)
            {
                timer -= Time.fixedDeltaTime;
                timerInt = (int)timer;
                textTimer.text = timerInt.ToString();
            }
            else if (timer <= 5 && timer > 0 && FindObjectOfType<ShapeMedium>().lives > 0)
            {
                timer -= Time.fixedDeltaTime;
                timerInt = (int)timer;
                textTimer.text = timerInt.ToString();
                textTimer.GetComponent<Text>().color = Color.red;
                textTimer.fontSize = 155;
            }
            else if (FindObjectOfType<ShapeMedium>().lives > 0 && timer <= 0)
            {
                GameOver();
                if (isMuted == false)
                {
                    _win.Play();
                }
            }
            else
            {
                GameOver();
                if (isMuted == false)
                {
                    _loose.Play();
                }
            }
        }
        if (gameState == State.InGameHard)
        {

            score.text = scoreCurrent.ToString();
            if (timer > 5 && FindObjectOfType<ShapeHard>().life > 0)
            {
                timer -= Time.fixedDeltaTime;
                timerInt = (int)timer;
                textTimer.text = timerInt.ToString();
            }
            else if (timer <= 5 && timer > 0 && FindObjectOfType<ShapeHard>().life > 0)
            {
                timer -= Time.fixedDeltaTime;
                timerInt = (int)timer;
                textTimer.text = timerInt.ToString();
                textTimer.GetComponent<Text>().color = Color.red;
                textTimer.fontSize = 155;
            }
            else if (FindObjectOfType<ShapeHard>().life > 0 && timer <= 0)
            {
                GameOver();
                if (isMuted == false)
                {
                    _win.Play();
                }
            }
            else
            {
                GameOver();
                if (isMuted == false)
                {
                    _loose.Play();
                }
            }
        }

    }

    public void EasyStart()
    {
        easyClick = true;
    }

    public void MediumStart()
    {
        mediumClick = true;
    }

    public void HardStart()
    {
        hardClick = true;
    }
    void StartGameMedium()
    {
        gameState = State.InGameMedium;
        shapeMedium.gameObject.SetActive(true);

        shapeMedium.gameObject.transform.position = new Vector3(0, 1, 0);

        BroadcastMessage("OnStartGame", SendMessageOptions.DontRequireReceiver);

        startBack.gameObject.SetActive(false);
        TitleBack.gameObject.SetActive(false);
        score.gameObject.SetActive(true);
        livesMedium.gameObject.SetActive(true);
        textLivesMedium.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(false);
        easyB.gameObject.SetActive(false);
        mediumB.gameObject.SetActive(false);
        hardB.gameObject.SetActive(false);
        muteB.gameObject.SetActive(false);

        RandomColorMedium();

    }

    void StartGameHard()
    {
        gameState = State.InGameHard;
        shapeHard.gameObject.SetActive(true);

        shapeHard.gameObject.transform.position = new Vector3(0, 1, 0);

        BroadcastMessage("OnStartGame", SendMessageOptions.DontRequireReceiver);

        startBack.gameObject.SetActive(false);
        TitleBack.gameObject.SetActive(false);
        score.gameObject.SetActive(true);
        lifeHard.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(false);
        easyB.gameObject.SetActive(false);
        mediumB.gameObject.SetActive(false);
        hardB.gameObject.SetActive(false);
        muteB.gameObject.SetActive(false);

        RandomColorHard();

    }

    void StartGameEasy()
    {
        gameState = State.InGameEasy;
        shape.gameObject.SetActive(true);

        shape.gameObject.transform.position = new Vector3(0, 1, 0);

        BroadcastMessage("OnStartGame", SendMessageOptions.DontRequireReceiver);

        startBack.gameObject.SetActive(false);
        TitleBack.gameObject.SetActive(false);
        score.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(false);
        easyB.gameObject.SetActive(false);
        mediumB.gameObject.SetActive(false);
        hardB.gameObject.SetActive(false);
        muteB.gameObject.SetActive(false);

        RandomColorEasy();

    }

    public void RandomColorEasy()
    {
        int i = 0;

        MaterialList.Sort((a, b) => 1 - 2 * Random.Range(0, MaterialList.Count));
        shape.GetComponent<MeshRenderer>().material = MaterialList[1];

        MaterialList.Sort((a, b) => 1 - 2 * Random.Range(0, MaterialList.Count));
        foreach(GameObject Area in AreaList)
        {
            Area.GetComponent<MeshRenderer>().material = MaterialList[i];
            i += 1;
        }

    }

    public void RandomColorMedium()
    {
        int i = 0;

        MaterialListMedium.Sort((a, b) => 1 - 2 * Random.Range(0, MaterialListMedium.Count));
        shapeMedium.GetComponent<MeshRenderer>().material = MaterialListMedium[1];

        MaterialListMedium.Sort((a, b) => 1 - 2 * Random.Range(0, MaterialListMedium.Count));
        foreach (GameObject Area in AreaList)
        {
            Area.GetComponent<MeshRenderer>().material = MaterialListMedium[i];
            i += 1;
        }

    }

    public void RandomColorHard()
    {
        int i = 0;

        MaterialListHard.Sort((a, b) => 1 - 2 * Random.Range(0, MaterialListHard.Count));
        shapeHard.GetComponent<MeshRenderer>().material = MaterialListHard[1];

        MaterialListHard.Sort((a, b) => 1 - 2 * Random.Range(0, MaterialListHard.Count));
        foreach (GameObject Area in AreaList)
        {
            Area.GetComponent<MeshRenderer>().material = MaterialListHard[i];
            i += 1;
        }

    }

    public void AddScore()
    {
        score.GetComponent<Text>().color = Color.green;
        score.fontSize = 160;
        scoreCurrent++;
        score.text = score.ToString();
        Invoke("ResetFxText", 0.3f);
        if(isMuted == false)
        {
            _pointPlus.Play();
        }
        
    }
    public void RemoveScore()
    {
        if ( gameState == State.InGameMedium)
        {
            livesMedium.GetComponent<Text>().color = Color.red;
            textLivesMedium.GetComponent<Text>().color = Color.red;
            Invoke("ResetFxTextLives", 0.3f);
        }
        score.GetComponent<Text>().color = Color.red;
        score.fontSize = 160;
        scoreCurrent--;
        score.text = score.ToString();
        FindObjectOfType<CamShake>().shakecamera();
        Invoke("ResetFxText", 0.3f);
        if (isMuted == false)
        {
            _pointMoins.Play();
        }
    }

    public void GameOver()
    {
        gameState = State.GameOver;

        if (scoreCurrent > bestScoreCurrent)
        {
            PlayerPrefs.SetInt("bestScoreCurrent", scoreCurrent);
            bestScore.GetComponent<Text>().color = Color.yellow;
        }
        else
        {
            bestScore.GetComponent<Text>().color = Color.white;
        }

        bestScore.gameObject.SetActive(true);
        score.gameObject.SetActive(false);
        lifeHard.gameObject.SetActive(false);
        livesMedium.gameObject.SetActive(false);
        textLivesMedium.gameObject.SetActive(false);
        Invoke("Restart", 3f);
      }  

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ResetFxText()
    {
        score.fontSize = 150;
        score.GetComponent<Text>().color = Color.white;
    }

    void ResetFxTextLives()
    {
        livesMedium.GetComponent<Text>().color = Color.white;
        textLivesMedium.GetComponent<Text>().color = Color.white;
    }

    void SwitchSound()
    {
        if ( isMuted == false)
        {
            isMuted = true;
            mutebTextMP.color = new Color32(175, 0, 0, 255);
            _music.Pause();
        }
        else
        {
            isMuted = false;
            mutebTextMP.color = new Color32(0, 175, 0, 255);
            _music.Play();
        }
    }

}
