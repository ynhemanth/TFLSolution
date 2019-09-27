using Model;
using System;
using System.Threading.Tasks;
using System.Configuration;
using Service;

namespace Presentation
{
    public class RoadStatus
    {
        public static void Main(string[] args)
        {
            try
            {
                var roadDetails = new RoadDetails();
                for (; ; )
                {
                    Console.WriteLine("Please enter a Road Name");
                    roadDetails.RoadName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(roadDetails.RoadName))
                    {
                        Console.WriteLine("Can't enter empty value, Please enter a Road Name");
                        continue;
                    }

                    if (roadDetails.RoadName.ToLower() == "exit")
                        Exit();

                    getRoadStatusDetails(roadDetails);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("error processing your request, please reset and restart");
            }
        }

        private static void getRoadStatusDetails(RoadDetails roadDetails)
        {
            ContainerSetUp.Init();
            var initiator = InitiateContainer.Retrieve<ProcessRoadService>();

            setBaseValues(roadDetails);

            var resultSet = initiator.FetchRoadStatusDetails(roadDetails);

            if (string.IsNullOrWhiteSpace(resultSet.Result.httpStatusCode) &&
                resultSet.Result.httpStatusCode != Constants.statusCode &&
                string.IsNullOrWhiteSpace(resultSet.Result.message))
                successMessage(resultSet.Result);
            else
                unsuccessMessage(resultSet.Result);
        }

        private static void successMessage(RoadStatusInfo roadStatusInfo)
        {
            Console.WriteLine("The status of the " + roadStatusInfo.displayName + " is as follows");
            Console.WriteLine("Road Status is " + roadStatusInfo.statusSeverity);
            Console.WriteLine("Road Status Description is " + roadStatusInfo.statusSeverityDescription);
        }

        private static void unsuccessMessage(RoadStatusInfo roadStatusInfo)
        {
            Console.WriteLine(roadStatusInfo.message);
        }

        private static void Exit()
        {
            Console.WriteLine("The application is exiting");
            Console.WriteLine("Thank you");
            Console.WriteLine("Press enter key");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private static void setBaseValues(RoadDetails roadDetails)
        {
            roadDetails.BaseUrl = getAppsettingValue(Constants.AppKeyBaseUrl);
            roadDetails.ApiName = getAppsettingValue(Constants.AppKeyApiName);
            roadDetails.ApiId = getAppsettingValue(Constants.AppKeyApiId);
            roadDetails.ApiKey = getAppsettingValue(Constants.AppKeyApiKey);
        }

        private static string getAppsettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
