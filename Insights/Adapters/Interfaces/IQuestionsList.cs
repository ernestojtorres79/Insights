using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Adapters.Interfaces
{
    interface IQuestionsList
    {
        List<_QuestionViewModel> GetAllQuestions(int campaignId);
    }
}
