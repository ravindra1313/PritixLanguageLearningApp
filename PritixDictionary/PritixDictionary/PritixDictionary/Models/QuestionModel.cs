using PritixDictionary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Models
{
    public class QuestionModel : ViewModelBase
    {

        private string question;
        public string Question
        {
            get { return question; }
            set
            {
                if (value == question) return;
                question = value;
                Notify("Question");
            }
        }
        private string userAnswer;
        public string UserEnteredAnswer
        {
            get { return userAnswer; }
            set
            {
                if (value == userAnswer) return;
                userAnswer = value;
                Notify("UserEnteredAnswer");
            }
        }
        private List<string> answerChooserList;
        public List<string> AnswerChooserList
        {
            get { return answerChooserList; }
            set
            {
                if (value == answerChooserList) return;
                answerChooserList = value;
                Notify("AnswerChooserList");
            }
        }
        private int selectedAnswerIndex=-1;
        public int SelectedAnswerIndex
        {
            get { return selectedAnswerIndex; }
            set
            {
                if (value == selectedAnswerIndex) return;
                selectedAnswerIndex = value;
                Notify("SelectedAnswerIndex");
            }
        }

    }
}
