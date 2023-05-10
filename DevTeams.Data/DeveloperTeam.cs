// ctrl + b -> hide explorer
public class DeveloperTeam
{
    // empty constructor
    public DeveloperTeam() { }

    //partial constructor
    public DeveloperTeam(string teamName)
    {
        TeamName = teamName;
    }

    //full constructor
    public DeveloperTeam(string teamName, List<Developer> developers)
    {
        TeamName = teamName;
        Developers = developers;
    }

    //[Key]
    public int ID { get; set; }

    public string TeamName { get; set; } = string.Empty;

    public List<Developer> Developers { get; set; } = new List<Developer>();

    public override string ToString()
    {
        var str = $"TeamId: {ID} \n" +
                    $"TeamName: {TeamName}\n" +
                    $"===========Team Members===========\n";
        foreach (Developer dev in Developers)
        {
            str += dev + "\n";
        }
        return str;
    }
}
