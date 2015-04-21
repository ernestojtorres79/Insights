using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Adapters.Interfaces
{
    interface ITwilioAdapter
    {
        _TwilioMSGViewModel SendMessage(_TwilioMSGViewModel model);
        List<_TwilioMSGViewModel> GetAllMessages();
    }
}
