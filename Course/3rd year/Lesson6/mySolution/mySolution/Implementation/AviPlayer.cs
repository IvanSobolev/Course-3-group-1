using mySolution.Interfaces;

namespace mySolution.Implementation;

public class AviPlayer : IVideoPlayer
{
    public void PlayAlert()
    {
        Console.WriteLine("Avi video playing");
    }
}