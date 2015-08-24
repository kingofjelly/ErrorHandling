using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ErrorHandling
{
    class exceptionEncounteredHandling
    {
        /* This is a special method i've created, to be able to interact with it in other applications. 
         * This method is used to handle exceptions and write to a log file.
         * The aim of this is to be able to export it to whatever application I make
         * It will be continually tweaked and developed in order to handle exceptions according to their type.         
         */


        /*
         * Goals of this class:
         * -Use enmums
         * -Use boolean triggers
         * -Investigate and use if possible try catch + throw (bubble up)
         * -Enable for general use in future applications
         * -Eventually package to .DLL and test?
         * -Try switching
         */
        //private string G_errorMessage;//global variable, formed from string parsed from method calling this class

        private bool G_folderPresent = false;//boolean to be tripped if folder is present. Static, as after set, it will remain until program rebooted. Not reset each time method called.

        private bool G_filePresent = false;//boolean to be tripped if file is present. Static, as after set, it will remain until program rebooted. Not reset each time method called.

        /*This is a global variable which will be used to hold the error message, opposed to passing it down all the layers
         *If it was the same error message for everything, it would be static. We just want it accessible in this method, however it isn't constant          
         *That means it isn't static.
         */

       

        

        public void handleErrors(string errorMessage)
        {
            /*This method is the one which kicks off the error handling. It will be called, after being istantiated through the parent class & method, then
             *the error message is passed down, into this method, where it's then stored in a global variable.
             *This global variable is then used throughout.
             */

            //G_errorMessage = errorMessage;//set the parsed variable to the global one.

            //MAKE A CONSTRUCTOR CLASS. RUNS THIS EVERY TIME, SO THAT BOOLEANS ARE PRE-SET?

            if (G_folderPresent && G_filePresent)
            {
                //as both are present, skip to method which writes to file.
                writeToErrorLog(errorMessage);//pass in errorMessage. skip global
            }
            else
            {
                checkForFolder(errorMessage);//pass in errorMessage. skip global
                /*if not. check for folder and work down tree to check whether file is there. Create what's missing, then set booleans showing they're there, to bypass
                 *this upon the next run.
                 */
            }
           


        }

        private void checkForFolder(string errorMessage)
        {
            //check default area for folder to store error log in. if not, create one.
            //give feedback to user as to whether one exists or has been created.

            //IF DIRECTORY EXISTS, INFORM USER FOR TEST PURPOSES. IF DOESN'T EXIST, OUTPUT RELEVANT MESSAGE.
            if (Directory.Exists("C:\\tmp"))
            {
                Console.WriteLine("Error Folder Exists");
                //NOW GO INTO FOLDER LOOKING FOR RELEVANT ERROR FILE
                G_folderPresent = true;
                //checkForErrorLog();//don't need this. writing creates if not present.
                writeToErrorLog(errorMessage);
            }
            else
            {
                Console.WriteLine("Doesn't Exist, \nCreating folder for you now");
                createFolder();
                //CREATE FOLDER
                //createErrorLog();
                //you don't need to check for the log, as it's not there. no folder = no file under that directory.
                writeToErrorLog(errorMessage);//by this point both booleans should be true
            }
        }

        private void createFolder()
        {
            //this method creates the folder for the user.
            try
            {
                //try creating folder for user
                Directory.CreateDirectory("C:\\tmp");
                Console.WriteLine("Error Directory Created");
                G_folderPresent = true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine("Error Creating Error Directory Folder");
                //error
            }
        }
        //THE TWO METHODS BELOW ARE NOT NEEDED. LOGS ARE AUTOMATICALLY CREATED
        //private void checkForErrorLog()
        //{
        //    //this method is used for check whether the error log file exists. this will be called if the folder exists. if it doesn't file will be assumed missing and created.
        //    //need to check if it exists. if so is it read only/open? if so throw error. explaining situation
        //    if (File.Exists(@"C:\tmp\errors.txt"))
        //    {
        //        Console.WriteLine("Log file exists. Will now write to it");
        //        G_filePresent = true;
                
                
        //    }
        //    else
        //    {
        //        Console.WriteLine("Log file not present. Now Creating it");
        //        createErrorLog();
        //    }

        //}

        //public static void createErrorLog()
        //{
        //    //this method will create the error log. it will auto create it if it's creating the tmp folder, as it won't be present. It will also create it if it has examined
        //    //the tmp folder and found it to be not present.
        //    string path = @"C:\tmp\errors.txt";

        //    try
        //    {
        //        TextWriter tw = new StreamWriter(path, true);
        //        // tw.WriteLine("This is an error line!");
        //        tw.Close();
        //        // Console.WriteLine("written to error log");
        //        Console.WriteLine("Error file created");

        //        G_filePresent = true;
                

        //        //write to it here
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error creating error log file");
        //        Console.WriteLine(ex.Message.ToString());

        //    }
        //}

        private static void writeToErrorLog(string errorMessage)
        {
            //writes to the rror log, created above.
            string path = @"C:\tmp\errors.txt";
            string timeNow = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            try
            {
                using (FileStream fs = new FileStream(path,FileMode.Append,FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //format entry to log file here.
                    //I want Error + error message + Date/Time stamp
                    sw.WriteLine("Error:        " + errorMessage + "      " + timeNow);
                }
                Console.WriteLine("Successfully written to error log file");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered writing to error file. Oh the irony.");
                
                Console.WriteLine(ex.Message.ToString());


            }
        }
    }
}
