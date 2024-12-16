using System.Text.RegularExpressions;

const string pathData = "C:\\Users\\pd51pyb\\Documents\\GitHub\\AdventOfCode2024\\dataset";
int total = 0;
var lines = File.ReadLines(pathData + "\\dataset_one.txt");
daySix();
Console.ReadLine();

/////////////////////////////////////////////////////////////////// 1.1 ///////////////////////////////////////////////
void dayOne()
{
    List<int> listOne = new List<int>();
    List<int> listTwo = new List<int>();
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

        total += Math.Abs(numberListOne - numberListTwo);
        listOne.Remove(numberListOne);
        listTwo.Remove(numberListTwo);
    }

    Console.WriteLine("1. Part one: " + total);
    /////////////////////////////////////////////////////////////////// 1.2 ///////////////////////////////////////////////
    total = 0;
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
        total += two * numberList;
        listOne.Remove(numberList);
    }

    Console.WriteLine("1. Part two: " + total);
}


/////////////////////////////////////////////////////////////////// 2.1 ///////////////////////////////////////////////
void dayTwo()
{
    lines = File.ReadLines(pathData + "\\dataset_two.txt");
    total = 0;

    foreach (var line in lines)
    {
        int[] numbers = Array.ConvertAll(line.Split(' '), v => int.Parse(v));
        if (GoodReport(numbers))
        {
            total++;
        }
    }

    bool GoodReport(int[] numbers)
    {
        bool isAug = true;
        if (numbers[0] - numbers[1] > 0) isAug = false;

        for (int i = 0; i < numbers.Length - 1; i++)
        {
            if (Math.Abs(numbers[i] - numbers[i + 1]) < 1 || Math.Abs(numbers[i] - numbers[i + 1]) > 3 ||
                (isAug && numbers[i] - numbers[i + 1] > 0) || (!isAug && numbers[i] - numbers[i + 1] < 0))
            {
                return false;
            }
        }

        return true;
    }

    Console.WriteLine("\n2. Part one: " + total);

/////////////////////////////////////////////////////////////////// 2.2 ///////////////////////////////////////////////
    total = 0;
    List<bool> test = new List<bool>();
    foreach (var line in lines)
    {
        List<int> numbers = line
            .Split(' ')
            .Select(s => int.TryParse(s, out int n) ? (int?)n : null) // Convertit en nullable
            .Where(n => n.HasValue) // Filtrer les nulls
            .Select(n => n.Value) // Extraire les valeurs
            .ToList();
        if (GoodReporDampener(numbers, true))
        {
            total++;
        }
    }

    bool GoodReporDampener(List<int> numbers, bool firstTime)
    {
        bool isAug = true;
        if (numbers[0] - numbers[1] > 0) isAug = false;

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (Math.Abs(numbers[i] - numbers[i + 1]) < 1 || Math.Abs(numbers[i] - numbers[i + 1]) > 3 ||
                (isAug && numbers[i] - numbers[i + 1] > 0) || (!isAug && numbers[i] - numbers[i + 1] < 0))
            {
                if (firstTime)
                {
                    for (int j = 0; j < numbers.Count(); j++)
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

    Console.WriteLine("2. Part two: " + total);
}


/////////////////////////////////////////////////////////////////// 3.1 ///////////////////////////////////////////////
void dayThree()
{
    Regex regex = new Regex(@"mul\(\d+,\d+\)");
    total = 0;

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
                total += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
            }
        }
    }

    Console.WriteLine("\n3. Part one: " + total);

/////////////////////////////////////////////////////////////////// 3.2 ///////////////////////////////////////////////
    regex = new Regex(@"mul\(\d+,\d+\)|do\(\)|don't\(\)");
    bool isActivate = true;
    total = 0;
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
                    total += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
                }
            }
        }
    }

    Console.WriteLine("3. Part two: " + total);
}


/////////////////////////////////////////////////////////////////// 4.1 ///////////////////////////////////////////////
void dayFours()
{
    lines = File.ReadLines(pathData + "\\dataset_four.txt");
    total = 0;
    char[,] values;
    values = new char[lines.Count(), lines.First().Length];
    for (int i = 0; i < lines.Count(); i++)
    {
        string line = lines.Skip(i).First();
        for (int j = 0; j < line.Length; j++)
        {
            values[i, j] = line[j];
        }
    }

    for (int i = 0; i < lines.Count(); i++)
    {
        for (int j = 0; j < lines.First().Length; j++)
        {
            if (values[i, j] == 'X')
            {
                if (IfXams(i, j, 1, 1, "XMAS", 1)) total++;
                if (IfXams(i, j, -1, -1, "XMAS", 1)) total++;
                if (IfXams(i, j, -1, 1, "XMAS", 1)) total++;
                if (IfXams(i, j, 1, -1, "XMAS", 1)) total++;
                if (IfXams(i, j, 0, 1, "XMAS", 1)) total++;
                if (IfXams(i, j, 0, -1, "XMAS", 1)) total++;
                if (IfXams(i, j, 1, 0, "XMAS", 1)) total++;
                if (IfXams(i, j, -1, 0, "XMAS", 1)) total++;
            }
        }
    }

    Console.WriteLine("\n4. Part one: " + total);
/////////////////////////////////////////////////////////////////// 4.2 ///////////////////////////////////////////////
    total = 0;
    for (int i = 0; i < lines.Count(); i++)
    {
        for (int j = 0; j < lines.First().Length; j++)
        {
            if (values[i, j] == 'M')
            {
                if (IfXams(i, j, 1, 1, "AS", 0))
                {
                    if (values[i, j + 2] == 'M')
                    {
                        if (IfXams(i, j + 2, 1, -1, "AS", 0)) total++;
                    }

                    if (values[i + 2, j] == 'M')
                    {
                        if (IfXams(i + 2, j, -1, 1, "AS", 0)) total++;
                    }
                }

                if (IfXams(i, j, 1, -1, "AS", 0))
                {
                    if (values[i + 2, j] == 'M')
                    {
                        if (IfXams(i + 2, j, -1, -1, "AS", 0)) total++;
                    }
                }

                if (IfXams(i, j, -1, 1, "AS", 0))
                {
                    if (values[i, j + 2] == 'M')
                    {
                        if (IfXams(i, j + 2, -1, -1, "AS", 0)) total++;
                    }
                }
            }
        }
    }

    bool IfXams(int x, int y, int xModifier, int yModifier, string letterToFound, int placeLetter)
    {
        if (placeLetter == letterToFound.Length)
        {
            return true;
        }

        try
        {
            if (values[x + xModifier, y + yModifier] == letterToFound[placeLetter])
            {
                if (IfXams(x + xModifier, y + yModifier, xModifier, yModifier, letterToFound, placeLetter + 1))
                {
                    return true;
                }
            }
        }
        catch
        {
            return false;
        }

        return false;
    }

    Console.WriteLine("4. Part two: " + total);
}

