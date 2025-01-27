using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Media.Devices.Core;

namespace SpaceInvaders.Presentation.Game;

internal class EnemyManager
{
    List<Enemy> _enemies;
    Canvas _canvas;
    bool isFinishedRound;



    public EnemyManager(Canvas canvas)
    {
        _enemies = new List<Enemy>();
        isFinishedRound = false;
        _canvas = canvas;
    }
    public void UpdateEnemies()
    {

    }
    public void SpawnBoss()
    {

    }
    public void ResetEnemies()
    {

    }
    public void GenerateNewRound(int rows, int columns)
    {
        int canvasWidth = (int)_canvas.ActualWidth;
        int canvasHeight = (int)_canvas.ActualHeight;
        int enemyWidth = 40;
        int enemyHeight = 40;
        int spacing = 10;

        int totalRowWidth = (columns * (enemyWidth + spacing)) - spacing;
        int initialX = (canvasWidth - totalRowWidth) / 2;

        int totalEnemiesHeight = (rows * (enemyHeight + spacing)) - spacing;
        int initialY = canvasHeight - totalEnemiesHeight - 100; 

        int speed = 5;

        if (!isFinishedRound)
        {
            isFinishedRound = true;

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Enemy enemy = null;

                    if (i == 1)
                    {
                        enemy = new NormalEnemy(
                            initialX + j * (enemyWidth + spacing),
                            initialY + i * (enemyHeight + spacing),
                            speed,
                            "ms-appx:///Assets/Images/normalEnemy.png",
                            10);
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
                        enemy = new ShootingEnemy(
                            initialX + j * (enemyWidth + spacing),
                            initialY + i * (enemyHeight + spacing),
                            speed,
                            "ms-appx:///Assets/Images/shootingEnemy.png",
                            40);
                    }

                    _enemies.Add(enemy);

                    Image enemyImage = new Image
                    {
                        Source = new BitmapImage(new Uri(enemy.ImagePath)),
                        Width = enemyWidth,
                        Height = enemyHeight
                    };

                    Canvas.SetLeft(enemyImage, enemy.X);
                    Canvas.SetTop(enemyImage, enemy.Y);

                    _canvas.Children.Add(enemyImage);
                }
            }
            Console.WriteLine(_enemies.Count);
        }
    }




}

