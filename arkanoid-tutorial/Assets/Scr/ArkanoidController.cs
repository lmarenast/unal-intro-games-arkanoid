 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ArkanoidController : MonoBehaviour
 {
    private const string BALL_PREFAB_PATH = "Prefabs/Ball";
    private readonly Vector2 BALL_INIT_POSITION = new Vector2(0, -0.86f);

    [SerializeField]
    private GridController _gridController;
 
    [Space(20)]
    [SerializeField]
    private List<LevelData> _levels = new List<LevelData>();
    private Ball _ballPrefab = null;
    private List<Ball> _balls = new List<Ball>();
    private int _currentLevel = 0;
     
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        InitGame();
        }
    }
     
    private void Start()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent += OnBallReachDeadZone;
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent -= OnBallReachDeadZone;
    }

    private void InitGame()
    {
         _currentLevel = 0;
         _gridController.BuildGrid(_levels[0]);
         SetInitialBall();
    }

    private void SetInitialBall()
    {
        ClearBalls();
        Ball ball = CreateBallAt(BALL_INIT_POSITION);
        ball.Init();
        _balls.Add(ball);
    }

     private void ClearBalls()
    {
        for (int i = _balls.Count - 1; i >= 0; i--)
        {
            _balls[i].gameObject.SetActive(false);
            Destroy(_balls[i]);
        }
    
        _balls.Clear();
    }

     private Ball CreateBallAt(Vector2 position)
    {
        if (_ballPrefab == null)
        {
            _ballPrefab = Resources.Load<Ball>(BALL_PREFAB_PATH);
        }
        return Instantiate(_ballPrefab, position, Quaternion.identity);
    }

    private void OnBallReachDeadZone(Ball ball)
    {
        ball.Hide();
        _balls.Remove(ball);
        Destroy(ball.gameObject);

        CheckGameOver();
    }
    
    private void CheckGameOver()
    {
        //Game over
        if (_balls.Count == 0)
        {
            ClearBalls();
            
            Debug.Log("Game Over: LOSE!!!");
        }
    }

 }