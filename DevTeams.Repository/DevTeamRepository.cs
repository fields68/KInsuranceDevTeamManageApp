public class DevTeamRepository
{

    // private DeveloperRepository _devRepo = new DeveloperRepository();
    private readonly DeveloperRepository _devRepo;

    public DevTeamRepository(DeveloperRepository devRepo)
    {
        _devRepo = devRepo;
        Seed();
    }
    // we need a virable container that will hold the collection of Developers

    private List<DeveloperTeam> _devTeamDb = new List<DeveloperTeam>();

    // We need to auto increment the developerTeam ID
    private int _count = 0;

    // C.R.U.D

    // Create
    public bool AddDevTeam(DeveloperTeam devTeam)
    {
        if (devTeam is null)
        {
            return false;
        }
        else
        {
            // increment the _count
            _count++;
            // assign the developerTeamId to _count
            devTeam.ID = _count;

            // save to the database
            _devTeamDb.Add(devTeam);

            return true;
        }
    }

    // Read All
    public List<DeveloperTeam> GetDeveloperTeams()
    {
        return _devTeamDb;
    }

    // Read by ID (helper method)
    public DeveloperTeam GetDeveloperTeam(int teamID)
    {
        foreach (DeveloperTeam team in _devTeamDb)
        {
            if (team.ID == teamID)
            {
                return team;
            }
        }
        return null;
        // return _devTeamDb.SingleOrDefault(team => team.ID == teamID);
    }

    // Update
    public bool UpdateExistingDeveloperTeam(int teamID, DeveloperTeam newDeveloperTeam)
    {
        DeveloperTeam oldDeveloperTeam = GetDeveloperTeam(teamID);

        if (oldDeveloperTeam != null)
        {
            oldDeveloperTeam.TeamName = newDeveloperTeam.TeamName;

            if (newDeveloperTeam.Developers.Count() > 0)
            {
                oldDeveloperTeam.Developers = newDeveloperTeam.Developers;
            }

            return true;
        }
        return false;
    }

    // Delete
    public bool DeleteExistingDeveloperTeam(int teamID)
    {
        DeveloperTeam oldDeveloperTeam = GetDeveloperTeam(teamID);

        if (oldDeveloperTeam != null)
        {
            return _devTeamDb.Remove(oldDeveloperTeam);
        }
        else
            return false;
    }

    // Add multiple devs to team
    public bool AddMultipleDevs(int teamID, List<Developer> devsToAdd)
    {
        DeveloperTeam teamInDB = GetDeveloperTeam(teamID);

        if (teamInDB != null)
        {
            teamInDB.Developers.AddRange(devsToAdd);
            return true;
        }
        return false;
    }

    public void Seed()
    {
        DeveloperTeam bBirds = new DeveloperTeam()
        {
            TeamName = "Blue Birds"
        };
        bBirds.Developers.Add(_devRepo.GetDeveloperbyID(1));

        DeveloperTeam rRhinos = new DeveloperTeam()
        {
            TeamName = "Red Rhinos"
        };
        rRhinos.Developers.Add(_devRepo.GetDeveloperbyID(3));
        rRhinos.Developers.Add(_devRepo.GetDeveloperbyID(2));

        DeveloperTeam gGeckos = new DeveloperTeam()
        {
            TeamName = "Green Geckos"
        };
        AddDevTeam(bBirds);
        AddDevTeam(rRhinos);
        AddDevTeam(gGeckos);
    }
}