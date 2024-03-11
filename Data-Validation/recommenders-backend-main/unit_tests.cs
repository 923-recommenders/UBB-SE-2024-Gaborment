using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory2_Data_Validation_Module
{
    internal class unit_tests
    {

        private static bool testSanitizeSearchTerm()
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
            Console.WriteLine($"bad input: {badWord} , sanitized input: {sanitizedWord}");
            Console.WriteLine($"bad input: {badWord2} , sanitized input: {sanitizedWord2}");
            return true;

        }
        public static void test()
        {

            if (testSanitizeSearchTerm())
            {
                Console.WriteLine("tests passed!");
            }
            else
            {
                Console.WriteLine("error: tests did not pass.");
            }
        }
    }
}
