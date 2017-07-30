using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Utilities
{
    public class AppConstants
    {
        public static string ERROR_MESSAGE_ALL_FIELDS_REQUIRED = "All fields are mandatory.";
        public static string ERROR_MESSAGE_USER_ALREADY_EXIST = "User already exist.Please Sign In.";
        public static string ERROR_MESSAGE_USER_SIGN_IN_FAILED = "Sign In Failed.You are not registered with us.";
        public static string ERROR_MESSAGE_EMAIL_VALIDATION = "Please enter a valid email address.";

        public enum Languages { English, Dutch, German, French, Italian, Korean, Polish, Russian, Turkish, Spanish };

        public static List<string> DEFAULT_DICTIONARY_KEYS = new List<string>() { "Vegetable", "Steak", "Bag of crisps", "Cabbage", "Carrot" };
        public static List<string> DEFAULT_DICTIONARY_VALUES = new List<string>() { "Groente", "Biefstuk", "Zak van chips","Kool", "Wortel" };
        public static string DEFAULT_FROM_LANGUAGE = Languages.English.ToString();
        public static string DEFAULT_TO_LANGUAGE = Languages.Dutch.ToString();

        public const string TEST_TYPE_TYPE_ALONG = "Type Along";
        public const string TEST_TYPE_MULTIPLE_CHOICE = "Multiple Choice";
        //Should be less than number of questions present in the dictionary
        public const int TEST_TYPE_MULTIPLE_CHOICE_ANSWER_CHOOSER_LIST_LENGTH = 4;

        public const string TEST_TYPE_TRANSLATE= "Translate";
        public static List<string> TEST_TYPES = new List<string>() { TEST_TYPE_TYPE_ALONG, TEST_TYPE_MULTIPLE_CHOICE, TEST_TYPE_TRANSLATE };
        public static int MINIMUM_QUESTIONS_REQUIRED_FOR_TEST = 5;

    }
}
