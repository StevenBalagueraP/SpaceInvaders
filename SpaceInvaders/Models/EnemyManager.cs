using SpaceInvaders.Presentation.Game;

namespace SpaceInvaders.Models;

public class EnemyManager
{
    private List<Enemy> _enemies;
    private DispatcherTimer _timer;
    private DispatcherTimer resetBulletsTimer;
    private DispatcherTimer spawnBossTimer;
    private DispatcherTimer moveBooss;
    private Random _random;
    private Canvas _canvas;
    private string value = "positive";
    private int numberOfBullets;
    private bool isFinishedRound;
    private int incrementablePosition;
    private int round;
    private int bulletSpeed;
    private int enemySpeed;
    public List<Enemy> Enemies
    {
        get { return _enemies; }
    }


    public EnemyManager(Canvas canvas, StartGame startGame)
    {
        _enemies = new List<Enemy>();
        _canvas = canvas;
        _timer = new DispatcherTimer();
        resetBulletsTimer = new DispatcherTimer();
        spawnBossTimer = new DispatcherTimer();
        moveBooss = new DispatcherTimer();
        _random = new Random();

        ResetBulletTimer(startGame);
        SpawnBoossTimer(startGame);
        isFinishedRound = false;
        incrementablePosition = 0;
        bulletSpeed = 30;
        round = 1;

    }
    /// <summary>
    /// Generates a number of bullets that will be launched by the enemy every certain time
    /// </summary>
    /// <param name="startGame">is the view class</param>
    private void ResetBulletTimer(StartGame startGame)
    {
        resetBulletsTimer.Interval = TimeSpan.FromSeconds(3);
        resetBulletsTimer.Tick += (sender, e) => {
            numberOfBullets = _random.Next(1, 4);
            GenerateBullet(startGame);

        };
        resetBulletsTimer.Start();
    }

    /// <summary>
    /// Spawns a boss after 1 minute
    /// </summary>
    /// <param name="startGame">is the view class</param>
    private void SpawnBoossTimer(StartGame startGame)
    {
        spawnBossTimer.Interval = TimeSpan.FromMinutes(1);
        spawnBossTimer.Tick += (sender, e) => SpawnBoss(startGame);
        spawnBossTimer.Start();
    }

