namespace TaskSchedular.CronJob
{
    public class LeadHandler
    {

        public static List<int> Payload { get;set; }  = new List<int>();
        
        public static void SetLeadId(int id)
        {
            Payload.Add(id);
        }

        public static bool LeadExist(int id)
        {
            var bReturn = false;
            foreach(var item in Payload)
            {
                if(item.Equals(id))
                {
                    bReturn = true;
                    break;
                }
            }

            return bReturn;
        }
    }
}
