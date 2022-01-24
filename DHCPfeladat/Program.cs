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
    static Dictionary<string, int> DHCP;
    static Dictionary<string, int> reserved;
    static List<int> exclud;
    static string induloIPcim = "192.168.10.";
    static int elsoIP = 100;
    static int utsoIP = 199;

    static void Main(string[] args)
    {
      exclud = new List<int>();
      reserved = new Dictionary<string, int>();
      DHCP = new Dictionary<string, int>();

      beolvas();
      testfajl();
      kiir();
      KonzolosKiir();
      Console.ReadKey();
    }

    private static void KonzolosKiir()
    {
      foreach (var D in DHCP)
      {
        Console.WriteLine("{0};\t{1}{2}\n", D.Key, induloIPcim, D.Value);
      }
    }

    private static void testfajl()
    {
      foreach (string testI in File.ReadAllLines("test.csv"))
      {
        string tipus = testI.Split(';')[0];
        if (tipus == "request")
        {
          string mac = testI.Split(';')[1];
          if (!DHCP.ContainsKey(mac))
          {
            if (reserved.ContainsKey(mac))
            {
              int ip = reserved[mac];
              if (!DHCP.ContainsValue(ip))
              {
                DHCP.Add(mac, ip);
              }
            }
            else
            {

              while (elsoIP > utsoIP)
              {
                if (!DHCP.ContainsValue(elsoIP) && !exclud.Contains(elsoIP) && !reserved.ContainsValue(elsoIP))
                {
                  DHCP.Add(mac, elsoIP);
                  break;
                }
                elsoIP++;
              }
            }
          }
        }
        else if (tipus == "release")
        {
          int ip = int.Parse(testI.Split(';', '.')[4]);
          if (DHCP.ContainsValue(ip))
          {
            DHCP.Remove(DHCP.First(d => d.Value == ip).Key);
          }
        }
      }

    }

    private static void kiir()
    {
      StreamWriter ki = new StreamWriter("dhcp_kesz.csv");
      foreach (var D in DHCP)
      {
        ki.WriteLine("{0};{1} {2}", D.Key, induloIPcim, D.Value);
      }
      ki.Close();
    }

    private static void beolvas()
    {

      StreamReader be1 = new StreamReader("excluded.csv");

      while (!be1.EndOfStream)
      {
        string[] a = be1.ReadLine().Split('.');
        exclud.Add(int.Parse(a[3]));
      }
      be1.Close();



      //második beolvasás ----------------------------------------------------
      foreach (string resv in File.ReadAllLines("reserved.csv"))
      {
        reserved.Add(resv.Split(';')[0], int.Parse(resv.Split(';', '.')[4]));
      }

      //StreamReader be2 = new StreamReader("reserved.csv");                                                           //\\//\\            //\\//\\
      //while (!be2.EndOfStream)                                                                                       //\\//\\            //\\//\\
      //{                                                                                                              //\\//\\            //\\//\\
      //  //string[] b = be2.ReadLine().Split(';', '.');                                                               //\\//\\            //\\//\\
      //  //string vlm = b.Split(';');                                                                                 //\\//\\            //\\//\\
      // // reserved[b](int.Parse(b[4]).Split(';', '.'));                                                              //\\//\\            //\\//\\
      // // reserved.Add((b[0],b[1],b[2],b[3], int.Parse(b[4])));                                                      //\\//\\            //\\//\\
      // // reserved.Add(b.Split(';')[0], int.Parse(a.Split(';', '.')[4]));                                            //\\//\\    KULA    //\\//\\
      //}                                                                                                              //\\//\\            //\\//\\
      //be2.Close();                                                                                                   //\\//\\            //\\//\\
      //                                                                                                               //\\//\\            //\\//\\
      //foreach (string item in File.WriteAllLines("reserved.csv"))                                                    //\\//\\            //\\//\\
      //{                                                                                                              //\\//\\            //\\//\\
      //  reserved.Add((b.Split(';')[0], int.Parse(b[4])));                                                            //\\//\\            //\\//\\
      //}


      //harmadik beolvasás ----------------------------------------------------
      foreach (string dhcp in File.ReadAllLines("dhcp.csv"))
      {
        DHCP.Add(dhcp.Split(';')[0], int.Parse(dhcp.Split(';', '.')[4]));
      }

    }
  }
}
