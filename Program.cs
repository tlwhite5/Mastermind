

using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

Run();

void Run()
{
    string secretAnswer = GenerateSecretAnswer();

    Console.WriteLine(secretAnswer);

    Console.WriteLine("Welcome to Mastermind mock where you have to guess a secret passcode with 4 digits that each have a value between 1 and 6");
    Console.WriteLine("'+' means you've guess a digit correctly, '-' means you've guess a correct digit just in the wrong place, and if you are missing a + or - you didn't guess a digit correctly at all");
    bool correctAnswer = false;
    string error = "";
    string answerFixes = "";
    while (!correctAnswer)
    {
        Console.WriteLine("Please enter a 4 digit answer with the digit values being between 1 and 6");
        Console.WriteLine("or type 'Exit' to quit");

        string input = Console.ReadLine();
        if (input == "Exit")
        {
            Console.WriteLine("Exiting and thank you for playing");
            break;
        }

        error = CheckForError(input);
        if (error == "")
        {
            answerFixes = CheckUserAnswer(input, secretAnswer);
            Console.WriteLine(answerFixes);
            if (answerFixes == "++++")
            {
                correctAnswer = true;
                Console.WriteLine("Congratulations you have guess the secret answer. Thank you for playing");
            }
        }
        else
        {
            Console.WriteLine(error);
        }
    }
}

string GenerateSecretAnswer()
{
    Random rnd = new Random();
    string digit1 = "" + rnd.Next(1, 7);
    string digit2 = "" + rnd.Next(1, 7);
    string digit3 = "" + rnd.Next(1, 7);
    string digit4 = "" + rnd.Next(1, 7);

    return digit1 + digit2 + digit3 + digit4;
}

string CheckForError(string? input)
{
   string message = "";
   if (input.Length != 4)
   {
        message = "The amount of digits entered is more or less than 4.";
   }
   
   for (int i = 0; i < input.Length; i++)
   {
        if (int.TryParse(input[i].ToString(), out int value))
        {
            if (value > 6)
            {
                message = "In your guess you've entered a value greater than 6.";
            }
        }
        else
        {
            message = "the value entered was not a number with 4 digits.";
        }
   }

   return message;
}

string CheckUserAnswer(string? input, string secretAnswer)
{
    string gatheredOutput = "";
    int indexOfCharacter;
    for (int i = 0; i < input.Length; i ++)
    {
       if (input[i].ToString() == secretAnswer[i].ToString())
       {
            gatheredOutput += "+";
       }
       else if (secretAnswer.Contains(input[i]))
       {
            gatheredOutput+= "-";
       }
    }

    string sortedOutput = System.String.Concat(gatheredOutput.OrderBy(c => c));

    return sortedOutput;
}