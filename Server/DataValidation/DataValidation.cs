using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.DataValidation
{
    internal class DataValidation
    {
        private static Regex badWordsRegularExpression;
        private static Regex emailRegularExpression;
        private static Regex phoneNumberRegularExpression;
        private readonly string badWordsFilePath;
        public DataValidation()
        ///constructor for a DataValidation object
        {
            badWordsFilePath = GenerateDefaultFilePath();
            
            if (!File.Exists(badWordsFilePath))
            {
              
                List<string> badWords = new List<string> { "badword1", "badword2", "badword3" };

                
                try
                {
                    using (StreamWriter sw = new StreamWriter(badWordsFilePath))
                    {
                        foreach (var badWord in badWords)
                        {
                            sw.WriteLine(badWord);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            List<string> badWordsList = File.ReadLines(badWordsFilePath).ToList();

            badWordsRegularExpression = new Regex(@"\b(" + string.Join("|", badWordsList.Select(Regex.Escape)) + @")\b", RegexOptions.IgnoreCase);
            emailRegularExpression = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            phoneNumberRegularExpression = new Regex(@"^\+?\d{10,}$");
        }

        private string GenerateDefaultFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BadWords.txt");
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
                string[] sqlKeywords = { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "CREATE", "TRUNCATE", "FROM", "TABLE" };

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
    }
}


