namespace Library;

public class Settings
{
    private bool _showPossibilities = true;
    public bool ShowPossibilities
    {
        get => _showPossibilities;
        set
        {
            if (_showPossibilities != value)
            {
                _showPossibilities = value;
                OnPossibilitiesChanged();
            }
        }
    }
    
    public event Action PossibilitiesChanged;
    public Settings()
    {
    }

    protected virtual void OnPossibilitiesChanged()
    {
        PossibilitiesChanged.Invoke();
    }
}