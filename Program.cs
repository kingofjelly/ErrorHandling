using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ErrorHandling
{
    class Program
    {

        public static string G_errorMessage;//this is a global variable which will be used to hold the error message, opposed to passing it down all the layers
        //look into public, static, private
        //incorporate enums
        //figure out procedure to hook in my exception method

        static void Main(string[] args)
        {
            //this is an application i'm making in order to handle exceptions and relay to an error file. if the file doesnt exist, it will be created
            getUserInput();//run method below
        }

        public static void getUserInput()
        {
            //1: Ask question, then get user input. Wrong data will intentionally be entered, to trip an error and look at error handling. Investigate try.parse too and try catch.
            int userNumber;//for user age. will be entering strings to trip and error
            string userInput;//used for any parsing needed.

            exceptionEncounteredHandling errorHandling = new exceptionEncounteredHandling();
            
            Console.WriteLine("enter a numerical number");
            userInput = Console.ReadLine();
           
                //wrap this within try parse/try catch?
                try
                {
                    
                    userNumber = int.Parse(userInput);
                    Console.WriteLine("The number is: " + userNumber + "?\nIs this correct?");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());//this needs to be passed down the multiple layers OR stored globally. globally is probably best bet
                    G_errorMessage = ex.Message.ToString();
                    Console.WriteLine("Error collecting your input. Will attempt to write to log file");
                    //this is where it calls a seperate method nested here. it will check for a folder in the default location. if not create it and state whether or not it has

                    //run the error class here. pass in the error message.

                    errorHandling.handleErrors(ex.Message.ToString());//call my error handling class
                    
                }

              
            
        }

       
    }
}
