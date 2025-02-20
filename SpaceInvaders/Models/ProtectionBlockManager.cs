using SpaceInvaders.Presentation.Game;

namespace SpaceInvaders.Models;

public class ProtectionBlockManager
{
    private List<ProtectionBlock> _protectionBlocks;
    private Canvas _canvas;
    private bool _isFinishedRound;

    public List<ProtectionBlock> ProtectionBlocks
    {
        get { return _protectionBlocks; }
    }

    public ProtectionBlockManager(Canvas canvas)
    {
        _canvas = canvas;
        _protectionBlocks = new List<ProtectionBlock>();
        _isFinishedRound = false;
    }

    /// <summary>
    /// generates the blocks that will be used as defense
    /// </summary>
    /// <param name="numberBlocks"> number of blocks puted on the screen</param>
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
                protectionBlock.AddBlock(_canvas);
                initialX += 250;

            }
        }
    }
}
