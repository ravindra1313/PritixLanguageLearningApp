using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PritixDictionary.Models
{
    public class TestTemplateInfo
    {
        public int TestQuestionId { get; set; }
        public QuestionModel TestQuestion { get; set; }
        public ControlTemplate QuestionTemplate { get; set; }

    }
}
