using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartWCFClient_2.Classes
{
    public class MyParameterInfo
    {
        public ParameterInfo parameterInfo { get; set; }
        public String ValueTest { get; set; }
        public MyParameterInfo(ParameterInfo param)
        {
            parameterInfo = param;


        }
    }
}
