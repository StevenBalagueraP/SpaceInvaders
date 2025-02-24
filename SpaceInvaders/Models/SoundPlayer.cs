using System;
using NAudio.Wave;

public class SoundPlayer
{
    private WaveOutEvent outputDevice;
    private AudioFileReader audioFile;
    private string currentFilePath;

    public void PlaySound(string filePath, bool loop = false)
    {
        StopSound(); // Detener cualquier audio en curso

        try
        {
            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader(filePath);
            currentFilePath = filePath;

            outputDevice.Init(audioFile);

            // 🔄 Si `loop` es `true`, reiniciar audio cuando termine
            if (loop)
            {
                outputDevice.PlaybackStopped += (s, e) => RestartSound();
            }

            outputDevice.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al reproducir sonido: {ex.Message}");
        }
    }

    private void RestartSound()
    {
        if (audioFile != null)
        {
            audioFile.Position = 0; // Reiniciar el audio
            outputDevice.Play();
        }
    }

    public void StopSound()
    {
        if (outputDevice != null)
        {
            outputDevice.Stop();
            outputDevice.Dispose();
            outputDevice = null;
        }

        if (audioFile != null)
        {
            audioFile.Dispose();
            audioFile = null;
        }
    }
}
