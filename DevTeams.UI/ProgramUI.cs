using static System.Console;

public class ProgramUI
{
    // Globally scoped vairable container with the DeveloperRepository Data
    private DeveloperRepository _dRepo = new DeveloperRepository();

    private DevTeamRepository _dTRepo;
    public ProgramUI()
    {
        _dTRepo = new DevTeamRepository(_dRepo);
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        bool IsRunning = true;

        while (IsRunning)
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine("Welcome to Komodo DevTeams\nPlease make a selection:");
            ResetColor();

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("==========Developer Management==========\n");
            ResetColor();
            WriteLine("1.  Show all Developers \n" +
                        "2.  Find Developer by ID\n" +
                        "3.  Edit Developers Menu\n");

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("==========Dev Team Management==========\n");
            ResetColor();
            WriteLine("4.  Show all Developer Teams\n" +
                        "5.  Find Developer Team by ID\n" +
                        "6.  Edit Developer Teams Menu\n");

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("==========Bonus==========\n");
            ResetColor();
            WriteLine("7. Developers without Pluralsight Account\n" +
                        "8. Add Multiple Developers to a team\n");

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("==========Exit Application==========\n");
            ResetColor();
            WriteLine("00. Exit Application");

            string userInput = ReadLine()!;

            switch (userInput)
            {
                case "1":
                    ShowAllDevelopers();
                    break;
                case "2":
                    FindDeveloperByID();
                    break;
                case "3":
                    EditDeveloperMenu();
                    break;
                case "4":
                    ShowAllDeveloperTeams();
                    break;
                case "5":
                    FindDeveloperTeamByID();
                    break;
                case "6":
                    EditDeveloperTeamsMenu();
                    break;
                case "7":
                    DevsWoPSAccount();
                    break;
                case "8":
                    AddMultiDevstoATeam();
                    break;
                case "00":
                    IsRunning = false;
                    ForegroundColor = ConsoleColor.Magenta;
                    WriteLine("Thanks for using DevTeams");
                    WaitKeyPress();
                    ResetColor();
                    Clear();
                    break;
                default:
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine($"{userInput} is not an option.\n" +
                                "Please enter a valid number between 1-8 or 00");
                    ResetColor();
                    WaitKeyPress();
                    break;
            }


        }
    }

    private void ShowAllDevelopers()
    {
        Clear();

        ShowEnlistedDevs();

        WaitKeyPress();
    }

    private void ShowEnlistedDevs()
    {
        Clear();
        ForegroundColor = ConsoleColor.Magenta;
        WriteLine("***Developer Listing***");
        ResetColor();
        List<Developer> devsInDb = _dRepo.GetDevelopers();
        ValadateDeveloperDataBase(devsInDb);
    }

    private void ValadateDeveloperDataBase(List<Developer> devsInDb)
    {
        if (devsInDb.Count() > 0)
        {
            foreach (Developer dev in devsInDb)
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            WriteLine("There are no Developers in the Database.");
        }
    }

    private void DisplayDevData(Developer dev)
    {
        WriteLine(dev);
        ForegroundColor = ConsoleColor.Magenta;
        WriteLine("###############################################################");
        ResetColor();
    }

    private void FindDeveloperByID()
    {
        Clear();
        ShowEnlistedDevs();
        WriteLine("~~~~~~~~~~~~~~~\n");

        try
        {
            WriteLine("Select Developer by ID.");
            int userInputDevID = int.Parse(ReadLine()!);
            ValadateDeveloperInDataBase(userInputDevID);
            WaitKeyPress();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private bool ValadateDeveloperInDataBase(int userInputDevID)
    {
        Developer dev = GetDeveloperDataFromDB(userInputDevID);
        if (dev != null)
        {
            Clear();
            DisplayDevData(dev);
            return true;
        }
        else
        {
            WriteLine($"The developer with the ID: {userInputDevID} doesn't exist!");
            return false;
        }
    }

    private void SomethingWentWrong()
    {
        ForegroundColor = ConsoleColor.DarkRed;
        WriteLine("Something went wrong. \n" +
                    "Please Try Again \n" +
                    "Returning to previous Menu");
        ResetColor();
    }

    private void EditDeveloperMenu()
    {
        bool editDeveloper = true;

        while (editDeveloper)
        {
            Clear();

            ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine("=====Edit Developer Menu=====");
            ResetColor();
            WriteLine($"Please make a selection:\n" +
                                        "1. Add new Developers \n" +
                                        "2. Delete Developer\n" +
                                        "3. Update Developer Information\n" +
                                        "4. Go Back"
                                        );

            string userInput = ReadLine()!;

            switch (userInput)
            {
                case "1":
                    AddDev();
                    break;
                case "2":
                    DeleteDev();
                    break;
                case "3":
                    UpdateDevInfo();
                    break;
                case "4":
                    editDeveloper = false;
                    break;
                default:
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine($"{userInput} is not an option.\n" +
                                "Please select a number between 1-4.\n");
                    ResetColor();
                    WaitKeyPress();
                    break;
            }

        }
    }

    private void AddDev()
    {
        Clear();
        try
        {
            Developer dev = InitializeDevCreationSetUp();
            if (_dRepo.AddDeveloper(dev))
            {
                WriteLine($"Successfully added {dev.FullName} to the database!");
                WaitKeyPress();
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private Developer InitializeDevCreationSetUp()
    {
        Developer dev = new Developer();

        WriteLine("***Add Developer Menu***");
        WriteLine("What is the developers FIRST NAME?");
        dev.FirstName = ReadLine()!;

        WriteLine("What is the developers LAST NAME?");
        dev.LastName = ReadLine()!;

        WriteLine("Does this developer have a Pluralsight Account?\n" +
                    "1. Yes\n" +
                    "2. No \n");

        bool hasMadeSelection = false;

        while (hasMadeSelection == false)
        {
            string userInputPSAccount = ReadLine()!;
            switch (userInputPSAccount)
            {
                case "1":
                    dev.HasPluralsight = true;
                    hasMadeSelection = true;
                    break;
                case "2":
                    dev.HasPluralsight = false;
                    hasMadeSelection = true;
                    break;
                default:
                    WriteLine("Invalid Selection");
                    WaitKeyPress();
                    break;
            }
        }
        return dev;
    }

    private void DeleteDev()
    {
        Clear();
        ShowEnlistedDevs();
        WriteLine("~~~~~~~~~~~~~~~\n");

        try
        {
            WriteLine("Select Developer by ID.");
            int userInputDevID = int.Parse(ReadLine()!);
            Developer devInDb = GetDeveloperDataFromDB(userInputDevID);
            var isValidated = ValadateDeveloperInDataBase(userInputDevID);

            if (isValidated)
            {
                WriteLine($"Do you want to delete {devInDb.FullName} from the database? y/n");
                string userInputDeleteDev = ReadLine()!.ToLower();

                if (userInputDeleteDev == "y")
                {
                    if (_dRepo.DeleteExistingDeveloper(userInputDevID))
                    {
                        WriteLine($"{devInDb.FullName} was deleted!");
                    }
                    else
                    {
                        WriteLine($"{devInDb.FullName} WAS NOT deleted!");
                    }
                }
            }
            else
            {

                WriteLine("Please select and existing developer.");
            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
        WaitKeyPress();
    }

    private void UpdateDevInfo()
    {
        Clear();
        ShowEnlistedDevs();
        WriteLine("~~~~~~~~~~~~~~~\n");
        try
        {
            WriteLine("Select Developer by ID.");

            int userInputDevID = int.Parse(ReadLine()!);

            Developer devInDb = GetDeveloperDataFromDB(userInputDevID);

            bool isValidated = ValadateDeveloperInDataBase(userInputDevID);

            if (isValidated)
            {
                WriteLine("Do you want to update this developer? y/n");
                string userInputUpdateDev = ReadLine()!.ToLower();

                if (userInputUpdateDev == "y")
                {
                    Developer updateDevData = InitializeDevCreationSetUp();

                    if (_dRepo.UpdateExistingDeveloper(devInDb.ID, updateDevData))
                    {
                        WriteLine($"The developer {updateDevData.FullName} has been updated!");
                    }
                    else
                    {

                        WriteLine($"The developer {updateDevData.FullName} HAS NOT been updated!");
                    }
                }
            }
            else
            {
                WriteLine("Please select and existing developer.");
            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
        WaitKeyPress();
    }

    private Developer GetDeveloperDataFromDB(int userInputDevID)
    {
        return _dRepo.GetDeveloperbyID(userInputDevID);
    }

    private void ShowAllDeveloperTeams()
    {
        Clear();
        ForegroundColor = ConsoleColor.DarkMagenta;
        WriteLine("***Developer Team Listing***");
        ResetColor();
        GetDevTeamData();
        WaitKeyPress();
    }

    private void GetDevTeamData()
    {
        List<DeveloperTeam> dTeams = _dTRepo.GetDeveloperTeams();
        if (dTeams.Count() > 0)
        {
            foreach (DeveloperTeam team in dTeams)
            {
                DisplayDevTeamData(team);
            }
        }
        else
        {
            WriteLine("There are no Developer Teams formed!");
        }
    }

    private void DisplayDevTeamData(DeveloperTeam team)
    {
        WriteLine(team);
        ForegroundColor = ConsoleColor.Magenta;
        WriteLine("###############################################################");
        ResetColor();
    }

    private void FindDeveloperTeamByID()
    {
        Clear();
        ForegroundColor = ConsoleColor.DarkMagenta;
        WriteLine("***Developer Team Listing***");
        ResetColor();
        GetDevTeamData();
        List<DeveloperTeam> devTeam = _dTRepo.GetDeveloperTeams();
        if (devTeam.Count() > 0)
        {
            WriteLine("Select DevTeam by ID");
            int userInputDevTeamID = int.Parse(ReadLine()!);
            ValadateDevTeamData(userInputDevTeamID);
        }
        WaitKeyPress();
    }

    private void ValadateDevTeamData(int userInputDevTeamID)
    {
        Clear();
        DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamID);
        if (team != null)
        {
            DisplayDevTeamData(team);
        }
        else
        {
            WriteLine($"Sorry the team with ID {userInputDevTeamID} does not exist!");
        }
    }

    private void EditDeveloperTeamsMenu()
    {
        bool editDevTeam = true;

        while (editDevTeam)
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine("=====Edit Developer Team Menu=====");
            ResetColor();
            WriteLine($"Please make a selection:\n" +
                                        "1. Add new Developer Team \n" +
                                        "2. Delete Developer Team\n" +
                                        "3. Update Developer Team Information\n" +
                                        "4. Go Back"
                                        );

            string userInput = ReadLine()!;

            switch (userInput)
            {
                case "1":
                    AddDevTeam();
                    break;
                case "2":
                    DeleteDevTeam();
                    break;
                case "3":
                    UpdateDevTeamInfo();
                    break;
                case "4":
                    editDevTeam = false;
                    break;
                default:
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine($"{userInput} is not an option.\n" +
                                "Please select a number between 1-4.\n");
                    ResetColor();
                    WaitKeyPress();
                    break;
            }

        }
    }

    private void AddDevTeam()
    {
        Clear();
        DeveloperTeam dTeam = InitalizeDevTeamCreation();
        if (_dTRepo.AddDevTeam(dTeam))
        {
            WriteLine($"Success, {dTeam.TeamName} was added!");
        }
        else
        {
            WriteLine("Fail, please try again.");
        }
        WaitKeyPress();
    }

    private DeveloperTeam InitalizeDevTeamCreation()
    {
        try
        {
            DeveloperTeam team = new DeveloperTeam();
            WriteLine("Please enter the new TEAM NAME.");
            team.TeamName = ReadLine()!;
            bool hasFilledPositions = false;

            List<Developer> auxDevelopers = new List<Developer>();
            foreach (Developer auxDev in _dRepo.GetDevelopers())
            {
                auxDevelopers.Add(auxDev);
            }

            while (hasFilledPositions == false)
            {
                WriteLine("Does this team have any developers? y/n");
                string userInputAnyDevs = ReadLine()!.ToLower();
                if (userInputAnyDevs == "y")
                {
                    if (auxDevelopers.Count() > 0)
                    {
                        DisplayDevsInDb(auxDevelopers);
                        WriteLine("Select Developer by ID");
                        int userInputDevID = int.Parse(ReadLine()!);

                        Developer selectedDev = _dRepo.GetDeveloperbyID(userInputDevID);

                        bool devAlreadyAdded = false;

                        if (selectedDev != null)
                        {
                            foreach (Developer aDev in auxDevelopers)
                            {
                                // to check and see if you have already added this dev to the team
                                switch (aDev == selectedDev)
                                {
                                    case true:
                                        devAlreadyAdded = false;
                                        break;
                                    default:
                                        devAlreadyAdded = true;
                                        break;
                                }
                                if (!devAlreadyAdded)
                                {
                                    break;
                                }
                            }
                            if (!devAlreadyAdded)
                            {
                                team.Developers.Add(selectedDev);
                                auxDevelopers.Remove(selectedDev);
                            }
                            else
                            {
                                ForegroundColor = ConsoleColor.Cyan;
                                WriteLine($"You have already added {selectedDev.FullName} ID: {selectedDev.ID} to the team!");
                                ResetColor();
                            }
                        }
                        else
                        {
                            WriteLine($"Sorry the developer with ID {userInputDevID} does not exist.");
                        }
                    }
                    else
                    {
                        WriteLine("There are no developers in databse!");
                        WaitKeyPress();
                        break;
                    }
                }
                else
                {
                    hasFilledPositions = true;
                }
            }
            return team;
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
        return null;
    }

    private void DisplayDevsInDb(List<Developer> auxDevelopers)
    {
        if (auxDevelopers.Count() > 0)
        {
            foreach (Developer dev in auxDevelopers)
            {
                WriteLine(dev);
            }
        }
    }

    private void DeleteDevTeam()
    {
        try
        {
            Clear();
            WriteLine("***Developer Team Listing***");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();

            if (dTeam.Count() > 0)
            {
                WriteLine("Please select a developer team by ID for DELETION.");
                int userInputDevTeamID = int.Parse(ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamID);

                if (team != null)
                {
                    WriteLine($"Are you sure you want to DELETE {team.TeamName} from the database? y/n");
                    string userInputDeleteDevTeam = ReadLine()!.ToLower();
                    if (userInputDeleteDevTeam == "y")
                    {
                        if (_dTRepo.DeleteExistingDeveloperTeam(team.ID))
                        {
                            WriteLine($"Success, {team.TeamName} has been DELETED");
                        }
                        else
                        {
                            WriteLine("Fail, please try again.");
                        }
                    }
                    else
                    {
                        WriteLine($"{team.TeamName} HAS NOT been DELETED");
                    }
                }
                else
                {
                    WriteLine($"There is no DevTeam with ID {userInputDevTeamID} to delete.");
                }
            }
            else
            {
                WriteLine("There aren't any avaiable developer teams.");
            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
        WaitKeyPress();
    }

    private void UpdateDevTeamInfo()
    {
        try
        {
            Clear();
            WriteLine("***Developer Team Listing***");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();
            if (dTeam.Count() > 0)
            {
                WriteLine("Please select a DevTeamID for update.");
                int userInputDevTeamID = int.Parse(ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamID);

                if (team != null)
                {
                    DeveloperTeam updatedTeamData = InitalizeDevTeamCreation();
                    if (_dTRepo.UpdateExistingDeveloperTeam(team.ID, updatedTeamData))
                    {
                        WriteLine($"Success, {team.TeamName} was updated!");
                    }
                    else
                    {
                        WriteLine("Fail, pleas try again.");
                    }
                }
                else
                {
                    WriteLine($"Sorry, {userInputDevTeamID} is an invalid ID.");
                }
            }
        }
        catch (System.Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
        WaitKeyPress();
    }

    private void DevsWoPSAccount()
    {
        Clear();
        List<Developer> devsWoPS = _dRepo.GetDevelopersWithoutPluralsight();
        if (devsWoPS.Count() > 0)
        {
            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("***Developers Without Pluralsight***");
            ResetColor();
            foreach (Developer dev in devsWoPS)
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("Every Developer has Pluralsight!");
            ResetColor();
        }
        WaitKeyPress();
    }

    private void AddMultiDevstoATeam()
    {
        try
        {
            Clear();
            WriteLine("***Developer Team Listing***");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();

            if (dTeam.Count() > 0)
            {
                WriteLine("Select DevTeam by ID.");
                int userInputDevTeamID = int.Parse(ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamID);

                List<Developer> auxDevsInDb = new List<Developer>();
                foreach (Developer auxDev in _dRepo.GetDevelopers())
                {
                    auxDevsInDb.Add(auxDev);
                }

                List<Developer> devsToAdd = new List<Developer>();

                if (team != null)
                {
                    bool hasFilledPositions = false;

                    while (!hasFilledPositions)
                    {
                        if (auxDevsInDb.Count() > 0)
                        {
                            Clear();
                            ForegroundColor = ConsoleColor.Magenta;
                            WriteLine("Avaiable developers to add:");
                            ResetColor();
                            DisplayDevsInDb(auxDevsInDb);
                            ForegroundColor = ConsoleColor.Magenta;
                            WriteLine($"Do you want to add a developer to {team.TeamName}? y/n");
                            ResetColor();
                            string userInputAnyDev = ReadLine()!.ToLower();

                            if (userInputAnyDev == "y")
                            {
                                ForegroundColor = ConsoleColor.Magenta;
                                WriteLine("Input Developer ID");
                                ResetColor();
                                int userInputDevID = int.Parse(ReadLine()!);
                                Developer dev = _dRepo.GetDeveloperbyID(userInputDevID);
                                bool devAlreadyAdded = false;

                                if (dev != null)
                                {
                                    foreach (Developer aDev in auxDevsInDb)
                                    {
                                        switch (aDev == dev)
                                        {
                                            case true:
                                                devAlreadyAdded = false;
                                                break;
                                            default:
                                                devAlreadyAdded = true;
                                                break;
                                        }
                                        if (!devAlreadyAdded)
                                        {
                                            break;
                                        }
                                    }
                                    if (!devAlreadyAdded)
                                    {
                                        devsToAdd.Add(dev);
                                        auxDevsInDb.Remove(dev);
                                    }
                                    else
                                    {
                                        ForegroundColor = ConsoleColor.Cyan;
                                        WriteLine($"You have already added {dev.FullName} ID: {dev.ID} to the team!");
                                        ResetColor();
                                        WaitKeyPress();
                                    }
                                }
                                else
                                {
                                    WriteLine($"The developer with ID {userInputDevID} does not exist.");
                                    WaitKeyPress();
                                }
                            }
                            else
                            {
                                hasFilledPositions = true;
                            }
                        }
                        else
                        {
                            WriteLine("There are no developers in the database.");
                            WaitKeyPress();
                            break;
                        }
                    }
                    if (_dTRepo.AddMultipleDevs(team.ID, devsToAdd))
                    {
                        Clear();
                        WriteLine("Success!\n");
                        foreach (Developer dev in devsToAdd)
                        {
                            WriteLine($"{dev.FullName} was added to {team.TeamName}.");
                        }
                    }
                    else
                    {
                        WriteLine("Fail.");
                    }
                }
                else
                {
                    WriteLine($"Sorry there is no DevTeam with ID {userInputDevTeamID}");
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            SomethingWentWrong();
        }
        WaitKeyPress();
    }

    private void WaitKeyPress()
    {
        WriteLine("Press any key to continue.");
        ReadKey();
    }


}