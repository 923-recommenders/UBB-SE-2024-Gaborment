using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laboratory2_Data_Validation_Module
{
    internal class DataValidation
    {
        private static Regex badWordsRegularExpression;
        private static Regex emailRegularExpression;
        private static Regex phoneNumberRegularExpression;
        static DataValidation()
        ///constructor for a DataValidation object
        {
            List<string> badWords = File.ReadLines("C:\\Users\\maria\\Downloads\\Data-Validation\\recommenders-backend-main\\bad_words.txt").ToList();
            badWordsRegularExpression = new Regex(@"\b(" + string.Join("|", badWords.Select(Regex.Escape)) + @")\b", RegexOptions.IgnoreCase);
            emailRegularExpression = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            phoneNumberRegularExpression = new Regex( @"^\+?\d{10,}$");
        }

        private string SanitizeForBadWords(string inputFromUser)
        /// inputFromUser: string, the text the user types in the searchbar
        /// returns: a sanitized string that contains the original text without any bad words
        {
            if (string.IsNullOrEmpty(inputFromUser))
                throw new ArgumentNullException("error: the input is null.");
            else
            {
                string sanitizedInput = badWordsRegularExpression.Replace(inputFromUser, match => new string('*', match.Value.Length));
                return sanitizedInput;
            }
        }

        private string SanitizeForSqlInjection(string inputFromUser)
        /// inputFromUser: string, the text the user types in the searchbar
        /// returns: a string that contains the original text sanitized against sql injection
        {
            if (string.IsNullOrEmpty(inputFromUser))
                throw new ArgumentNullException("error: the input is null.");
            else
            {
                string pattern = @"[^0-9a-zA-Z* ]";
                string sanitizedInput = Regex.Replace(inputFromUser, pattern, " ");
                string[] sqlKeywords = { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "CREATE", "TRUNCATE" , "FROM", "TABLE"};

                string[] words = sanitizedInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < words.Length; i++)
                {
                    if (sqlKeywords.Contains(words[i].ToUpper()))
                    {
                        words[i] = new string('*', words[i].Length);
                    }
                }

                sanitizedInput = string.Join(" ", words);

                return sanitizedInput;
            }

        }

        public string SanitizeSearchTerm(string inputFromUser)
        /// inputFromUser: string, the text the user types in the searchbar
        /// returns: a sanitized version of the given input, protected against bad words and sql injection
        {
            inputFromUser = SanitizeForBadWords(inputFromUser);
            string finalSanitizedTerm = SanitizeForSqlInjection(inputFromUser);
            return finalSanitizedTerm;
        }

        public bool ValidateEmail(string inputFromUser)
        /// inputFromUser: string, the text the user types in the searchbar
        /// returns a boolean value: true if the input matches the format of an email, false if it doesnt
        {
            if (string.IsNullOrEmpty(inputFromUser))
                throw new ArgumentNullException("error: the input is null");
            else
                return emailRegularExpression.IsMatch(inputFromUser);
        }

        public bool ValidatePhoneNumber(string inputFromUser)
        /// inputFromUser: string, the text the user types in the searchbar
        /// returns a boolean value: true if the input matches the format of a phone number, false if it doesnt
        {
            if (string.IsNullOrEmpty(inputFromUser))
                throw new ArgumentNullException("error: the input is null");
            else
                return phoneNumberRegularExpression.IsMatch(inputFromUser);
        }


        static void Main()
        {
            DataValidation dataValidation = new DataValidation();
            unit_tests.Test();

            Console.WriteLine("This is an example of how the DataValidation class works.");
            Console.WriteLine();
            Console.WriteLine("Sanitize input from bad words:");

            string searchTerm2 = "I hate those badwords.";
            string sanitizedSearchTerm2 = dataValidation.SanitizeSearchTerm(searchTerm2);
            Console.WriteLine("intial input: " + searchTerm2 + " sanitized input: " + sanitizedSearchTerm2);

            string searchTerm5 = "This is a safe search term.";
            string sanitizedSearchTerm5 = dataValidation.SanitizeSearchTerm(searchTerm5);
            Console.WriteLine("intial input: " + searchTerm5 + " sanitized input: " + sanitizedSearchTerm5);

            Console.WriteLine();
            Console.WriteLine("Sanitize input from sql injections:");

            string searchTerm1 = "SELECT * FROM Users; DROP TABLE Users;";
            string sanitizedSearchTerm1 = dataValidation.SanitizeSearchTerm(searchTerm1);
            Console.WriteLine("intial input: " + searchTerm1 +" sanitized input: " + sanitizedSearchTerm1);

            
            string searchTerm3 = "1'; DROP TABLE Users;--";
            string sanitizedSearchTerm3 = dataValidation.SanitizeSearchTerm(searchTerm3);
            Console.WriteLine("intial input: " + searchTerm3 + " sanitized input: " + sanitizedSearchTerm3);

            string searchTerm4 = "I hate SELECT * FROM Users; DROP TABLE Users;";
            string sanitizedSearchTerm4 = dataValidation.SanitizeSearchTerm(searchTerm4);
            Console.WriteLine("intial input: " + searchTerm4 + " sanitized input: " + sanitizedSearchTerm4);

            Console.WriteLine();
            Console.WriteLine("Validate an email input format:");
            string email1 = "test@example.com";
            Console.WriteLine($"Email '{email1}' is valid: {dataValidation.ValidateEmail(email1)}");

            string email2 = "invalidemail@.com";
            Console.WriteLine($"Email '{email2}' is valid: {dataValidation.ValidateEmail(email2)}");

            Console.WriteLine();
            Console.WriteLine("Validate an phone number input format:");
            string phoneNumber1 = "1234567890";
            Console.WriteLine($"Phone number '{phoneNumber1}' is valid: {dataValidation.ValidatePhoneNumber(phoneNumber1)}");

            string phoneNumber2 = "12345";
            Console.WriteLine($"Phone number '{phoneNumber2}' is valid: {dataValidation.ValidatePhoneNumber(phoneNumber2)}");

         
        }

    }

    
}
