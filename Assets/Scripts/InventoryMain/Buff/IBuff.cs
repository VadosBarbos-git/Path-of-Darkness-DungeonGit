
public interface IBuff
{
    void Apply(CharactersDescription User );
    void Remove(CharactersDescription User);
    int GetDuretion();
    IBuff Clone();
}
