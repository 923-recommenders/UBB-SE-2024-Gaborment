using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory2_Data_Validation_Module
{
    internal class unit_tests
    {

        private static bool TestSanitizeSearchTerm()
        {
            DataValidation validate = new DataValidation();
            string badWord = "bad1'--";
            string sanitizedWord = validate.SanitizeSearchTerm(badWord);
            if (sanitizedWord.Contains("bad1"))
            {
                Console.WriteLine("error in funtion SanitizeForBadWords()!");
                return false;
            }

            if (sanitizedWord.Contains("'") || sanitizedWord.Contains("--"))
            {
                Console.WriteLine("error in function SanitizeForSqlInjection()!");
                return false;
            }
            string badWord2 = "bad1 good bad2 -- CREATE";
            string sanitizedWord2 = validate.SanitizeSearchTerm(badWord2);
            if (sanitizedWord2.Contains("bad1") || sanitizedWord2.Contains("bad2"))
            {
                Console.WriteLine("error in funtion SanitizeForBadWords()!");
                return false;
            }
            if (sanitizedWord2.Contains("--") || sanitizedWord2.Contains("CREATE"))
            {
                Console.WriteLine("error in function SanitizeForSqlInjection()!");
                return false;
            }
            
            return true;

        }

        private static bool TestValidateEmail()
        {
            DataValidation validate = new DataValidation();
            string validEmail = "test@example.com";
            string invalidEmail = "invalidemail@.com";

            if (!validate.ValidateEmail(validEmail))
            {
                Console.WriteLine($"error: '{validEmail}' is a valid email!");
                return false;
            }

            if (validate.ValidateEmail(invalidEmail))
            {
                Console.WriteLine($"error: '{invalidEmail}' is an invalid email!");
                return false;
            }

            return true;
        }

        private static bool TestValidatePhoneNumber()
        {
            DataValidation validate = new DataValidation();
            string validPhoneNumber = "1234567890";
            string invalidPhoneNumber = "12345";

            if (!validate.ValidatePhoneNumber(validPhoneNumber))
            {
                Console.WriteLine($"error: '{validPhoneNumber}' is a valid phone number!");
                return false;
            }

            if (validate.ValidatePhoneNumber(invalidPhoneNumber))
            {
                Console.WriteLine($"error: '{invalidPhoneNumber}' is an invalid phone number!");
                return false;
            }

            return true;
        }
        public static void Test()
        {
            bool allTestsPassed = true;

            if (!TestSanitizeSearchTerm())
            {
                allTestsPassed = false;
            }

            if (!TestValidateEmail())
            {
                allTestsPassed = false;
            }

            if (!TestValidatePhoneNumber())
            {
                allTestsPassed = false;
            }

            if (allTestsPassed)
            {
                Console.WriteLine("All tests passed!");
            }
            else
            {
                Console.WriteLine("Error: Some tests did not pass.");
            }
        }
        }
}
