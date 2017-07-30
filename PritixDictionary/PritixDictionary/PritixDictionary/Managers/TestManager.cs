using PritixDictionary.Models;
using PritixDictionary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static PritixDictionary.Utilities.AppConstants;

namespace PritixDictionary.Managers
{
    public class TestManager
    {
        private static TestManager instance;
        public int TestQuestionsCount { get; set; }
        public string TestType { get; set; }
        List<DictionaryDetail> SourceDictionary { get; set; }
        List<DictionaryDetail> TestQuestionBank { get; set; }
        public List<TestTemplateInfo> TestTemplateDetails { get; set; }
        Dictionary<int, bool> TestProgress ;

        private TestManager()
        {

        }
        public static TestManager GetInstance()
        {
            if (instance == null)
            {
                instance = new TestManager();
            }
            return instance;
        }

        public void ConfigureTest(List<DictionaryDetail> dictionary,int questionCount,string testType)
        {
            TestProgress = new Dictionary<int, bool>();
            SourceDictionary = dictionary;
            TestQuestionsCount = questionCount;
            TestType = testType;
            TestQuestionBank = GetTestQuestionBank();
            TestTemplateDetails = GetTestTemplateDetails();
        }

        public ControlTemplate GetTemplateFromTestType(string type)
        {
            var template = (ControlTemplate)Application.Current.Resources["TestTypeAlong"];
            switch (type)
            {
                case AppConstants.TEST_TYPE_TYPE_ALONG:
                    template = (ControlTemplate)Application.Current.Resources["TestTypeAlong"];
                    break;
                case AppConstants.TEST_TYPE_MULTIPLE_CHOICE:
                    template = (ControlTemplate)Application.Current.Resources["TestMultipleChoice"];
                    break;
                case AppConstants.TEST_TYPE_TRANSLATE:
                    template = (ControlTemplate)Application.Current.Resources["TestTranslate"];
                    break;
            }
            return template;
        }

        List<DictionaryDetail> GetTestQuestionBank()
        {
            Random rnd = new Random();
            return this.SourceDictionary.OrderBy(x => rnd.Next()).Take(TestQuestionsCount).ToList();
        }

         List<TestTemplateInfo>  GetTestTemplateDetails()
        {
            List<TestTemplateInfo> templateDetails = new List<TestTemplateInfo>();
            for (int item = 0; item < TestQuestionBank.Count; item++)
            {
                var detail = TestQuestionBank.ElementAt(item);

                var question = new QuestionModel() { Question = detail.Key,AnswerChooserList = GetAnswerChooserList(detail)};
                Random rnd = new Random();
                TestTemplateInfo templateInformation = new TestTemplateInfo()
                {
                    TestQuestionId = item,
                    TestQuestion = question,
                    QuestionTemplate = GetTemplateFromTestType(this.TestType)
                    //For shuffle
                 //R   QuestionTemplate = AvailableTemplates.ElementAt(rnd.Next(0, AvailableTemplates.Count))
                };

                templateDetails.Add(templateInformation);
            }


            return templateDetails;

        }
        
        public void SubmitQuestionReportAndUpdateProgress(TestTemplateInfo templateInfo,QuestionModel question)
        {
            var correctAnswer = TestQuestionBank.Where(x => x.Key == question.Question).FirstOrDefault().Value;
           if(this.TestType == AppConstants.TEST_TYPE_MULTIPLE_CHOICE)
            {
                question.UserEnteredAnswer = question.SelectedAnswerIndex!=-1?question.AnswerChooserList.ElementAt(question.SelectedAnswerIndex):string.Empty;
            }            
           if(question.UserEnteredAnswer==null)
            {
                question.UserEnteredAnswer = string.Empty;
            }
            //to avoid case sensitivity
            bool IsValidAnswer = correctAnswer.ToLower() == question.UserEnteredAnswer.ToLower() ? true : false;

            TestProgress.Add(templateInfo.TestQuestionId, IsValidAnswer);
        }

        public int GetUserTestMarks()
        {
            return TestProgress.Where(x => x.Value == true).ToList().Count;
        }
        public int GetTotalTestMarks()
        {
           return TestProgress.Count;
        }
        List<string> GetAnswerChooserList(DictionaryDetail question)
        {
            Random rnd = new Random();
            var otherWords = this.SourceDictionary.Where(x=>x.Value != question.Value).Take(AppConstants.TEST_TYPE_MULTIPLE_CHOICE_ANSWER_CHOOSER_LIST_LENGTH - 1).ToList();
            otherWords.Add(question);
            return otherWords.OrderBy(x => rnd.Next()).Select(x=>x.Value).ToList();
        }


    }
}
