using Insights.Adapters.Interfaces;
using Insights.Data;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using System.Data.Entity;

namespace Insights.Adapters.Adapters
{
    public class TwilioAdapter : ITwilioAdapter
    {

        public _TwilioMSGViewModel SendMessage(_TwilioMSGViewModel model)
        {
            TwilioRestClient client = new TwilioRestClient(model.ACCOUNT_SID, model.AUTH_TOKEN);
            client.SendSmsMessage(model.From, model.To, model.Body);
            return model;
        }


        public List<_TwilioMSGViewModel> GetAllMessages()
        {            
            // Twilio request of list of messages
            TwilioRestClient client = new TwilioRestClient(AuthTwilio.ACCOUNT_SID, AuthTwilio.AUTH_TOKEN);
            MessageListRequest request = new MessageListRequest();
            DateTime today = DateTime.Now;
            request.DateSent = today;
            var messages = client.ListMessages(request);
            List<_TwilioMSGViewModel> msg;
            msg = messages.Messages.Select(s => new _TwilioMSGViewModel()
            {
                Body = s.Body,
                From = s.From,
                To = s.To,
                Sid = s.Sid,
                DateSent = s.DateSent
            }).ToList();

            // Takes Msg looks into Active Campaigns for Keyword, assigns SurveyClient to Campaign
            List<string> TwilioSid = msg.Select(t => t.Sid).ToList();
            List<_TwilioMSGViewModel> newMessages;
            List<_TwilioMSGViewModel> newSurveyClient;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<TwilioMSG> previousMessages = db.TwilioMSGs.Where(m=> TwilioSid.Contains(m.TwiilioMSGSid)).ToList();
                newMessages = msg.Where(m => !previousMessages.Any(m2 => m2.TwiilioMSGSid == m.Sid)).ToList();
                if (newMessages.Count != 0)
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    { 
                            foreach (_TwilioMSGViewModel element in newMessages)
                            {
                        
                        
                                    TwilioMSG dbModel = context.TwilioMSGs.Create();
                                    dbModel.TwilioMSGBody = element.Body;
                                    dbModel.TwilioMSGFrom = element.From;
                                    dbModel.TwilioMSGTo = element.To;
                                    dbModel.TwiilioMSGSid = element.Sid;
                                    dbModel.TwilioMSGDate = element.DateSent;
                                    context.TwilioMSGs.Add(dbModel);
                           
                        
                            }
                        context.SaveChanges();
                    }
                    List<Campaign> activeCampaings = db.Campaigns.Where(c => c.CampaignActive == true).ToList();
                    List<string> activeKeywords = activeCampaings.Select(k => k.CampaignKeyword).ToList();
                    List<_TwilioMSGViewModel> MsgWithKeywords = newMessages.Where(c => activeKeywords.Contains(c.Body)).ToList();
                    if (MsgWithKeywords.Count != 0)
                    {
                        List<string> activePhoneNumbers = db.SurveyClients.Select(p => p.SurveyClientPhone).ToList();
                        List<SurveyClient> previousSurveyClients = db.SurveyClients.Where(s => activePhoneNumbers.Contains(s.SurveyClientPhone)).ToList();
                        newSurveyClient = MsgWithKeywords.Where(q => !previousSurveyClients.Any(q2 => q2.SurveyClientPhone == q.From)).ToList();
                        if (newSurveyClient.Count != 0)
                        {
                            foreach (_TwilioMSGViewModel SClient in newSurveyClient)
                                {
                                    using (ApplicationDbContext context = new ApplicationDbContext())
                                    {
                                        SurveyClient dbClient = context.SurveyClients.Create();
                                        dbClient.SurveyClientPhone = SClient.From;
                                        context.SurveyClients.Add(dbClient);
                                        context.SaveChanges();
                                    }
                                    using (ApplicationDbContext context = new ApplicationDbContext())
                                    {
                                        SurveyClientControl SCdb = context.SurveyClientControls.Create();
                                        SCdb.SurveyClientId = context.SurveyClients.Where(x => x.SurveyClientPhone == SClient.From).FirstOrDefault().SurveyClientId;
                                        SCdb.SurveyClientControlId = context.SurveyClients.Where(x => x.SurveyClientPhone == SClient.From).FirstOrDefault().SurveyClientId;
                                        SCdb.SurveyClientPhone = SClient.From;
                                        SCdb.CampaignId = context.Campaigns.Where(y => y.CampaignKeyword == SClient.Body).FirstOrDefault().CampaignId;
                                        SCdb.Questions = context.Questions.Where(q => q.CampaignId == SCdb.CampaignId).ToList();
                                        context.SurveyClientControls.Add(SCdb);
                                        context.SaveChanges();
                                    }
                                    using (ApplicationDbContext questionDb = new ApplicationDbContext())
                                    {
                                        SurveyClientControl dbQuestions = questionDb.SurveyClientControls.Include(s=> s.Questions).Where(q => q.SurveyClientPhone == SClient.From).FirstOrDefault();
                                        if (dbQuestions.Questions.Count != 0) 
                                        {                                     
                                            foreach (var element in dbQuestions.Questions)
                                            {
                                                using (ApplicationDbContext progressDb = new ApplicationDbContext())
                                                {
                                                    Progress dbProgress = progressDb.ProgressSwitches.Create();
                                                    dbProgress.ProgressSwitch = false;
                                                    dbProgress.QuestionId = element.QuestionId;
                                                    dbProgress.SurveyClientControlId = dbQuestions.SurveyClientControlId;
                                                    progressDb.ProgressSwitches.Add(dbProgress);
                                                    progressDb.SaveChanges();
                                                }

                                                using (ApplicationDbContext currentQuestionDb = new ApplicationDbContext())
                                                {
                                                    CurrentQuestion dbCQuestion = currentQuestionDb.CurrentQuestionSwitches.Create();
                                                    dbCQuestion.CurrentQuestionSwitch = false;
                                                    dbCQuestion.QuestionId = element.QuestionId;
                                                    dbCQuestion.SurveyClientControlId = dbQuestions.SurveyClientControlId;
                                                    currentQuestionDb.CurrentQuestionSwitches.Add(dbCQuestion);
                                                    currentQuestionDb.SaveChanges();
                                                }
                                            }
                                        }
                                    }

                                }
                        }
                    }
                    // If Msg does not includes a Keyword, is considered as san Answer and it comes here
                    else
                    {
                        using (ApplicationDbContext questionsCompare = new ApplicationDbContext())
                        {
                            
                            List<SurveyClientControl> SurveyClients = questionsCompare.SurveyClientControls.ToList();
                            List<string> Questions = questionsCompare.Questions.Select(q => q.QuestionBody).ToList();
                            List<_TwilioMSGViewModel> leftMsgs = newMessages.Where(m => !Questions.Contains(m.Body)).ToList();
                            if (leftMsgs.Count != 0)
                            {
                                
                                    foreach (var MsgElement in leftMsgs)
                                    {
                                        if (MsgElement.From != "+18324101832")
                                        {
                                            using (ApplicationDbContext LogResponse = new ApplicationDbContext())
                                            {
                                                List<int> Progress_QuestionId = LogResponse.ProgressSwitches.Where(d => d.ProgressSwitch == true).Select(c => c.QuestionId).ToList();
                                                int Last_Progress_QuestionId = Progress_QuestionId.LastOrDefault();
                                                Response AddResponse = LogResponse.Responses.Create();
                                                AddResponse.ResponseBody = MsgElement.Body;
                                                AddResponse.QuestionId = Last_Progress_QuestionId;
                                                AddResponse.SurveyClientControlId = SurveyClients.Where(b => b.SurveyClientPhone == MsgElement.From).FirstOrDefault().SurveyClientControlId;
                                                LogResponse.Responses.Add(AddResponse);
                                                LogResponse.SaveChanges();
                                                break;
                                            }
                                        }
                                        
                                    }
                                using (ApplicationDbContext StatusChangeCurrentQuestion = new ApplicationDbContext())
                                    {
                                        foreach (var ClientElement in leftMsgs)
                                        {
                                            List<int> Progress_QuestionId = StatusChangeCurrentQuestion.ProgressSwitches.Where(d => d.ProgressSwitch == true).Select(c=> c.QuestionId).ToList();
                                            List<int> Current_Switches_False = StatusChangeCurrentQuestion.CurrentQuestionSwitches.Where(c => c.CurrentQuestionSwitch == false).Select(d => d.QuestionId).ToList();
                                            int Last_Progress_QuestionId = Progress_QuestionId.LastOrDefault();
                                            if (Progress_QuestionId.Count != 0 && Current_Switches_False.Count != 0 && ClientElement.From != "+18324101832")
                                            {
                                                SurveyClientControl CurrentClient = SurveyClients.Where(q => q.SurveyClientPhone == ClientElement.From).FirstOrDefault();
                                                CurrentQuestion CurrentStatus = StatusChangeCurrentQuestion.CurrentQuestionSwitches.Where(c => c.QuestionId == Last_Progress_QuestionId && c.SurveyClientControlId == CurrentClient.SurveyClientControlId).FirstOrDefault();
                                                CurrentStatus.CurrentQuestionSwitch = true;                               
                                                StatusChangeCurrentQuestion.SaveChanges();
                                                break;
                                            }
                                            if (Progress_QuestionId.Count != 0 && Current_Switches_False.Count != 0 && ClientElement.From == "+18324101832")
                                            {
                                                SurveyClientControl CurrentClient = SurveyClients.Where(q => q.SurveyClientPhone == ClientElement.To).FirstOrDefault();
                                                CurrentQuestion CurrentStatus = StatusChangeCurrentQuestion.CurrentQuestionSwitches.Where(c => c.QuestionId == Last_Progress_QuestionId && c.SurveyClientControlId == CurrentClient.SurveyClientControlId).FirstOrDefault();
                                                CurrentStatus.CurrentQuestionSwitch = true;
                                                StatusChangeCurrentQuestion.SaveChanges();
                                                break;
                                            }
                                        }
                                    }   
                            }
                        }
                        
                    }
                }
                // If Not new message, system will send questions to Survey Clients
                else
                {
                    List<SurveyClientControl> SurveyClientsReady;
                    using (ApplicationDbContext serviceReady = new ApplicationDbContext())
                           {
                                SurveyClientsReady = serviceReady.SurveyClientControls.Include(s=> s.Questions).Include(s=> s.ProgressSwitches).Include(s=> s.CurrentQuestionSwitches).Where(q => q.BlackList == false).ToList();
                                foreach (var ClientElement in SurveyClientsReady)
                                {
                                    List<int> ProgressIds = ClientElement.ProgressSwitches.Where(o=> o.ProgressSwitch == false).Select(p=> p.QuestionId).ToList();
                                    List<int> ProgressTrue = ClientElement.ProgressSwitches.Where(p => p.ProgressSwitch == true).Select(z => z.QuestionId).ToList();
                                    List<int> CurrentQuestionIds = ClientElement.CurrentQuestionSwitches.Where(q => q.CurrentQuestionSwitch == true).Select(r => r.CurrentQuestionId).ToList();
                                    if (ProgressIds.Count != 0 && CurrentQuestionIds.Count != 0 && ProgressTrue.Count == CurrentQuestionIds.Count)
                                    {
                                            Question Question_Filter_Progress = ClientElement.Questions.Where(q => ProgressIds.Contains(q.QuestionId)).FirstOrDefault();                                               
                                            TwilioRestClient clientSendMsg = new TwilioRestClient(AuthTwilio.ACCOUNT_SID, AuthTwilio.AUTH_TOKEN);
                                            clientSendMsg.SendSmsMessage("832-410-1832", ClientElement.SurveyClientPhone, Question_Filter_Progress.QuestionBody);
                                            using (ApplicationDbContext changeProgress = new ApplicationDbContext())
                                            {
                                                Progress ProgressChangeStatus = changeProgress.ProgressSwitches.Where(i => i.QuestionId == Question_Filter_Progress.QuestionId).FirstOrDefault();
                                                ProgressChangeStatus.ProgressSwitch = true;
                                                changeProgress.SaveChanges();
                                            }
                                    }
                                    else
                                    {
                                        if (ProgressTrue.Count == 0 && CurrentQuestionIds.Count == 0)
                                        {
                                            Question Question_Filter_Progress = ClientElement.Questions.Where(q => ProgressIds.Contains(q.QuestionId)).FirstOrDefault();                                            
                                            TwilioRestClient clientSendMsg = new TwilioRestClient(AuthTwilio.ACCOUNT_SID, AuthTwilio.AUTH_TOKEN);
                                            clientSendMsg.SendSmsMessage("832-410-1832", ClientElement.SurveyClientPhone, Question_Filter_Progress.QuestionBody);
                                            using (ApplicationDbContext changeProgress = new ApplicationDbContext())
                                            {
                                                Progress ProgressChangeStatus = changeProgress.ProgressSwitches.Where(i => i.QuestionId == Question_Filter_Progress.QuestionId).FirstOrDefault();
                                                ProgressChangeStatus.ProgressSwitch = true;
                                                changeProgress.SaveChanges();
                                            }
                                        }
                                    }
                                    if (ProgressIds.Count == 0 && ClientElement.BlackList == false)
                                    {
                                        Campaign Coupon;
                                        using (ApplicationDbContext lastMsg = new ApplicationDbContext())
                                        {
                                            Coupon = lastMsg.Campaigns.Where(q => q.CampaignId == ClientElement.CampaignId).FirstOrDefault();                                            
                                        }
                                        TwilioRestClient clientSendMsg = new TwilioRestClient(AuthTwilio.ACCOUNT_SID, AuthTwilio.AUTH_TOKEN);
                                        clientSendMsg.SendMessage("832-410-1832", ClientElement.SurveyClientPhone, "Thank you for Participating", new string[] { Coupon.CampaignGift });
                                        ClientElement.BlackList = true;
                                        serviceReady.SaveChanges();
                                    }
                                    
                                    
                                }                                       
                           }
                }
            }           
            return msg;
        }
    }
}