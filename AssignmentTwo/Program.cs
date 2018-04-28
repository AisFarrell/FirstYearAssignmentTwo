using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AssignmentTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] locations = new string[5];
            locations[0] = "The Pacific";
            locations[1] = "The Atlantic";
            locations[2] = "The Mediterranean";
            locations[3] = "The Indian Ocean";
            locations[4] = "The Other Seas";

            string[] vesselTypes = new string[6];
            vesselTypes[0] = "Aircraft Carrier";
            vesselTypes[1] = "Cruiser/Battleship";
            vesselTypes[2] = "Destroyer";
            vesselTypes[3] = "Frigate";
            vesselTypes[4] = "Nuclear Submarine";
            vesselTypes[5] = "Minelayer/Sweeper";

            int[] crewCosts = new int[6];
            crewCosts[0] = 2610;
            crewCosts[1] = 2350;
            crewCosts[2] = 2050;
            crewCosts[3] = 999;
            crewCosts[4] = 2550;
            crewCosts[5] = 2510;

            string choice = "";

            while (choice != "4")
            {
                // Clear out old reports so that just menu shows each time
                Console.Clear();

                // MENU
                Console.WriteLine("Menu ");
                Console.WriteLine("1. Vessel Report ");
                Console.WriteLine("2. Location Analysis Report ");
                Console.WriteLine("3. Search for a vessel ");
                Console.WriteLine("4. Exit ");
                Console.WriteLine();

                Console.Write("Enter Choice: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        VesselReport(locations, vesselTypes, crewCosts);

                        Console.WriteLine("Press any key to return to the menu ");
                        Console.ReadKey();
                        break;
                    case "2":
                        LocationAnalysisReport(locations);

                        Console.WriteLine("Press any key to return to the menu ");
                        Console.ReadKey();
                        break;
                    case "3":
                        Search(locations);

                        Console.WriteLine("Press any key to return to the menu ");
                        Console.ReadKey();
                        break;

                    case "4":
                        //nothing to do here. user wants to exit and loop supports that
                        break;
                }
            }
        }

        static void VesselReport(string[] locations, string[] vesselTypes, int[] crewCosts)
        {
            Console.WriteLine("French Naval Vessel Inventory Report");

            // declare heading variables
            string locationHeading = ("Location");
            string functionHeading = ("Function");
            string vesselNameHeading = ("Vessel Name");
            string tonnageHeading = ("Tonnage");
            string crewHeading = ("Crew");
            string monthlyCostHeading = ("Mthly Cost");

            // print out the headings
            Console.WriteLine("{0,-20}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", locationHeading, functionHeading, vesselNameHeading, tonnageHeading, crewHeading, monthlyCostHeading);

            // read in file
            StreamReader reader = new StreamReader("F:\\FrenchMF.txt");
            string[] parts = new string[5];
            string lineIn = reader.ReadLine();
            while (lineIn != null)
            {
                parts = lineIn.Split(',');


                // location
                string locationCodeStr = parts[4];
                int locationCode = int.Parse(locationCodeStr);
                int previousLocation = locationCode;
                int[] locationGrandtotals = new int[5];

                while (previousLocation == locationCode && lineIn != null)
                {
                    parts = lineIn.Split(',');
                    locationCodeStr = parts[4];
                    locationCode = int.Parse(locationCodeStr);

                    string locationName = locations[locationCode - 1];

                    //vessel type/function
                    string vesselTypesCodeStr = parts[1];
                    int vesselTypeCode = int.Parse(vesselTypesCodeStr);
                    string vesselType = vesselTypes[vesselTypeCode - 1];

                    // vessel name
                    string vesselName = parts[0];

                    // tonnage
                    int tonnage = int.Parse(parts[2]);

                    //crew
                    int crew = int.Parse(parts[3]);

                    //monthly cost
                    int monthlyCostPerCrewMember = crewCosts[vesselTypeCode - 1];
                    int totalCostPerCrew = monthlyCostPerCrewMember * crew;

                    // print out values for each
                    Console.WriteLine("{0,-20}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", previousLocation, vesselTypeCode, vesselName, tonnage, crew, monthlyCostPerCrewMember);

                    // bonus question - location grandtotal

                    locationGrandtotals[previousLocation - 1] += monthlyCostPerCrewMember;
                    lineIn = reader.ReadLine();
                } // end of an location

                Console.WriteLine("Location Total Ship Count {0}", locationGrandtotals[previousLocation - 1]);
            }

            reader.Close();
        }

        static void LocationAnalysisReport(string[] locations)
        {
            Console.WriteLine("Location Analysis Report");

            int location1Count = 0;
            int location2Count = 0;
            int location3Count = 0;
            int location4Count = 0;
            int location5Count = 0;

            //read in file
            StreamReader reader = new StreamReader("F:\\FrenchMF.txt");
            string[] parts = new string[5];
            string lineIn = reader.ReadLine();

            while (lineIn != null)
            {
                parts = lineIn.Split(',');
                int locationCode = int.Parse(parts[4]);

                switch (locationCode)
                {
                    case 1:
                        location1Count++;
                        break;
                    case 2:
                        location2Count++;
                        break;
                    case 3:
                        location3Count++;
                        break;
                    case 4:
                        location4Count++;
                        break;
                    case 5:
                        location5Count++;
                        break;
                }

                lineIn = reader.ReadLine();
            }
            // Print out headings
            string locationHeading = ("Location");
            string vesselCountHeading = ("Vessel Count");

            Console.WriteLine("{0,20}{1,30}", locationHeading, vesselCountHeading);

            Console.WriteLine("{0,20}{1,30}", locations[0], location1Count);
            Console.WriteLine("{0,20}{1,30}", locations[1], location2Count);
            Console.WriteLine("{0,20}{1,30}", locations[2], location3Count);
            Console.WriteLine("{0,20}{1,30}", locations[3], location4Count);
            Console.WriteLine("{0,20}{1,30}", locations[4], location5Count);

            // bonus mark Q
            int totalShips = location1Count + location2Count + location3Count + location4Count + location5Count;
            Console.WriteLine("{0,20}{1,30}", "Total: ", totalShips);

        }

        static void Search(string[] locations)
        {
            // prompt user to enter in vessel name
            Console.Write("Enter vessel name:  ");
            string nameInput = Console.ReadLine();

            // if no ship found - tell user
            bool shipFound = false;

            // read in file
            StreamReader reader = new StreamReader("F:\\FrenchMF.txt");
            string[] parts = new string[5];
            string lineIn = reader.ReadLine();

            while (lineIn != null)
            {
                parts = lineIn.Split(',');

                if (parts[0] == nameInput)
                {
                    int locationCode = int.Parse(parts[4]);

                    string shipLocation = locations[locationCode - 1];
                    Console.WriteLine("Ship Location : " + shipLocation);
                    shipFound = true;
                    break;
                }

                lineIn = reader.ReadLine();
            }

            if (shipFound == false)
            {
                Console.WriteLine("The ship was not found ");
            }
        }
    }
}
