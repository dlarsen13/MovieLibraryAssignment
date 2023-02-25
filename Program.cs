string dir = Directory.GetCurrentDirectory();
List<string> movieData = new List<string>();
// Putting the reading of the data into a try block so it doesn't immediately crash if file is not found
try {
StreamReader sr = new StreamReader(dir + "\\ml-latest-small\\movies.csv");
// Reading through the first line to remove the formatting line at top of the file
sr.ReadLine();
while (sr.Peek() >= 0){
    // Reading current line of file and splitting on the comma to get all 3 pieces of data
    string currentLine = sr.ReadLine();
    string[] splitLine = currentLine.Split(",");
    // Checking if movieID provided by the .csv file already is in list to avoid duplicate entries
    if (movieData.Contains(splitLine[0])){
        continue;
    }
    movieData.AddRange(splitLine);
}
}
// catch block for file not found errors
catch (FileNotFoundException){
    Console.WriteLine("File not found");
    Console.WriteLine("Please enter the filepath to movies.csv");

}

// TODO - Make a method to parse the file (Same as what's in the catch block) so it can be repeated in catch block with user-inputted filepath
