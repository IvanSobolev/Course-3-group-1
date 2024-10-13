using mySolution.Interfaces;

namespace mySolution.Implementation;

public class MultimediaPlayer
{
    private readonly IAudioPlayer _audioPlayer;
    private readonly IVideoPlayer _videoPlayer;

    public MultimediaPlayer(IAudioPlayer audio, IVideoPlayer video)
    {
        _audioPlayer = audio;
        _videoPlayer = video;
    }

    public void Alert()
    {
        _audioPlayer.PlayAlert();
        _videoPlayer.PlayAlert();
    }
}