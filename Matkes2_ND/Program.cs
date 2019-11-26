using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Matkes2_ND
{
    class Program
    {
        const string CDuom = "duomenys.txt";
        const int CKiekis = 100;
        const int C_Inervalu_skaicius = 7; 

        static void Main(string[] args)
        {
            double[] skaiciai = new double[CKiekis];

            Skaityti(skaiciai);

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(skaiciai[i]);
            //}

            Array.Sort(skaiciai);

            Console.WriteLine("Pradiniai duomenys");
            //TIKRSI GSLIMS NORMSLIAU PADARYTI CIKLA
            for (int i = 0; i < CKiekis; i += 10)
            {
                Console.WriteLine(skaiciai[i] + "  " + skaiciai[i + 2] + "  " + skaiciai[i + 3] + "  " + skaiciai[i + 4] + "  " + skaiciai[i + 5]
                    + "  " + skaiciai[i + 6] + "  " + skaiciai[i + 7] + "  " + skaiciai[i + 8] + "  " + skaiciai[i + 9] );
            }


            List<double> skirtingiSkaiciai = new List<double>();

            //GALIMA INT PADARYTI
            List<double> dazniai = new List<double>();
            List<double> santykiniaiDazniai = new List<double>();

            skirtingiSkaiciai = surastiSkirtingus(skaiciai);

            dazniai = suskaiciuotiDazni(skirtingiSkaiciai, skaiciai);

            if (skirtingiSkaiciai.Count != dazniai.Count)
            {
                Console.WriteLine("Klaida   skirtingu skaiciu ir dazniu turi buti vienodai");
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //ATPRINTINTI NRML
            for (int i = 0; i < skirtingiSkaiciai.Count; i++)
            {
                Console.Write(skirtingiSkaiciai[i] + "  ");
            }

            for (int i = 0; i < dazniai.Count; i++)
            {
                Console.Write(dazniai[i] + "    ");
            }

            //Sudedant santykinius daznius
            for (int i = 0; i < dazniai.Count; i++)
            {
                santykiniaiDazniai.Add(dazniai[i] / 100); 
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            double vid = vidurkis(skaiciai);
            Console.WriteLine(vid);



            // Imties apatinis kvartilis (X 0.25)
            double apatinis_kvartilis = (skaiciai[24] + skaiciai[25]) / 2;


            //Imties viršutinis kvartilis (X 0.75)
            double virsutinis_kvartilis = (skaiciai[74] + skaiciai[75]) / 2;

            // Mediana
            double mediana = (skaiciai[49] + skaiciai[50]) / 2;

            //Moda
            List<double> moda = rastiModa(skirtingiSkaiciai, dazniai);

            double didziausias = skaiciai.Max();
            double maziausias = skaiciai.Min();

            Console.WriteLine("DIDZIAUSIAS IR MAZIAUSIAS");
            Console.WriteLine(didziausias);
            Console.WriteLine(maziausias);



            //Nepataisyta
            double disp = dispersija(skaiciai, vid);

            //Imties standartinis nuokrypis
            double imties_nuokrypis = Math.Sqrt(disp);
            double intervaloPradzia;
            double intervaloPabaiga;
            double intervaloIlgis;

            intervaloIlgioNustatymas(maziausias, didziausias, out intervaloPradzia, ref intervaloPabaiga, ref intervaloIlgis);

            public static void intervaloIlgioNustatymas(double maziausias, double didziausias, ref double intervaloPrazia,
           ref double intervaloPabaiga, ref double inervalo_Ilgis)



            Console.ReadKey();
        }



        public static void Skaityti(double[] sk)
        {
            //Duomenu nuskaitymas is failo i strukturu masyva
            using (StreamReader reader = new StreamReader(CDuom))
            {
                string line;
                string[] parts;
                int n = 0;

                while ((line = reader.ReadLine()) != null)
                {


                    parts = line.Split(';');
                    //pav = parts[0];
                    //gatve = parts[1];
                    //numeris = long.Parse(parts[2]);


                    for (int i = 0; i < 10; i++)
                    {
                        sk[n++] = double.Parse(parts[i]);
                    }

                }



            }



        }

        public static List<double> surastiSkirtingus(double[] reiksmes)
        {
            List<double> skirtingi = new List<double>();

            for (int i = 0; i < CKiekis; i++)
            {
                if (!skirtingi.Contains(reiksmes[i]) )
                {
                    skirtingi.Add(reiksmes[i]);
                }
            }


            return skirtingi;
        }

        public static List<double> suskaiciuotiDazni(List<double> skirtingi, double[] reiksmes)
        {
            List<double> daznis = new List<double>();

            for (int i = 0; i < skirtingi.Count ; i++)
            {
                int kiekis = 0;
                for (int j = 0; j < CKiekis; j++)
                {
                    if (skirtingi[i] == reiksmes[j])
                    {
                        kiekis++;
                    }
                }

                if (kiekis == 0)
                {
                    Console.WriteLine("KLAIDA  Daznis negali buti 0");
                }
                daznis.Add(kiekis);
            }


            return daznis;
        }

        public static double vidurkis(double[] mas)
        {
            double suma = 0;

            for (int i = 0; i < CKiekis; i++)
            {
                suma += mas[i];
            }

            return (suma / CKiekis);
        }

        public static List<double> rastiModa(List<double> skirtingi,  List<double> dazniai )
        {
            int kiekis = 0;
            List<int> indeksai = new List<int>();

            //GALIMA ISKART MAX PAIMTI
            for (int i = 0; i < dazniai.Count; i++)
            {
                if (dazniai[i] > kiekis)
                {
                    kiekis =  (int)dazniai[i] ;
                }

            }

            for (int i = 0; i < dazniai.Count; i++)
            {
                if (dazniai[i] == kiekis )
                {
                    indeksai.Add(i);
                }
            }

            List<double> moda = new List<double>();

            for (int i = 0; i < indeksai.Count; i++)
            {
                moda.Add(skirtingi[indeksai[i]]);
            }


            return moda;

        }

        public static double dispersija(double[] skaiciai, double empyrinisVidurkis)
        {
            double disper = 0;

            for (int i = 0; i < CKiekis; i++)
            {
                disper += (Math.Pow((skaiciai[i] - empyrinisVidurkis), 2));
            }


            return (disper / CKiekis);
        }

        //PAKEISTI PAVADINIMA
        public static double intervaloIlgis(double min, double max)
        {
            //NEREIKIA NAUJA KINTAMOJO KURTI
            double minimali = Math.Floor(min);
            
            //O jeigu max == 140.00
            double maksimali = Math.Ceiling(max);

            return (maksimali - minimali) / C_Inervalu_skaicius;

        }


        public static void intervaloIlgioNustatymas(double maziausias, double didziausias, ref double intervaloPrazia,
            ref double intervaloPabaiga, ref  double inervalo_Ilgis)
        {

            //NEGRAZUS INTERVAL ILGIAI
            double ilgis = intervaloIlgis(maziausias, didziausias);

            char ats;
            do
            {
                Console.WriteLine("Ar tinkami intervalai? (ivesti y arba n)");
                for (int i = 0; i < CKiekis; i++)
                {
                    double prad = maziausias + i * ilgis;
                    double pab = maziausias + i * ilgis + ilgis;
                    Console.Write("Nuo {0:F2} iki {1:F2}"  , prad);
                }

                ats = Console.ReadKey().KeyChar;

            } while (ats == 'n');





        }

    }
}
