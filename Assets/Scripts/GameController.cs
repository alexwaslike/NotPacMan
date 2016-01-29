using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // prefabs
    public GameObject ghost;
    public GameObject hope;
    public GameObject hopeUsedAnimation;

    // scene refs
    public SoundController soundController;
    public Camera mainCamera;
    public GameObject HUD;
    public GameObject gameStartUI;
    public GameObject loseUI;
    public GameObject winUI;
    public GameObject pauseUI;
    public GameObject mainCharacter;
    public MainCharacterObj mainCharacterObj;

    // balance vars
    public int numGhostsToSpawn = 10;
    public int numHopeToSpawn = 9;
    public float mapRadius = 100.0f;

    // state control
    private bool allowGameplay;
    public bool AllowGameplay
    {
        get { return allowGameplay; }
    }
    private bool _gamePaused;
    private float _timeScale;

    void Awake() {
        allowGameplay = false;
        _gamePaused = false;
        _timeScale = Time.timeScale;

        // spawn ghosts
        Vector3 newPosition;
        for (int i = 0; i < numGhostsToSpawn; i++)
        {
            newPosition = GetRandomPointWithinRadius(mapRadius);
            for (int j=0; j<30; j++)
            {
                if (Vector3.Distance(newPosition, mainCharacter.transform.position) < 15.0f)
                    newPosition = GetRandomPointWithinRadius(mapRadius);
            }
                
            GameObject newGhost = Instantiate(ghost, newPosition, Quaternion.identity) as GameObject;
            newGhost.GetComponentInChildren<GhostObj>().controller = this;
        }
        // spawn hope
        SpawnNewHope(numHopeToSpawn);
    }

    private void SpawnNewHope(int numToSpawn)
    {
        Vector3 newPosition;
        for (int i = 0; i < numToSpawn; i++)
        {
            newPosition = GetRandomPointWithinRadius(mapRadius);
            GameObject newHope = Instantiate(hope, new Vector3(newPosition.x, hope.transform.position.y, newPosition.z), Quaternion.identity) as GameObject;
            newHope.GetComponent<HopeObj>().controller = this;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (_gamePaused)
            {
                PauseGame(paused: false);
            }
            else
            {
                PauseGame(paused: true);
            }
        }
    }

    // GENERAL HANDLERS

    public void ClickedHopeObj(bool isEndGameHope)
    {
        if (isEndGameHope)
            GameEnd(wonGame: true);
        else
        {
            mainCharacterObj.IncreaseHope();
            SpawnNewHope(1);
        }
    }

    public void GhostCanSeePlayer()
    {
        if(allowGameplay)
            mainCharacterObj.DecreaseHope();
    }

    public void GameEnd(bool wonGame)
    {
        allowGameplay = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        HUD.SetActive(false);

        mainCharacter.GetComponent<CharacterController>().enabled = false;
        mainCharacter.GetComponent<MouseLook>().enabled = false;

        if (wonGame)
        {
            winUI.SetActive(true);
        } else
        {
            loseUI.SetActive(true);
        }
    }

    // BUTTON HANDLERS

    public void ReturnToMainMenu()
    {
        Time.timeScale = _timeScale;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = _timeScale;
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        PauseGame(paused: false);
        gameStartUI.SetActive(false);
        HUD.SetActive(true);
    }

    public void UnpauseGame()
    {
        PauseGame(paused: false);
    }


    // UTILITY

    private void PauseGame(bool paused)
    {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
            mainCharacter.GetComponent<CharacterController>().enabled = false;
            mainCharacter.GetComponent<MouseLook>().enabled = false;
            _gamePaused = true;
            allowGameplay = false;
            pauseUI.SetActive(true);
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = _timeScale;
            mainCharacter.GetComponent<CharacterController>().enabled = true;
            mainCharacter.GetComponent<MouseLook>().enabled = true;
            _gamePaused = false;
            allowGameplay = true;
            pauseUI.SetActive(false);
        }
    }

    public Vector3 GetRandomPointWithinRadius(float maxRadius)
    {
        Vector3 randomInsideCircle;
        Vector3 randomPoint;
        for (int i = 0; i < 30; i++)
        {
            randomInsideCircle = new Vector3(Random.insideUnitCircle.x, 0.0f, Random.insideUnitCircle.y);
            randomPoint = randomInsideCircle * maxRadius;
            randomPoint.y = 0.0f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, maxRadius, NavMesh.AllAreas))
            {
                return (hit.position);
            }
        }
        return Vector3.zero;
    }

    public Vector3 GetRandomPointWithinRadius(float maxRadius, Vector3 centerPoint)
    {
        Vector3 randomInsideCircle;
        Vector3 randomPoint;
        for (int i = 0; i < 30; i++)
        {
            randomInsideCircle = new Vector3(Random.insideUnitCircle.x, 0.0f, Random.insideUnitCircle.y);
            randomPoint = centerPoint + (randomInsideCircle * maxRadius);
            randomPoint.y = 0.0f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, maxRadius, NavMesh.AllAreas))
            {
                return (hit.position);
            }
        }
        return Vector3.zero;
    }

}
