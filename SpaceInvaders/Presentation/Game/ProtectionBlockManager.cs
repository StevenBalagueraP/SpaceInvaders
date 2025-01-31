using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace SpaceInvaders.Presentation.Game
{
    internal class ProtectionBlockManager
    {
        private List<ProtectionBlock> _protectionBlocks;
        private Canvas _canvas;
        private bool _isFinishedRound;

        public ProtectionBlockManager(Canvas canvas)
        {
            _canvas = canvas;
            _protectionBlocks = new List<ProtectionBlock>();
            _isFinishedRound = false;

        }
        public void GenerateBlock(int numberBlocks)
        {
            int initialX = -250;
            int initialY = 600 - 200;
            if (!_isFinishedRound)
            {
                _isFinishedRound = true;
                for (int i = 0; i < numberBlocks; i++)
                {
                    ProtectionBlock protectionBlock = new ProtectionBlock(initialX, initialY, "ms-appx:///Assets/Images/protectionBlockImage.png");
                    _protectionBlocks.Add(protectionBlock);
                    Console.WriteLine(_protectionBlocks.Count);
                    Image protectionImage = new Image
                    {
                        Source = new BitmapImage(new Uri(protectionBlock.ImagePath)),
                        Width = 130,
                        Height = 130
                    };
                    Canvas.SetLeft(protectionImage, protectionBlock.X);
                    Canvas.SetTop(protectionImage, protectionBlock.Y);

                    _canvas.Children.Add(protectionImage);
                    initialX += 250;
                    
                }
            }
        }
    }
}
