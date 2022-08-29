using DNG.Foundation.ORM.DependencyInjection.Attributes;
using DNG.Foundation.ORM.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DNG.Feature.PageContent.Services
{
    [Service(typeof(IBuggyBitsService))]
    public class BuggyBitsService : IBuggyBitsService
    {
        private readonly IRenderingService _renderingService;
        private readonly IEntityService _entityService;
        public BuggyBitsService(IRenderingService renderingService, IEntityService entityService)
        {
            _renderingService = renderingService;
            _entityService = entityService;
        }

       
        public string Execute()
        {
            var rc = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;
            string scenario = string.Empty;
            string result = "Invalid scenario, please provide correct Scenario.";
            if (rc != null)
            {
                var parms = rc.Rendering.Parameters;
                scenario = parms["Scenario"];

                switch (scenario)
                {
                    case "DBZ":
                        // Divide by zero
                        result= DivideByZeroException();
                        break;
                    case "Slow":
                        // Divide by zero
                        result = SlowPageScenario();
                        break;
                    case "Crash":
                        // Crash app
                         result =  CrashApplication();
                        break;
                    default:                        
                        break;
                }
            }

            return result;
        }

        private string CrashApplication()
        {
            try
            {
                int number1 = 10;
                int number2 = 0;
                int result = number1 / number2;
                return Convert.ToString(result);
            }
            catch (Exception ex)
            {
                // This call will crash application
                LogException(ex);
                return ex.Message;
            }
        }

        static void LogException(Exception ex)
        {
            WriteToLog(ex.Message, "c:\\log.txt");
        }

        static void WriteToLog(string message, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    //Log the event with date and time.
                    sw.WriteLine("--------------------------");
                    sw.WriteLine(DateTime.Now.ToLongTimeString());
                    sw.WriteLine("-------------------");
                    sw.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                // Infinite call - will crash
                LogException(ex);
            }
        }

        private string DivideByZeroException()
        {
            try
            {
                int number1 = 10;
                int number2 = 0;
                int result = number1 / number2;
                return Convert.ToString(result);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, ex, this);
                return ex.Message;
            }

        }

        private string SlowPageScenario()
        {
            // Slow page for 2 minutes
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(2));
            return "Page is loaded in 2 minutes";
        }
    }
}