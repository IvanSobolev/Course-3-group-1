using mySolution.Interfaces;

namespace mySolution.Implementation;

public class WavPlayer : IAudioPlayer
{
    public void PlayAlert()
    {
        Console.WriteLine("Wav file playing");
    }
}