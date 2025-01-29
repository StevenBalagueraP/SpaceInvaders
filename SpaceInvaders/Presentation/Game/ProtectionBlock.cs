namespace SpaceInvaders.Presentation.Game
{
    internal class ProtectionBlock
    {
        private int _x;
        private int _y;
        private string _imagePath;
        private int _healt;
        private bool _isDestroyed;

        public ProtectionBlock(int x, int y, string imagePath)
        {
            _x = x;
            _y = y;
            _imagePath = imagePath;
            _healt = 5;
            _isDestroyed = false;

        }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public string ImagePath { get { return _imagePath; } }

        public int Healt { get { return _healt; } }

        public bool IsDestroyed { 
            get { return _isDestroyed; }
            set { _isDestroyed = value; }
        }

        public void CollidesWith(Object _object)
        {

        }


    }
}
