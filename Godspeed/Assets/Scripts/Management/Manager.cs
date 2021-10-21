using Management;

public interface Manager 
{
    public GameManager gameManager { get; set; }
    public void Init();
}
