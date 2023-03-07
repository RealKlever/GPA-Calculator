Dictionary<string, int> creditHours = new Dictionary<string, int>();
Dictionary<string, string> grade = new Dictionary<string, string>();

double GPA = 0;
int totalCreditHours = 0;
string answer = "";

/*
 * Can comment out the three lines of code below and replace "fileName" in string[] fileContents
 * with the location of the file. This will make the program easier to use
 * if it is planned to be used over and over.
 */
string fileName = "";
Console.Write("File Location: ");
fileName = Console.ReadLine();

string[] fileContents = System.IO.File.ReadAllLines(fileName);

// Adds the information from the file into the Dictionaries
for(int i = 0; i < fileContents.Length; i++)
{
    if (!(fileContents[i].Equals("")))
    {
        creditHours.Add(getClass(fileContents[i]), getCreditHours(fileContents[i]));
        grade.Add(getClass(fileContents[i]), getGrade(fileContents[i]));
    }
}

// Adds up all the classes and credit hours together
for(int i = 0; i < fileContents.Length; i++)
{
    if (!(fileContents[i].Equals("")) && !(getGrade(fileContents[i]).Equals("X")))
    {
        GPA += (creditHours[getClass(fileContents[i])] * gradeToNum(grade[getClass(fileContents[i])]));
        totalCreditHours += creditHours[getClass(fileContents[i])];
    }
}

GPA = GPA / totalCreditHours;

Console.WriteLine("\nYour GPA is currently a " + GPA);

Console.Write("\nWould you like to get the information for a specific class? (Y/N): ");
answer = Console.ReadLine().ToLower();

// Constantly asks if the user wants the grade of a specific class
while (answer.Equals("y") || answer.Equals("yes"))
{
    Console.Write("What class: ");
    answer = Console.ReadLine().ToLower();

    try
    {
        Console.WriteLine("Grade: " + grade[answer]);
        Console.WriteLine("Credit Hours: " + creditHours[answer]);
    }
    catch (Exception)
    {
        Console.WriteLine("Class not spelled correctly or not found");
    }

    Console.Write("\nWould you like another classes information? (Y/N): ");
    answer = Console.ReadLine().ToLower();
}

// Returns class name
static string getClass(string line)
{
    int firstColon = line.IndexOf(':'); // Gets the index of the first colon
    int firstComma = line.IndexOf(','); // Gets the index of the first comma
    int length = (firstComma - firstColon) - 2; // Gets length needed to obtain class name

    return line.Substring(firstColon + 2, length).ToLower();
}


// Returns class credit hours
static int getCreditHours(string line)
{
    int creditHourIndex = line.IndexOf(',') + 16; // Gets the index of the credit hours
    int creditHour = Int32.Parse(line.Substring(creditHourIndex, 1)); // Gets credit hours and changes it to string
    
    return creditHour;
}


// Returns the grade
static string getGrade(string line)
{
    int lastColon = line.LastIndexOf(':'); // Gets the index of the last colon
    string grade = line.Substring(lastColon + 2);
    
    return grade;
}

// Returns the grade as a number
static int gradeToNum(string grade)
{
    int gradeToNum = 0;

    switch (grade)
    {
        case "A": gradeToNum = 4; break;
        case "B": gradeToNum = 3; break;
        case "C": gradeToNum = 2; break;
        case "D": gradeToNum = 1; break;
        case "F": gradeToNum = 0; break;
        case "X": gradeToNum = -1; break;
        default: gradeToNum = -1; break;
    }

    return gradeToNum;
}