    private void SpawnBoss(StartGame startGame)
    {
        int count = _random.Next(0, 2);
        Console.WriteLine(count);
        int number = count == 0 ? -300 : 650;
        string value = number < 0 ? "positive" : "negative";

        int randomNumber = _random.Next(60, 151);
        BossEnemy boossEnemy = new BossEnemy(number, 30, 10, "ms-appx:///Assets/Images/boossEnemy.png", randomNumber);
        boossEnemy.AddEnemy(45, 45, _canvas);
        _enemies.Add(boossEnemy);
        moveBooss.Interval = TimeSpan.FromMilliseconds(10);
        moveBooss.Tick += (sender, e) =>
        {
            if (boossEnemy.Update(value, _canvas))
            {
                _enemies.Remove(boossEnemy);

                boossEnemy.RemoveEnemy(_canvas);
                ResetEnemies(startGame);
            }
        };
        if (!moveBooss.IsEnabled) moveBooss.Start();
        
    }
    public void MoveEnemies(StartGame startGame)
    {
        
        _timer.Interval = TimeSpan.FromMilliseconds(50);
        _timer.Tick += (sender, e) => Move(startGame);
        if (!_timer.IsEnabled) _timer.Start();
        

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="startGame">is the view class</param>
    public void Move(StartGame startGame)
    {
        foreach (Enemy enemy in _enemies)
        {   
            if (!(enemy is BossEnemy))
            {
                enemy.Update(value, _canvas);

                if (enemy.XPosition == 665)
                {
                    _timer.Stop();
                    foreach (Enemy enemy1 in _enemies)
                    {
                        enemy1.YPosition += 20;
                        Canvas.SetTop(enemy1.EnemyImage, enemy1.YPosition);
                    }
                    _timer.Start();
                    value = "negative";
                    validateEnemySpeed();
                    Console.WriteLine(_timer.Interval.TotalMilliseconds);
                }
                if (enemy.XPosition == -300)
                {
                    _timer.Stop();
                    foreach (Enemy enemy1 in _enemies)
                    {
                        enemy1.YPosition += 25;
                        Canvas.SetTop(enemy1.EnemyImage, enemy1.YPosition);
                    }
                    _timer.Start();
                    value = "positive";
                    validateEnemySpeed();
                    Console.WriteLine(_timer.Interval.TotalMilliseconds);
                }
                if (enemy.YPosition >= 500)
                {
                    startGame.GameOverPlay();
                }
            }
            
        }

    }
    private void GenerateBullet(StartGame startGame)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            if (_enemies.Count >= 15)
            {
                int randomShootingEnemy = _random.Next(0, 15);
                if (_enemies[randomShootingEnemy] is ShootingEnemy)
                {
                    Console.WriteLine(_enemies.Count());
                    Bullet enemyBullet = new Bullet(_enemies[randomShootingEnemy].XPosition - 5, _enemies[randomShootingEnemy].YPosition, "ms-appx:///Assets/Images/bulletImage.png", _canvas, false);
                    enemyBullet.SetSpeed(bulletSpeed);
                    Console.WriteLine($"si entra aqui {bulletSpeed}");
                    enemyBullet.Move(startGame);
                }
                else
                {

                    Console.WriteLine("papu eso no");
                }
            }
            else
            {
                int randomShootingEnemy = _random.Next(0, _enemies.Count);
                if (_enemies[randomShootingEnemy] is ShootingEnemy)
                {
                    Console.WriteLine(_enemies.Count());
                    Bullet enemyBullet = new Bullet(_enemies[randomShootingEnemy].XPosition - 5, _enemies[randomShootingEnemy].YPosition, "ms-appx:///Assets/Images/bulletImage.png", _canvas, false);
                    enemyBullet.SetSpeed(bulletSpeed);
                    Console.WriteLine($"si entra aqui {bulletSpeed}");
                    enemyBullet.Move(startGame);
                }
            }   
        }
    }
    /// <summary>
    /// reset the round when all the enemies was died
    /// </summary>
    /// <param name="startGame">is the view</param>
    public void ResetEnemies(StartGame startGame)
    {
        if (Enemies.Count == 0)
        {
            ResetSpeed();
            round++;
            isFinishedRound = false;
            enemySpeed += 5; 
            if (round >= 5)
            {
                bulletSpeed -= 5;
            }
            incrementablePosition += 30;
            GenerateNewRound(5, 15, incrementablePosition);
            startGame.ResetEnemiesSound();
        }

    }
    /// <summary>
    /// generates a new round at the start of the game
    /// </summary>
    /// <param name="rows"> number of rows</param>
    /// <param name="columns">number of columns</param>
    public void GenerateNewRound(int rows, int columns, int incrementableY)
    {
        int canvasWidth = (int)_canvas.ActualWidth;
        int canvasHeight = (int)_canvas.ActualHeight;
        int enemyWidth = 33;
        int enemyHeight = 33;
        int spacing = 10;

        int initialX = -110;

        int initialY = 25;

        int speed = 5;

        if (!isFinishedRound)
        {
            isFinishedRound = true;

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Enemy? enemy = null;

                    if (i == 1)
                    {
                        enemy = new ShootingEnemy(
                            initialX + j * (enemyWidth + spacing),
                            (initialY + i * (enemyHeight + spacing)) + incrementableY,
                            speed,
                            "ms-appx:///Assets/Images/shootingEnemy.png",
                            40);

                    }
                    else if (i <= 3)
                    {
                        enemy = new AdvancedEnemy(
                            initialX + j * (enemyWidth + spacing),
                            (initialY + i * (enemyHeight + spacing)) + incrementableY,
                            speed,
                            "ms-appx:///Assets/Images/advancedEnemy.png",
                            20);
                    }
                    else if (i <= 5)
                    {
                        enemy = new NormalEnemy(
                            initialX + j * (enemyWidth + spacing),
                            (initialY + i * (enemyHeight + spacing)) + incrementableY,
                            speed,
                            "ms-appx:///Assets/Images/normalEnemy.png",
                            10);
                    }

                    _enemies.Add(enemy);
                    enemy.AddEnemy(enemyWidth, enemyHeight, _canvas);
                }
            }
        }
    }
    /// <summary>
    /// Validates the speed when enemies touch the edges
    /// </summary>
    public void validateEnemySpeed()
    {
        if (_timer.Interval.TotalMilliseconds > 0)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(_timer.Interval.TotalMilliseconds - 3);
        }
        
    }
    public void ResetSpeed()
    {
        if ( _timer.Interval.TotalMilliseconds < 30)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(30);
        }
        
    }
    /// <summary>
    /// reset all the timers for enemies
    /// </summary>
    public void ResetTimers()
    {
        _timer.Stop();
        resetBulletsTimer.Stop();
        spawnBossTimer.Stop();
        moveBooss.Stop();
    }
}

