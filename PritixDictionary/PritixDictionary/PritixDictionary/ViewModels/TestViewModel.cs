using PritixDictionary.Managers;
using PritixDictionary.Models;
using PritixDictionary.Services;
using PritixDictionary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PritixDictionary.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        INavigation Navigation { set; get; }
        public ICommand Next { get; private set; }
        private ControlTemplate currentQuestionTemplate;
        public ControlTemplate CurrentQuestionTemplate
        {
            get { return currentQuestionTemplate; }
            set
            {
                if (value == currentQuestionTemplate) return;
                currentQuestionTemplate = value;
                Notify("CurrentQuestionTemplate");
            }
        }
        private QuestionModel currentQuestion = new QuestionModel();
        public QuestionModel CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                if (value == currentQuestion) return;
                currentQuestion = value;
                Notify("CurrentQuestion");
            }
        }
        private string buttonText;
        public string ButtonText
        {
            get { return buttonText; }
            set
            {
                if (value == buttonText) return;
                buttonText = value;
                Notify("ButtonText");
            }
        }
        private int questionNumber=1;
        public int QuestionIndex
        {
            get { return questionNumber; }
            set
            {
                if (value == questionNumber) return;
                questionNumber = value;
                Notify("QuestionIndex");
            }
        }
        List<TestTemplateInfo> TestTemplateDetails;
        int questionIndex = 0;//init in ctor and updates on next and back button
        public TestViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ButtonText = "Next";
            Next = new Command(NextQuestion);

            TestTemplateDetails = TestManager.GetInstance().TestTemplateDetails;

            CurrentQuestion = TestTemplateDetails.ElementAt(questionIndex).TestQuestion;
            CurrentQuestionTemplate = TestTemplateDetails.ElementAt(questionIndex).QuestionTemplate;
        }

        private void NextQuestion(object obj)
        {
            TestManager.GetInstance().SubmitQuestionReportAndUpdateProgress(TestTemplateDetails.ElementAt(questionIndex), CurrentQuestion);
            
            questionIndex++;
            if (questionIndex == TestManager.GetInstance().TestQuestionsCount)
            {
                SaveUserProgress();
                Navigation.PushModalAsync(new TestReportView());
                return;
            }

            if (questionIndex == TestManager.GetInstance().TestQuestionsCount - 1)
                ButtonText = "Submit";
            CurrentQuestion = TestTemplateDetails.ElementAt(questionIndex).TestQuestion;
            CurrentQuestionTemplate = TestTemplateDetails.ElementAt(questionIndex).QuestionTemplate;
            QuestionIndex++;
        }

        void SaveUserProgress()
        {
            
            UserProgress progressToSave = new UserProgress()
            {
                DictionaryId = App.PritixDB.CurrentDictionaryInUse.DictionaryId,
                UserMarks = TestManager.GetInstance().GetUserTestMarks(),
                TotalMarks = TestManager.GetInstance().GetTotalTestMarks(),
                UserId = App.PritixDB.CurrentLoggedInUser.UserId,
                ScoreTime = DateTime.Now                
            };
           
                App.PritixDB.SaveUserProgress(progressToSave).Wait();
        }
    }
}
