using DataSync;
using LabData;

public class PlayerHeadData : LabDataBase
{
    public float PosX { get; private set; }
    public float PosY { get; private set; }
    public float PosZ { get; private set; }
    public float PosW { get; private set; }

    public PlayerHeadData(float posX, float posY, float posZ,float posW)
    {
        PosX = posX;
        PosY = posY;
        PosZ = posZ;
        PosW = posW;
    }
}