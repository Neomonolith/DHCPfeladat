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
      exclud =new List<int>();
      reserved = new Dictionary<string, int>();
      DHCP = new Dictionary<string, int>();

      beolvas();
      testfajl();
      kiir();

      Console.ReadKey();
    }

    private static void testfajl()
    {


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
      //be2.Close();                                                                                                   //\\//\\  ( ﾉ ﾟｰﾟ)ﾉ //\\//\\
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
