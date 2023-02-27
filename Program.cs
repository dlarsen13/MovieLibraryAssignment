string dir = Directory.GetCurrentDirectory();
List<string> movieData = new List<string>();
bool menuQuit = false;
int maxMovieID = 0;
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
        // Finding max id value to make adding movies easier later
        if (Convert.ToInt64(splitLine[0]) > maxMovieID){
            maxMovieID = (int)Convert.ToInt64(splitLine[0]);
        }
        movieData.AddRange(splitLine);
        // Adding a line break at the end of each entry to make formatting look nice when printing the list
        movieData.Add(Environment.NewLine);
    }
    sr.Close();
}
// catch block for file not found errors
catch (FileNotFoundException){
    Console.WriteLine("File not found");
    Console.WriteLine("Please enter the filepath to movies.csv");
    string filePath = Console.ReadLine();
    getFileData(filePath, movieData);
}
catch (IOException){
    Console.WriteLine("Error parsing file, restarting process");
    getFileData((dir + "\\ml-latest-small\\movies.csv"), movieData);
}
catch (Exception e){
    Console.WriteLine("Error parsing file. Please check your file path and try again");
}

// Printing the visible menu choices
while (!menuQuit){
    // Break line before printing the menu to fix a loop formatting bug
    Console.WriteLine($"\nPlease enter 1 to view the current entries of the movie library, 2 to add an entry, or anything else to exit");
    string userInput = Console.ReadLine();
    switch (userInput){
        case "1":
            foreach (string movie in movieData){
                Console.Write(movie + " ");
            }
            break;
        // TODO: Open StreamWriter to append data to file and list, check against list to ensure against duplicate values (After list formatting is corrected)
        case "2":
            try {
                // StreamWriter made and set to append
                StreamWriter sw = new StreamWriter(dir + "\\ml-latest-small\\movies.csv", true);
                // Grabbing the maxID value recorded while populating data and adding 1 to ensure unique ID value
                sw.WriteLine((maxMovieID + 1) + " ");
                Console.WriteLine("Please enter the movie's title");
                string movieTitle = Console.ReadLine();
                sw.Write(movieTitle + " ");
                // Also adding values to list to be viewed in printout
                movieData.Add(Convert.ToString(maxMovieID + 1) + " ");
                movieData.Add(movieTitle + " ");
                // Conditional checks for the while loop
                bool flag = true;
                int i = 0;
                while (flag){
                    Console.WriteLine("Please enter the genres of the movie. Enter nothing when done");
                    string movieGenre = Console.ReadLine();
                    // Forgot what ReadLine returns with an empty character input so I'm covering my bases
                    if (movieGenre == "" || movieGenre == null || movieGenre == " "){
                        flag = false;
                        break;
                    }
                    // Checking if it's the first genre added to ensure formatting consistancy in printout
                    if (i == 0){
                        sw.Write(movieGenre);
                        movieData.Add(movieGenre);
                    }
                    else{
                        sw.Write("|" + movieGenre);
                        movieData.Add("|" + movieGenre);
                    }
                }
                // New line added at the end to ensure formatting consistancy in printout
                sw.Write(Environment.NewLine);
            }
            catch (Exception e){
                Console.WriteLine("Error adding entry. Please try again");
            }
            break;
        default:
            menuQuit = true;
            break;

    }

}



// Method to parse data in catch block with recursion to ensure file is found before proceeding
static void getFileData(string filePath, List<String> list){
    bool fileFound = false;
    while (!fileFound){
        try {
        StreamReader sr = new StreamReader(filePath);
        // Reading through the first line to remove the formatting line at top of the file
        sr.ReadLine();
        while (sr.Peek() >= 0){
            // Reading current line of file and splitting on the comma to get all 3 pieces of data
            string currentLine = sr.ReadLine();
            string[] splitLine = currentLine.Split(",");
            // Checking if movieID provided by the .csv file already is in list to avoid duplicate entries
            if (list.Contains(splitLine[0])){
                continue;
            }
                list.AddRange(splitLine);
            }
            fileFound = true;
        }
        catch{
            Console.WriteLine("File still not found, please check the file path and try again");
        }
    }
}