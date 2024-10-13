using mySolution.Interfaces;

namespace mySolution.Implementation;

public class Mp3Player : IAudioPlayer
{
    public void PlayAlert()
    {
        Console.WriteLine("Mp3 file playing");
    }
}