using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CondourApp;
namespace CondourApp
{
    public class UserData
    {
        public UserInfo UserInfo { set; get; }
        public List<PlotDetailsInfo> PlotsInfo { set; get; }
    }
   
}