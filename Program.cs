string dir = Directory.GetCurrentDirectory();
try {
StreamReader sr = new StreamReader(dir + "\\ml-latest-small\\movies.csv");
Console.WriteLine("File found");
}
catch{
    Console.WriteLine("File not found");
}

