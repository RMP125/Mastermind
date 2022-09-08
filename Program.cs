// Making mastermind, inkeeping with the expectations of the pre-interview assignment.
// It generates a random 4-digit integerand gives a user 12 guesses 
// with limited feedback (+ for right number right positions, - for right number wrong position) 
// to guess the generated number
public class Mastermind{
    // numberGenerator generates a 4-digit integer and returns it.
    public static int numberGenerator(){
        Random rand = new Random();
        return rand.Next(1000, 9999);
    }


    // numberChecker generates the feedback(+, -) for a key-guess pair.
    // Turns 4-digit ints into arrays of single digits, then compares the elements.
    public static string numberChecker(int key, int guess){
        int[] keyArr = new int[4];
        int[] guessArr = new int[4];
        string feedback = "'";
        for(int i = 0; i < 4; i++){
            keyArr[i] = key%10;
            key = key/10;
            guessArr[i] = guess%10;
            guess = guess/10;
        }
        // Looping through numbers to check if any are in the same place and same number, adding a + for each.
        // Then turning both numbers into -1 so that they won't be compared for same number different place
        for(int i = 0; i < 4; i++){
            if(keyArr[i] == guessArr[i]){
                feedback += "+ ";
                keyArr[i] = -1;
                guessArr[i] = -1;
            }
        }
        // Comparing each remaining element to each-other to check for right number wrong position
        for(int i = 0; i < 4; i++){
            if(keyArr[i] != -1){
                for(int j = 0; j < 4; j++){
                    if (keyArr[i] == guessArr[j]){
                        feedback += "- ";
                        keyArr[i] = -1;
                        guessArr[j] = -1;
                        // Found a match, don't have to keep looking for this one
                        // Technically don't need to set keyArr element to -1 since we won't be looking
                        // at it again, but I think it's more readable this way
                        break;
                    }
                }
            }
        }
        // Removing trailing space from feedback and returning completed feedback
        if(feedback.Length > 1){
            feedback = feedback.Remove(feedback.Length-1);
        }
        feedback +="'";
        return feedback;
    }


    // playGame is the main method for playing a game of mastermind.
    // The isTest input determines whether or not to run some preliminary tests and display the secret number.
    public static void playMM(int gennedNum, bool isTest){
        int numGuesses = 12;
        bool won = false;
        // Some basic tests to make sure everyone's running as planned.
        if(isTest){
            Console.WriteLine("In Mastermind testing...");
            Console.WriteLine("A randomly generated number is: " + numberGenerator());
            Console.WriteLine("The feedback for [1234, 4321] is: "+ numberChecker(1234, 4321));
            Console.WriteLine("The feedback for [1234, 1234] is: "+ numberChecker(1234, 1234));
            Console.WriteLine("The feedback for [1234, 5678] is: "+ numberChecker(1234, 5678));
            Console.WriteLine("The feedback for [1234, 3333] is: "+ numberChecker(1234, 3333));
            Console.WriteLine("The feedback for [3333, 1234] is: "+ numberChecker(3333, 1234));
            Console.WriteLine("This game's key is: " + gennedNum);
        }
        // Introduction
        Console.WriteLine("Welcome to Mastermind. In this game, you will have " + numGuesses + " chances to guess a secret 4-digit number.");
        Console.WriteLine("After each guess, you will receive a set of feedback including some '+' and '-' symbols.");
        Console.WriteLine("A '+' signifies that you have the right number in the right place, ");
        Console.WriteLine("and a '-' signifies that you have the right number in the wrong place. Good luck!");

        // Will loop while you have guesses available.
        // Takes input and checks to make sure it's a valid input.
        // Gives error if it isn't.
        // Continues until you win the game or run out of guesses.
        while(numGuesses > 0 && won == false){
            Console.WriteLine();
            Console.WriteLine("Please write your guess in the form of a 4-digit number.");
            string? inputGuess = Console.ReadLine();
            while(inputGuess == null){
                Console.WriteLine("You just submitted a null input, please try again.");
                inputGuess = Console.ReadLine();
            }
            // Converting input string into an integer, checking if integer is 4 digits, then 
            // Running it through numberChecker to get feedback
            try{
                int inputAsInt = Convert.ToInt32(inputGuess);
                // Making sure input is in the 4-digit range necessary for numberChecker
                if(inputAsInt/1000 > 0 && inputAsInt/10000 == 0){
                    string feedback = numberChecker(gennedNum, inputAsInt);
                    numGuesses--;
                    // Gives winning text if you win, otherwise gives feedback for next move
                    if(feedback == "'+ + + +'"){
                        won = true;
                        Console.WriteLine("Congratulations! you matched the hidden answer, " + gennedNum + ", with " + numGuesses + " guesses remaining.");
                        return;
                    }
                    else{
                        Console.WriteLine("Your feedback for your guess of "+ inputAsInt + " is: "+ feedback + ". " + 
                        "You have " + numGuesses + " guesses remaining.");
                        if(isTest){
                            Console.WriteLine("As a reminder, the key is: " + gennedNum);
                        }
                    }
                }
                // Gives error if not 4 digits long
                else{
                    Console.WriteLine("Your received number: "+ inputAsInt + " does not appear to be 4 digits long. Please try again.");
                }
            }
            // Gives error if not integer input
            catch(Exception){
                Console.WriteLine("There was a type error in your guess input: " + inputGuess + ". Please only use integers as guesses.");
            }
        }
        // Game ends once you run out of moves, this is just an exit text.
        Console.WriteLine("Unfortunately, you ran out of moves. Better luck next time!");
        return;
    }


    // Main method can open either the test mastermind or the actual game, as well as run multiple games consecutively.
    public static void Main(){
        Console.WriteLine("Welcome to Mastermind. Would you like to play?(yes, no, test)");
        string? userInput = Console.ReadLine();
        while(userInput == null){
            Console.WriteLine("You just submitted a null input, please try again.");
            userInput = Console.ReadLine();
        }
            userInput = userInput.ToLower();
        while(userInput != "no"){
            // checks user input (not case sensitive) and does appropriate action or gives error if not matching
            if(userInput == "yes"){
                playMM(numberGenerator(), false);
                Console.WriteLine("Thanks for playing! Would you like to play again?(yes, no, test)");
            }
            else if(userInput == "test"){
                playMM(numberGenerator(), true);
                Console.WriteLine("Daring today, aren't we?! Want to try again without the answers?(yes, no, test)");
            }
            else{
                Console.WriteLine("Sorry, I seemed to have trouble understanding you. Please try again. (Yes, no, test)");
            }
            userInput = Console.ReadLine();
            while(userInput == null){
                Console.WriteLine("You just submitted a blank line, please try again.");
                userInput = Console.ReadLine();
            }
            userInput = userInput.ToLower();    
        }
        Console.WriteLine("Thank you, goodbye!");
        return;
    }
}