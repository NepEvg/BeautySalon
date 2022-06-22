using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_Saloon.Model
{
    public partial class ClientService
    {
        public string MinuteBeforeRecords
        {
            get {
                var dateTime = StartTime - DateTime.Now;
                string time = dateTime.ToString();
                time = time.Substring(0, time.LastIndexOf(':'));
                string hour = time[0].ToString() + time[1];
                string minute = time[3].ToString() + time[4];
                //return StartTime.ToString()+" "+ DateTime.Now.ToString()+" " +time+" "+ dateTime.ToString();
                return hour + "часа(ов)" + minute + "минут(ы)";
            }
            set { }
        }
    }
}
