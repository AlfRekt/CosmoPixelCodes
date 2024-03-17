using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _killCount;
    [SerializeField] private GameObject _goblin;
    [SerializeField] private GameObject _skeleton;
    [SerializeField] private GameObject _mushroom;

    [SerializeField] private List<Transform> _mushroomSpawn;
    [SerializeField] private List<Transform> _skeletonSpawn;
    [SerializeField] private List<Transform> _goblinSpawn;
    private Slider _healthBar;

    [SerializeField]private GameObject _portal;
    private bool _isPortalEnable = false;

    bool _isSkeletonSpawned;
    bool _isMushroomSpawned;
    bool _isGoblinSpawned;

    public int KillCount { get => _killCount; set => _killCount = value; }
    public bool IsPortalEnable { get => _isPortalEnable; set => _isPortalEnable = value; }
    public Slider HealthBar { get => _healthBar; set => _healthBar = value; }
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _healthBar = GameObject.Find("Slider").GetComponent<Slider>();
    }

    void Start()
    {
        _healthBar.maxValue = 100;
        _healthBar.minValue = 0;
        _healthBar.value = _player.Health;
        _healthBar.wholeNumbers = true;
        _healthBar.fillRect.GetComponent<Image>().color = Color.green;
        _killCount = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isGoblinSpawned)
        {
            FirstPhase();
        }
        if (_killCount == 7 && !_isSkeletonSpawned)
        {
            SecondPhase();
        }

        if (_killCount == 13 && !_isMushroomSpawned)
        {
            ThirdPhase();
        }

        if (_killCount == 18)
        {
            _isPortalEnable = true;
            _portal.SetActive(true);
        }
    }

    void FirstPhase()
    {

        foreach (Transform t in _goblinSpawn)
        {
            Instantiate(_goblin,t.position,_goblin.transform.rotation);
        }
        _isGoblinSpawned = true;
    }

    void SecondPhase()
    {
        foreach (Transform t in _skeletonSpawn)
        {
            Instantiate(_skeleton, t.position, _skeleton.transform.rotation);
        }
        _isSkeletonSpawned = true;
    }

    void ThirdPhase()
    {
        foreach (Transform t in _mushroomSpawn)
        {
            Instantiate(_mushroom, t.position, _mushroom.transform.rotation);
        }
        _isMushroomSpawned = true;
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
