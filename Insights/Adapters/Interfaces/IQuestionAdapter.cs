using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Adapters.Interfaces
{
    interface IQuestionAdapter
    {
        _QuestionViewModel GetQuestion(int questionId);
        void AddQuestion(_QuestionViewModel model);
        void updateQuestion(_QuestionViewModel model);
    }
}
