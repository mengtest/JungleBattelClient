public class UserData
{
    public int ID { get; private set; }

    public string Username { get; private set; }

    public int TotalCount { get;  set; }

    public int WinCount { get;  set; }

    public UserData(int id, string username, int totalCount, int winCount)
    {
        ID = id;
        Username = username;
        TotalCount = totalCount;
        WinCount = winCount;
    }    
    
    public UserData(string username, int totalCount, int winCount)
    {
        Username = username;
        TotalCount = totalCount;
        WinCount = winCount;
    }
}
