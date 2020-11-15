using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["request"];
            var subjects = JsonConvert.DeserializeObject<List<Subject>>(data);
            int minMarksObtained = subjects.Min(y => y.marks);
            int maxMarksObtained = subjects.Max(y => y.marks);
            var MinMarksSubject = subjects.First(x => x.marks == minMarksObtained);
            var MaxMarksSubject = subjects.First(x => x.marks == maxMarksObtained);
            var subjecttotalMarks = subjects.Count();
            var totalMarksObt = subjecttotalMarks * 100;
            decimal totalmarks = subjects.Sum(x => x.marks);
            decimal percentage = (totalmarks / totalMarksObt) * 100;

            Result result = new Result()
            {
                MinimumSubject = MinMarksSubject,
                MaximumSubject = MaxMarksSubject,
                Percentage = percentage
            };

            return JsonConvert.SerializeObject(result);
        }



    }
    internal class StripMethodAttribute : Attribute
    {
        private ResponseFormat json;
        private bool UseHttpGet;

        public StripMethodAttribute(ResponseFormat json, bool UseHttpGet)
        {
            this.json = json;
            this.UseHttpGet = UseHttpGet;
        }
    }
}

