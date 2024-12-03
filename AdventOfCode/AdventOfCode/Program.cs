using System.Text.RegularExpressions;

const string pathData = "C:\\Users\\user\\Documents\\GitHub\\AdventOfCode2024\\dataset";
/////////////////////////////////////////////////////////////////// 1.1 ///////////////////////////////////////////////

List<int> listOne = new List<int>();
List<int> listTwo = new List<int>();
int diff = 0;

var lines = File.ReadLines(pathData + "\\dataset_one.txt");
foreach (var line in lines)
{
    string[] numbers = line.Split(' ');
    listOne.Add(Convert.ToInt32(numbers[0]));
    listTwo.Add(Convert.ToInt32(numbers[3]));
}

while (listOne.Count > 0)
{
    int numberListOne = listOne.Min();
    int numberListTwo = listTwo.Min();

    diff += Math.Abs(numberListOne - numberListTwo);
    listOne.Remove(numberListOne);
    listTwo.Remove(numberListTwo);
}

Console.WriteLine("1. Part one: " + diff);
/////////////////////////////////////////////////////////////////// 1.2 ///////////////////////////////////////////////
int diff2 = 0;
listOne.Clear();
listTwo.Clear();
foreach (var line in lines)
{
    string[] numbers = line.Split(' ');
    listOne.Add(Convert.ToInt32(numbers[0]));
    listTwo.Add(Convert.ToInt32(numbers[3]));
}
while (listOne.Count > 0)
{
    int numberList = listOne.Min();
    int two = listTwo.Where(x => x == numberList).Count();
    diff2 += two * numberList;
    listOne.Remove(numberList);
}
Console.WriteLine("1. Part two: " + diff2);

/////////////////////////////////////////////////////////////////// 2.1 ///////////////////////////////////////////////
lines = File.ReadLines(pathData + "\\dataset_two.txt");
int sumGoodReport = 0;

foreach (var line in lines)
{
    int[] numbers = Array.ConvertAll(line.Split(' '), v => int.Parse(v));
    if (GoodReport(numbers))
    {
        sumGoodReport++;
    }
}
bool GoodReport(int[] numbers)
{
    bool isAug = true;
    if (numbers[0] - numbers[1] > 0) isAug = false;

    for (int i = 0; i < numbers.Length - 1; i++)
    {
        if (Math.Abs(numbers[i] - numbers[i + 1]) < 1 || Math.Abs(numbers[i] - numbers[i + 1]) > 3 || (isAug && numbers[i] - numbers[i + 1] > 0) || (!isAug && numbers[i] - numbers[i + 1] < 0))
        {
            return false;
        }
    }

    return true;
}

Console.WriteLine("\n2. Part one: " + sumGoodReport);

/////////////////////////////////////////////////////////////////// 2.2 ///////////////////////////////////////////////
sumGoodReport = 0;
List<bool> test = new List<bool>();
foreach (var line in lines)
{
    List<int> numbers = line
     .Split(' ')
     .Select(s => int.TryParse(s, out int n) ? (int?)n : null) // Convertit en nullable
     .Where(n => n.HasValue) // Filtrer les nulls
     .Select(n => n.Value)  // Extraire les valeurs
     .ToList();
    if (GoodReporDampener(numbers, true))
    {
        sumGoodReport++;
    }
}

bool GoodReporDampener(List<int> numbers, bool firstTime)
{
    bool isAug = true;
    if (numbers[0] - numbers[1] > 0) isAug = false;

    for (int i = 0; i < numbers.Count - 1; i++)
    {
        if (Math.Abs(numbers[i] - numbers[i + 1]) < 1 || Math.Abs(numbers[i] - numbers[i + 1]) > 3 || (isAug && numbers[i] - numbers[i + 1] > 0) || (!isAug && numbers[i] - numbers[i + 1] < 0))
        {
            
            if (firstTime)
            {
                for(int j = 0; j < numbers.Count(); j++)
                {
                    List<int> numbers2 = new List<int>(numbers);
                    numbers2.RemoveAt(j);
                    if (GoodReporDampener(numbers2, false))
                    {
                        return true;
                    }
                }

                
            }
            return false;
        }
    }
    return true;
}
Console.WriteLine("2. Part two: " + sumGoodReport);

/////////////////////////////////////////////////////////////////// 3.1 ///////////////////////////////////////////////
Regex regex = new Regex(@"mul\(\d+,\d+\)");
long result = 0;

lines = File.ReadLines(pathData + "\\dataset_three.txt");
foreach (var line in lines)
{
    MatchCollection match = regex.Matches(line);
    for (int i = 0; i < match.Count; i++)
    {
        if (match[i] != null)
        {
            Regex getNumbers = new Regex(@"\d+");
            MatchCollection numbers = getNumbers.Matches(match[i].ToString());
            result += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
        }
        
    }
}

Console.WriteLine("\n3. Part one: " + result);

/////////////////////////////////////////////////////////////////// 3.2 ///////////////////////////////////////////////
regex = new Regex(@"mul\(\d+,\d+\)|do\(\)|don't\(\)");
bool isActivate = true;
result = 0;
foreach (var line in lines)
{
    MatchCollection match = regex.Matches(line);
    for (int i = 0; i < match.Count; i++)
    {
        if (match[i] != null)
        {
            if (match[i].Value == "do()") 
            {
                isActivate = true;
            }
            else if (match[i].Value == "don't()")
            {
                isActivate = false;
            }
            else if (isActivate)
            {
                Regex getNumbers = new Regex(@"\d+");
                MatchCollection numbers = getNumbers.Matches(match[i].ToString());
                result += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
            }
        }

    }
}

Console.WriteLine("3. Part two: " + result);
Console.ReadLine();

