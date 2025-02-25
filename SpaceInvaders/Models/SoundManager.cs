namespace SpaceInvaders.Models;

public class SoundManager
{
    private SoundPlayer soundPlayer;
    private SoundPlayer enemyCollided;
    private SoundPlayer bulletSound;
    private SoundPlayer protectionBlockSound;
    private SoundPlayer resetEnemiesSound;
    private SoundPlayer gameOverSound;

    public SoundManager()
    {
        soundPlayer = new SoundPlayer();
        enemyCollided = new SoundPlayer();
        bulletSound = new SoundPlayer();
        protectionBlockSound = new SoundPlayer();
        resetEnemiesSound = new SoundPlayer();
        gameOverSound = new SoundPlayer();
            
    }

    public void StartGameSound()
    {
        soundPlayer.PlaySound("Assets/Sounds/StartGameSound.mp3", true);
    }
    public void DamageEnemiesSound()
    {
        enemyCollided.PlaySound("Assets/Sounds/DamageEnemies.mp3");
    }
    public void PauseDamageEnemiesSound()
    {
        enemyCollided.StopSound();
    }
    public void ShootingSoundMedia()
    {
        bulletSound.PlaySound("Assets/Sounds/ShootingSound.wav");
    }
    public void ProtectionBlockSound()
    {
        protectionBlockSound.PlaySound("Assets/Sounds/ProtectionBlockDamage.mp3");
    }
    public void ResetEnemiesSound()
    {
        resetEnemiesSound.PlaySound("Assets/Sounds/ResetEnemies.mp3");
    }
    public void GameOverSound()
    {
        gameOverSound.PlaySound("Assets/Sounds/GameOverSound.mp3");
    }
    public void PlayerDamageSound()
    {
        gameOverSound.PlaySound("Assets/Sounds/PlayerDamage.mp3");
    }
    public void IncreaseLivesSound()
    {
        gameOverSound.PlaySound("Assets/Sounds/IncreaseLives.mp3");
    }
    public void PausePlayerDamageSound()
    {
        gameOverSound.StopSound();
    }
    public void PauseGameSound()
    {
        soundPlayer.StopSound();
    }
}
