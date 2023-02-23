using Quartz;
using RestSharp;
using Newtonsoft.Json;
using TaskSchedular.Helpers;
using TaskSchedular.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace TaskSchedular.CronJob
{
    public class LeadSender : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("----------------------------------------------------------------------------------------------------------");
          
            var db = new AppDBContext();
            var compaignId = Convert.ToInt32(context.MergedJobDataMap.Get("CompaignId"));

            


            var CompaignData = db.Campaigns.Where(a => a.Id == compaignId && Convert.ToInt32(a.leadperday) >= Convert.ToInt32(a.asignedtoday)).FirstOrDefault();
                if (CompaignData != null)
                {


                    var source =await db.source.Where(o => o.Name == CompaignData.sourcename).FirstOrDefaultAsync();
                    var lead =await db.sourcelead.OrderByDescending(o => o.Id).Where(o => (o.auth_key == source.auth_key && o.asignto == null && o.archieve != "false" && o.archieve != "Unverified") && (o.esp == CompaignData.esp || o.esp == CompaignData.espa || o.esp == CompaignData.espb || o.esp == CompaignData.espc || o.esp == CompaignData.espd || o.esp == CompaignData.espe)).FirstOrDefaultAsync();
                        var leadStatus = await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                        leadStatus.asignto = CompaignData.campaignname;
                        db.sourcelead.Update(leadStatus);
                        await db.SaveChangesAsync();
                var emailJson = JsonConvert.SerializeObject(new
                    {
                        ListId = "162513",
                        Email = lead.email
                    });

                string strMessage = $"\n\n({context.JobDetail.Key.Name}) - LEAD:{lead.email} - Compaign:({CompaignData.campaignname}) - AssignedToday: {CompaignData.asignedtoday} - SRC:";

                var client = new RestClient("https://api.emailoversight.com/api/emailvalidation");
                    var request = new RestRequest("https://api.emailoversight.com/api/emailvalidation", Method.Post);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("apitoken", Config.Get("OverSightEmailApiKey"));
                    request.AddParameter("application/json", emailJson, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);
                string resultss = "";

                if(response.Content !=null)
                {
                    dynamic responseObj = JsonConvert.DeserializeObject(response.Content);
                    resultss = responseObj.Result;
                }

                    

                    if (resultss == "Verified")
                    {

                        if (CompaignData.destination.Substring(0, 25) == "https://app.campaignrefin")
                        {
                            var clientts = new RestClient(CompaignData.destination);
                            var requesttss = new RestRequest(CompaignData.destination, Method.Post);

                            requesttss.AddHeader("accept", "application/x-www-form-urlencoded");
                            requesttss.AddHeader("content-type", "application/x-www-form-urlencoded");
                            requesttss.AddParameter("application/x-www-form-urlencoded", "immediate_cleaning=0&key='" + CompaignData.auth_key + "'&email='" + lead.email + "'&first_name='" + lead.first_name + "'", ParameterType.RequestBody);
                            RestResponse responseds = await clientts.ExecuteAsync(requesttss);

                            if (responseds.IsSuccessStatusCode)
                            {

                                var srcLead =await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                                srcLead.asignto = CompaignData.campaignname;
                                db.sourcelead.Update(srcLead);
                                await db.SaveChangesAsync();
                                 Console.Out.WriteLine(strMessage+ "campaignrefin");
                            }
                            else
                            {
                                 Console.Out.WriteLine("ERROR:campaignrefin");
                            }
                        }
                        else if (CompaignData.destination.Substring(0, 25) == "https://api.sendinblue.co" || CompaignData.destination.Substring(0, 25) == "https://my.sendinblue.co")
                        {
                            int[] apiListIds = {Convert.ToInt32(CompaignData.list_id) };
                            List<int> listIds = apiListIds.ToList();

                            var json = JsonConvert.SerializeObject(new
                            {

                                attributes = new
                                {
                                    Email = lead.email,
                                    LASTNAME = lead.last_name,
                                    FIRSTNAME = lead.first_name,
                                    CITY = " ",
                                    STATE = " ",
                                    PHONE = lead.phone,
                                    SUBID1 = CompaignData.label
                                },

                                listIds = listIds.ToArray(),
                                updateEnabled = true,
                                email = lead.email
                            });

                            var clientss = new RestClient(CompaignData.destination);
                            var requestss = new RestRequest(CompaignData.destination, Method.Post);
                            requestss.AddHeader("accept", "application/json");
                            requestss.AddHeader("content-type", "application/json");
                            requestss.AddHeader("api-key", CompaignData.auth_key);
                            requestss.AddParameter("application/json", json, ParameterType.RequestBody);
                            RestResponse responsess = await clientss.ExecuteAsync(requestss);
                            if (responsess.IsSuccessStatusCode)
                            {
                                var dataa = await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                                dataa.asignto = CompaignData.campaignname;
                                db.sourcelead.Update(dataa);
                                await db.SaveChangesAsync();
                                string datetime = DateTime.Now.ToShortDateString();

                                var compaign =await db.Campaigns.Where(xm => xm.datetime == datetime && xm.Id == Convert.ToInt32(CompaignData.Id)).FirstOrDefaultAsync();
                                if (compaign != null)
                                {
                                    compaign.asignedtoday = (Convert.ToInt32(compaign.asignedtoday) + 1).ToString();
                                    compaign.totalasign = (Convert.ToInt32(compaign.totalasign) + 1).ToString();
                                    db.Campaigns.Update(compaign);
                                    await db.SaveChangesAsync();
                                    Console.Out.WriteLine(strMessage + "sendinblue(1stblock)");
                                 }
                                else
                                {
                                    var comp =await db.Campaigns.Where(x => x.Id == Convert.ToInt32(CompaignData.Id)).FirstOrDefaultAsync();
                                    comp.datetime = DateTime.Now.ToShortDateString();
                                    comp.asignedtoday = (1).ToString();
                                    comp.totalasign = (comp.totalasign + 1).ToString();
                                    db.Update(comp);
                                   await db.SaveChangesAsync();
                                     Console.Out.WriteLine(strMessage + "sendinblue(2ndblock)");
                                 }

                            }
                            else
                            {

                                var srcLoad =await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                                srcLoad.archieve = "false";
                                db.sourcelead.Update(srcLoad);
                               await db.SaveChangesAsync();
                                 Console.Out.WriteLine("Lead Archived");

                             }


                        }


                    }
                    else
                    {

                        var srcLoad =await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                        Console.WriteLine("\n Unverified Email \n");
                        srcLoad.archieve = "Unverified";
                        db.sourcelead.Update(srcLoad);
                       await db.SaveChangesAsync();
                    Console.Out.WriteLine(strMessage + "SKIPPING(UNVERIFIED)");

                    }

                  

            }
        }
    }
}