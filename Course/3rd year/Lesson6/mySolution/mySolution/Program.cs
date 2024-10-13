using mySolution.Implementation;
using mySolution.Interfaces;

namespace mySolution;

class Program
{
    static void Main(string[] args)
    {
        IAudioPlayer _audioPlayer = new Mp3Player();
        IVideoPlayer _videoPlayer = new AviPlayer();
        
        MultimediaPlayer _player = new MultimediaPlayer(_audioPlayer, _videoPlayer);
        
        _player.Alert();
    }
}