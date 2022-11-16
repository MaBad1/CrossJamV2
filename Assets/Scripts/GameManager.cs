using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject shape;
    [SerializeField] GameObject shapeMedium;
    [SerializeField] GameObject shapeHard;
    [SerializeField] GameObject easyB;
    [SerializeField] GameObject mediumB;
    [SerializeField] GameObject hardB;

    public float timer;
    private int timerInt;
    public Text textTimer;
    int scoreCurrent = 0;
    int bestScoreCurrent = 0;
    bool easyClick = false;
    bool mediumClick = false;
    bool hardClick = false;

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

        shape.gameObject.SetActive(false);
        shapeMedium.gameObject.SetActive(false);
        shapeHard.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        bestScore.gameObject.SetActive(true);
        bestScore.text = "High score :" + PlayerPrefs.GetInt("bestScoreCurrent").ToString();
    }

    // Update is called once per frame
    void Update()
    {

        easyB.GetComponent<Button>().onClick.AddListener(EasyStart);
        mediumB.GetComponent<Button>().onClick.AddListener(MediumStart);
        hardB.GetComponent<Button>().onClick.AddListener(HardStart);

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
            }
        }
        if (gameState == State.InGameMedium)
        {

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
            else
            {
                GameOver();
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
            else
            {
                GameOver();
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
        bestScore.gameObject.SetActive(false);
        easyB.gameObject.SetActive(false);
        mediumB.gameObject.SetActive(false);
        hardB.gameObject.SetActive(false);

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
        bestScore.gameObject.SetActive(false);
        easyB.gameObject.SetActive(false);
        mediumB.gameObject.SetActive(false);
        hardB.gameObject.SetActive(false);

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
    }
    public void RemoveScore()
    {
        score.GetComponent<Text>().color = Color.red;
        score.fontSize = 160;
        scoreCurrent--;
        score.text = score.ToString();
        FindObjectOfType<CamShake>().shakecamera();
        Invoke("ResetFxText", 0.3f);
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
        Invoke("Restart", 2f);
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



}
