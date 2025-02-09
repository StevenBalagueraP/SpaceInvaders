namespace SpaceInvaders.Models;

public class EnemyManager
{
    private List<Enemy> _enemies;
    private Canvas _canvas;
    private bool isFinishedRound;
    private DispatcherTimer _timer;
    string value = "positive";
    public List<Enemy> Enemies
    {
        get { return _enemies; }
    }


    public EnemyManager(Canvas canvas)
    {
        _enemies = new List<Enemy>();
        isFinishedRound = false;
        _canvas = canvas;
        _timer = new DispatcherTimer();
    }
    public void SpawnBoss()
    {

    }
    public void MoveEnemies()
    {
        _timer.Interval = TimeSpan.FromMilliseconds(50);
        _timer.Tick += (sender, e) => Move();
        if (!_timer.IsEnabled) _timer.Start();

    }
    public void Move()
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

