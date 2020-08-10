using DataSync;
using LabData;

public class PlayerEyeData : LabDataBase
{

    public float PosX { get; private set; }
    public float PosY { get; private set; }
    public float PosZ { get; private set; }

    public PlayerEyeData(float posX, float posY, float posZ)
    {
        PosX = posX;
        PosY = posY;
        PosZ = posZ;
    }
}