void daySix()
{
    lines = File.ReadLines(pathData + "\\dataset_six.txt");
    total = 0;
    int direction = 0;
    char[,] values = new char[lines.Count(), lines.First().Length];
    int x = 0;
    int y = 0;
    bool isFhinished = false;

    for (int i = 0; i < lines.Count(); i++)
    {
        string line = lines.Skip(i).First();
        for (int j = 0; j < lines.First().Length; j++)
        {
            if (line[j] == '^')
            {
                x = j;
                y = i;
            }

            values[i, j] = line[j];
        }
    }

    for (int i = 0; !isFhinished; i++)
    {
        switch (direction)
        {
            // up
            case 0:
                if (values[y - i, x] == '#')
                {
                    values[y - i + 1, x] = 'r';
                    direction = 1;
                    y -= i - 1;
                    i = 0;
                }
                else
                {
                    //printCircuit(y - i, x);
                    if (values[y - i, x] != '.')
                    {
                        canBlock(y - i, x);
                    }
                    else
                    {
                        for (int j = x; j < lines.First().Length; j++)
                        {
                            if (values[y - i, j] == 'r')
                            {
                                total += 1;
                                break;
                            }
                        }
                    }
                    values[y - i, x] = 'u';
                    //total += 1;
                }

                break;
            // right
            case 1:
                if (values[y, x + i] == '#')
                {
                    values[y, x + i - 1] = 'd';
                    direction = 2;
                    x += i - 1;
                    i = 0;
                }
                else
                {
                    //printCircuit(y,i + x);
                    if (values[y, x + i] != '.')
                    {
                        canBlock(y, x + i);
                    }
                    else
                    {
                        for (int j = y; j < lines.First().Length; j++)
                        {
                            if (values[j, x + i] == 'd')
                            {
                                total += 1;
                                break;
                            }
                        }
                    }
                    values[y, x + i] = 'r';
                    //total += 1;
                }

                break;
            // down
            case 2:
                if (values[y + i, x] == '#')
                {
                    values[y + i - 1, x] = 'l';
                    direction = 3;
                    y += i - 1;
                    i = 0;
                }
                else
                {
                    //printCircuit(y + i, x);
                    if (values[y + i, x] != '.')
                    {
                        canBlock(y + i, x);
                    }
                    else
                    {
                        for (int j = x; j > 0; j--)
                        {
                            if (values[y + i, j] == 'l')
                            {
                                total += 1;
                                break;
                            }
                        }
                    }
                    values[y + i, x] = 'd';
                    //total += 1;
                }

                break;
            // left
            case 3:
                if (values[y, x - i] == '#')
                {
                    values[y, x - i + 1] = 'u';
                    direction = 0;
                    x -= i - 1;
                    i = 0;
                }
                else
                {
                    //printCircuit(y,x - i);
                    if (values[y, x - i] != '.')
                    {
                        canBlock(y, x - i);
                    }
                    else
                    {
                        for (int j = y; j > 0; j--)
                        {
                            if (values[j, x-i] == 'u')
                            {
                                total += 1;
                                break;
                            }
                        }
                    }
                    values[y, x - i] = 'l';
                    //total += 1;
                }
                
                break;
        }

        if ((x + i + 1 > lines.First().Length + 1 && direction == 1) || (x - i - 1 < 0 && direction == 3) ||
            (y + i + 1 > lines.Count() - 1 && direction == 2) || (y - i - 1 < 0 && direction == 0)) isFhinished = true;
    }

    Console.WriteLine("6. Part one: " + total);

    void printCircuit(int y, int x)
    {
        Console.Clear();
        Console.WriteLine(
            "....#.....\n.........#\n..........\n..#.......\n.......#..\n..........\n.#..^.....\n........#.\n#.........\n......#...");
        Console.SetCursorPosition(x, y);
        Console.Write("0");
    }

    void canBlock(int y, int x)
    {
        switch (values[y, x])
        {
            case 'r':
                if (direction == 0) total += 1;
                break;
            case 'd':
                if (direction == 1) total += 1;
                break;
            case 'l':
                if (direction == 2) total += 1;
                break;
            case 'u':
                if (direction == 3) total += 1;
                break;
        }
    }
}