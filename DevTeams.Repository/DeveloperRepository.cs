public class DeveloperRepository
{
    public DeveloperRepository()
    {
        Seed();
    }
    // we need a virable container that will hold the collection of Developers

    private List<Developer> _developerDb = new List<Developer>();

    // We need to auto increment the developer ID
    private int _count = 0;

    // C.R.U.D

    // Create
    public bool AddDeveloper(Developer developer)
    {
        if (developer is null)
        {
            return false;
        }
        else
        {
            // increment the _count
            _count++;
            // assign the developerId to _count
            developer.ID = _count;

            // save to the database
            _developerDb.Add(developer);

            return true;
        }
    }

    // Read All
    public List<Developer> GetDevelopers()
    {
        return _developerDb;
    }

    // Read by ID
    public Developer GetDeveloperbyID(int devID)
    {
        // loop through the whole database
        foreach (Developer id in _developerDb)
        {
            // check to see if the dev has a matching ID
            if (id.ID == devID)
            {
                return id;
            }
        }
        return null;
    }


    // Update
    public bool UpdateExistingDeveloper(int devID, Developer newDeveloper)
    {
        // lets check if the dev exists
        Developer oldDeveloper = GetDeveloperbyID(devID);

        // so, if we actually have data:
        if (oldDeveloper != null)
        {
            // this is where we update our values....
            oldDeveloper.FirstName = newDeveloper.FirstName;
            oldDeveloper.LastName = newDeveloper.LastName;
            oldDeveloper.HasPluralsight = newDeveloper.HasPluralsight;
            // after values have been updated
            return true;
        }
        // if oldDeveloper data returns null...
        return false;
    }


    // Delete
    public bool DeleteExistingDeveloper(int developerid)
    {
        // lets check if the dev exists
        Developer oldDeveloper = GetDeveloperbyID(developerid);

        if (oldDeveloper != null)
        {
            return _developerDb.Remove(oldDeveloper);
        }

        // otherwise return false
        return false;

    }

    public List<Developer> GetDevelopersWithoutPluralsight()
    {
        // 1. we need an empty list
        List<Developer> devsWithOutPS = new List<Developer>();
        // 2. need to loop through the database
        foreach (Developer developer in _developerDb)
        {
            // 3. check to see if the developer doesn't have pluralsight
            if (developer.HasPluralsight == false)
            {
                // 4. if true we will add the dev to the database
                devsWithOutPS.Add(developer);
            }
        }
        // 5. when all is done we will return..
        return devsWithOutPS;
    }
    private void Seed()
    {
        // create developers to add to the database
        Developer clarissa = new Developer
        {
            FirstName = "Clarissa",
            LastName = "Fields",
            HasPluralsight = false
        };

        Developer sam = new Developer
        {
            FirstName = "Samuel",
            LastName = "Adams",
            HasPluralsight = false
        };

        Developer spencer = new Developer
        {
            FirstName = "Spencer",
            LastName = "Reed",
            HasPluralsight = true
        };

        // add developers to database
        AddDeveloper(clarissa);
        AddDeveloper(sam);
        AddDeveloper(spencer);
    }

}