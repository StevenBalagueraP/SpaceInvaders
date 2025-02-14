using SpaceInvaders.Presentation.Game;

namespace SpaceInvaders.Models;

public class EnemyManager
{
    private List<Enemy> _enemies;
    private DispatcherTimer _timer;
    private DispatcherTimer resetBulletsTimer;
    private DispatcherTimer spawnBossTimer;
    private Random _random;
    private Canvas _canvas;
    private string value = "positive";
    private int numberOfBullets;
    private bool isFinishedRound;

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
        _random = new Random();

        resetBulletTimer(startGame);
        SpawnBoossTimer();
        isFinishedRound = false;

    }
    private void resetBulletTimer(StartGame startGame)
    {
        resetBulletsTimer.Interval = TimeSpan.FromSeconds(3);
        resetBulletsTimer.Tick += (sender, e) => {
            numberOfBullets = _random.Next(1, 4);
            generateBullet(startGame);

        };
        resetBulletsTimer.Start();
    }
    private void SpawnBoossTimer()
    {
        spawnBossTimer.Interval = TimeSpan.FromMinutes(1);
        spawnBossTimer.Tick += (sender, e) => SpawnBoss();
        spawnBossTimer.Start();
    }
    public void SpawnBoss()
    {

        int randomNumber = _random.Next(60, 151);
        BoossEnemy boossEnemy = new BoossEnemy(650, 30, 10, "ms-appx:///Assets/Images/boossEnemy.png", randomNumber);
        boossEnemy.addEnemy(45, 45, _canvas);
        Console.WriteLine("holaaa");
    }
    public void MoveEnemies(StartGame startGame)
    {
        
        _timer.Interval = TimeSpan.FromMilliseconds(50);
        _timer.Tick += (sender, e) => Move(startGame);
        if (!_timer.IsEnabled) _timer.Start();

    }
    public void Move(StartGame startGame)
    {
        foreach (Enemy enemy in _enemies)
        {   
            enemy.Update(value);
            
            if (enemy.X == 665) 
            {
                _timer.Stop();
                foreach (Enemy enemy1 in _enemies)
                {
                    enemy1.Y += 10;
                    Canvas.SetTop(enemy1.EnemyImage, enemy1.Y);
                }
                _timer.Start();
                value = "negative";
            }
            if (enemy.X == -300)
            {
                _timer.Stop();
                foreach (Enemy enemy1 in _enemies)
                {
                    enemy1.Y += 10;
                    Canvas.SetTop(enemy1.EnemyImage, enemy1.Y);
                }
                _timer.Start();
                value = "positive";
            }
        }

    }
    public void generateBullet(StartGame startGame)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            if (_enemies.Count >= 15)
            {
                int randomShootingEnemy = _random.Next(0, 15);
                if (_enemies[randomShootingEnemy] is ShootingEnemy)
                {
                    Console.WriteLine(_enemies.Count());
                    Console.WriteLine($"random; {randomShootingEnemy}");

                    Bullet enemyBullet = new Bullet(_enemies[randomShootingEnemy].X - 5, _enemies[randomShootingEnemy].Y, "ms-appx:///Assets/Images/bulletImage.png", _canvas, false);
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
                    Console.WriteLine($"random; {randomShootingEnemy}");

                    Bullet enemyBullet = new Bullet(_enemies[randomShootingEnemy].X - 5, _enemies[randomShootingEnemy].Y, "ms-appx:///Assets/Images/bulletImage.png", _canvas, false);
                    enemyBullet.Move(startGame);
                }
                else
                {

                    Console.WriteLine("papu eso no");
                }
            }   
        }
    }
    public void ResetEnemies(StartGame startGame)
    {
        if (Enemies.Count == 0)
        {
            isFinishedRound = false;
            GenerateNewRound(5, 15);
            startGame.ResetEnemiesSound();
        }

    }
    public void GenerateNewRound(int rows, int columns)
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
                            initialY + i * (enemyHeight + spacing),
                            speed,
                            "ms-appx:///Assets/Images/shootingEnemy.png",
                            40);

                    }
                    else if (i <= 3)
                    {
                        enemy = new AdvancedEnemy(
                            initialX + j * (enemyWidth + spacing),
                            initialY + i * (enemyHeight + spacing),
                            speed,
                            "ms-appx:///Assets/Images/advancedEnemy.png",
                            20);
                    }
                    else if (i <= 5)
                    {
                        enemy = new NormalEnemy(
                            initialX + j * (enemyWidth + spacing),
                            initialY + i * (enemyHeight + spacing),
                            speed,
                            "ms-appx:///Assets/Images/normalEnemy.png",
                            10);
                    }

                    _enemies.Add(enemy);
                    enemy.addEnemy(enemyWidth, enemyHeight, _canvas);
                }
            }
        }
    }





}

