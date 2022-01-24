using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DHCPfeladat
{
  class Program
  {
    static Dictionary<string, int> dhcp;
    static Dictionary<string, int> reserved;
    static List<int> exclud;
    static string induloIPcim = "192.168.10.";
    static int elsoIP = 100;
    static int utsoIP = 199;

    static void Main(string[] args)
    {
      exclud =new List<int>();
      reserved = new Dictionary<string, int>();
      dhcp = new Dictionary<string, int>();

    }
  }
}
