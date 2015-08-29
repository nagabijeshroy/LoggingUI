using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logging_UI;
using PagedList;
namespace App_Config_Logger.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Session["Severity"] = "";
            Session["title"] = "";
            Session["machineName"] = "";
            Session["FromDate"] = "";
            Session["ToDate"] = "";
            return View();
        }
        public ActionResult LogData(string sortOrder, string CurrentSort, int? page, String Severity, String title, String machineName, String FromDate, String ToDate)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
                Session["Severity"] = "" + Severity;
                Session["title"] = "" + title;
                Session["machineName"] = "" + machineName;
                Session["FromDate"] = "" + FromDate;
                Session["ToDate"] = "" + ToDate;
            }
            else
            {
                Severity = Session["Severity"].ToString();
                title = Session["title"].ToString();
                machineName = Session["machineName"].ToString();
                FromDate = Session["FromDate"].ToString();
                ToDate = Session["ToDate"].ToString();
            }
            
            using (var db = new LoggingEntities())
            {
                var predicate = PredicateBuilder.True<Log>();
                IQueryable<Log> query = db.Logs;
                if (Severity != "All" && Severity != string.Empty)
                {
                    predicate = predicate.And(en => en.Severity == Severity);
                }

                if (title != string.Empty)
                {
                    predicate = predicate.And(en => en.Title.Contains(title));
                }
                if (machineName != string.Empty)
                {
                    predicate = predicate.And(en => en.MachineName.Contains(machineName));
                }
                if (FromDate != string.Empty)
                {
                    DateTime from = DateTime.Parse(FromDate);
                    query = query.Where(en => en.Timestamp >= from);
                }
                if (ToDate != string.Empty)
                {
                    DateTime to = DateTime.Parse(ToDate);
                    query = query.Where(en => en.Timestamp <= to);
                }
                ViewBag.CurrentSort = sortOrder;
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "Timestamp" : sortOrder;
                var LogsList = query.Where(predicate).OrderBy(en => en.Timestamp);
                switch (sortOrder)
                {
                    case "LogID":
                        if (sortOrder.Equals(CurrentSort))
                                LogsList = query.Where(predicate).OrderByDescending(en => en.LogID);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.LogID); 
                        break;
                    case "EventID": 
                        if (sortOrder.Equals(CurrentSort))
                                LogsList = query.Where(predicate).OrderByDescending(en => en.EventID);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.EventID); 
                        break;
                    case "Priority": 
                        if (sortOrder.Equals(CurrentSort))
                                LogsList = query.Where(predicate).OrderByDescending(en => en.Priority);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Priority);
                        break;
                    case "Severity":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.Severity);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Severity);
                        break;
                    case "Title": 
                        if (sortOrder.Equals(CurrentSort))
                                LogsList = query.Where(predicate).OrderByDescending(en => en.Title);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Title);
                        break;
                    case "Timestamp":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.Timestamp);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Timestamp);
                        break;
                    case "MachineName":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.MachineName);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.MachineName);
                        break;
                    case "AppDomainName":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.AppDomainName);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.AppDomainName);
                        break;
                    case "ProcessID":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.ProcessID);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.ProcessID);
                        break;
                    case "ProcessName":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.ProcessName);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.ProcessName);
                        break;
                    case "ThreadName":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.ThreadName);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.ThreadName);
                        break;
                    case "Win32ThreadId":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.Win32ThreadId);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Win32ThreadId);
                        break;
                    case "Message":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.Message);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Message);
                        break;
                    case "FormattedMessage":
                        if (sortOrder.Equals(CurrentSort))
                            LogsList = query.Where(predicate).OrderByDescending(en => en.FormattedMessage);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.FormattedMessage);
                        break;
                    default: 
                        if (sortOrder.Equals(CurrentSort))
                                LogsList = query.Where(predicate).OrderByDescending(en => en.Timestamp);
                        else
                            LogsList = query.Where(predicate).OrderBy(en => en.Timestamp);
                            break;
                }
                
                int pageSize = 20;
                int pageNumber = (page ?? 1);
                return View(LogsList.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}
