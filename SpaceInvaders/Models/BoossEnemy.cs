using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Models;

public class BoossEnemy : Enemy
{
    public BoossEnemy(int x, int y, int speed, string imagePath, int points) : base(x, y, speed, imagePath, points)
    {
    }
}
