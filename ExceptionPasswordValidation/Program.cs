using System;
using System.Text.RegularExpressions;
//validates a password to conform to the following rules:  Length between 6 and 24 characters. At least one uppercase letter (A-Z).
//At least one lowercase letter (a-z). At least one digit (0-9). Maximum of 2 repeated characters. "aa" is OK 👍 "aaa" is NOT OK 👎
//Supported special characters:
//! @ # $ % ^ & * ( ) + = _ - { } [ ] : ; " ' ? < > , .  Use exception handling and event to execute the program
namespace ExceptionPasswordValidation
{
    public delegate void PasswordEventHandler(string pw);
    class Password
    {
        public event PasswordEventHandler send;
        string password;
        public void GetInput(string password)
        {
            this.password = password;
            Notify();
        }
        public void Notify()
        {
            if(send != null)
            {
                send(password);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Password password = new Password();
            Console.WriteLine("Enter password:");                     
            try
            {
                string input = Console.ReadLine();
                password.send += Password_send;
                password.GetInput(input);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception handled: " + ex.Message);
            }

        }
        private static void Password_send(string pw)
        {
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,24}");
            var hasNumber = new Regex(@"[0-9]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            if (!hasLowerChar.IsMatch(pw))
            {
                Console.WriteLine("Password should contain at least one lower case letter");
            }
            else if (!hasUpperChar.IsMatch(pw))
            {
                Console.WriteLine("Password should contain at least one upper case letter");
            }
            else if (!hasMiniMaxChars.IsMatch(pw))
            {
                Console.WriteLine("Password should be between 6 and 24 characters");
            }
            else if (!hasNumber.IsMatch(pw))
            {
                Console.WriteLine("Password should contain at least one number");
            }
            else if (!hasSymbols.IsMatch(pw))
            {
                Console.WriteLine("Password should contain at least one special case characters");
            }
            else
            {
                Console.WriteLine("Password success!");
            }
        }
    }
}
