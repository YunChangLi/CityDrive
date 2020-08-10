using DataSync;
public class GameResultData : LabDataBase
{
    public float GameTimer { get; set; }
    public int   SignalCount { get; set; }
    public float WrongCount { get; set; }

    public GameResultData(float timer, int singalcount, float wrongount)
    {
        GameTimer = timer;
        SignalCount = singalcount;
        WrongCount = wrongount;
    }

    
}
