 
public class BuffInstance
{
    public IBuff buff;
    private int remainingDuration;
    public BuffInstance(IBuff buff)
    {
        this.buff = buff;
        remainingDuration = buff.GetDuretion();
    }

    public void UpdateDuration()
    {
        remainingDuration--;
    }
    public bool IsExpired()
    {
        return remainingDuration < 0;
    }
}
