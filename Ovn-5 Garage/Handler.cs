using Ovn_5_Garage.Garage;
using Ovn_5_Garage.Garage.Vehicles;
using Ovn_5_Garage.UserInterface;

namespace Ovn_5_Garage // Handler sköter anrop och validering.
{
    internal class Handler
    {
        readonly InputGarage Inputg = new(); // Andra klasser som Handler jobbar intimt med.
        readonly InputVehicles Inputv = new();
        readonly InputListSearch Inputl = new();

        string? getout = ""; // Ger möjlighet att hoppa ut/återgå med enter eller med "quit" enligt Inputklassen.
        public string Arrayname { get; set; } = ""; // Det valda namnet för garaget.
        public int Arraysize { get; set; } = 0; // Den valda kapaciteten för garaget.
        public int rak { get; set; } = 0; // Håller reda på antalet inlagda fordon. Mycket viktig (styr allt)!
        bool[]? seq; // Sekvens är beroende på fordon, vilka storheter som ska tas med resp. inte tas med.
        int number; // Global invariabel.
        byte bytnum; // Global bytevariabel.
        short shonum; // Global shortvariabel.
        byte Yp1 = 19; // Y-positionen för inputs.
        byte Yp2 = 22; // Y-positionen för fel och varningar.

        public Garage<Vehicle> garage { get; set; }

        public Handler()
        {
        }

        public void Choice_NewGarage() // För initiering av ett nytt garage.
        {
            getout = "null";

            getout = Inputg.InputAlternative1();
            string a_name = getout;
            if (getout != "") (getout, number) = Inputg.InputAlternative2();

            if (getout != "" && a_name.Length > 2 && number > 2)
            {
                if (Arraysize > 0)
                {
                    CC.ClearIn(0, Yp2 - 1, 65, 1, 0, true);
                    CC.WY($"The content in the garage {Arrayname} will be deletet...");
                    CC.W($"Type \"yes\" to proceed: ");
                    string str = CC.RL();

                    if (str == "yes")
                    {
                        Arrayname = a_name;
                        Arraysize = number;
                        rak = 0;
                        garage = new Garage<Vehicle>(Arraysize); // Garage ges ny kapacitet.
                        MenuUI.Str = Arrayname;
                        MenuUI.Cap = Arraysize;
                        MenuUI.Inr = rak;

                        CC.ClearIn(0, Yp2 - 1, 65, 2, 0, true);
                        CC.WB($"A garage with the name {Arrayname} with {Arraysize} spaces is ready to be used");
                        CC.RL();
                        rak = 0;
                    }
                    else
                    {
                        CC.ClearIn(0, Yp2 - 1, 65, 2, 0, true);
                        CC.WY($"No garage was initiated...");
                        CC.RL();
                    }

                }
                else
                {
                    Arrayname = a_name;
                    Arraysize = number;
                    rak = 0;
                    garage = new Garage<Vehicle>(Arraysize); // Garage ges ny kapacitet.
                    MenuUI.Str = Arrayname;
                    MenuUI.Cap = Arraysize;
                    MenuUI.Inr = rak;

                    CC.ClearIn(0, Yp2 - 1, 65, 2, 0, true);
                    CC.WB($"A garage with the name {Arrayname} with {Arraysize} spaces is ready to be used");
                    CC.RL();
                    rak = 0;
                }
            }
            else
            {
                CC.ClearIn(0, Yp2 - 1, 65, 2, 0, true);
                CC.WY($"No garage was initiated...");
                CC.RL();
            }
        }

        public void Choice_Add() // För inmatning av fordon.
        {
            getout = "null";

            if (Arrayname.Length > 2 && rak < Arraysize) // Obligatoriska indata: Typ, Regnummer, antal hjul, antal säten och färg.
            {                                            // Extra: Brand, Length, Weight, Engine, Cylvol, Fuel...
                (getout, seq) = Inputv.InputAlternative1();
                string v_type = getout;
                if (getout != "quit") getout = Inputv.InputAlternative2();
                string v_reg = getout;
                if (getout != "quit") (getout, bytnum) = Inputv.InputAlternative3();
                byte v_wheel = bytnum;
                if (getout != "quit") (getout, shonum) = Inputv.InputAlternative4();
                short v_seat = shonum;
                if (getout != "quit") getout = Inputv.InputAlternative5();
                string v_color = getout;
                if (getout != "quit" && seq[0]) getout = Inputv.InputAlternative6();
                string v_brand = getout;
                if (getout != "quit" && seq[1]) (getout, shonum) = Inputv.InputAlternative7();
                short v_length = bytnum;
                if (getout != "quit" && seq[2]) (getout, number) = Inputv.InputAlternative8();
                int v_weight = number;
                if (getout != "quit" && seq[3]) (getout, bytnum) = Inputv.InputAlternative9();
                byte v_engine = bytnum;
                if (getout != "quit" && seq[4]) (getout, number) = Inputv.InputAlternative10();
                int v_cylvol = number;
                if (getout != "quit" && seq[5]) getout = Inputv.InputAlternative11();
                string v_fuel = getout;

                if (getout != "quit")
                {
                    if (v_type == "Car")
                    {
                        garage.Add(new Car(v_reg, v_wheel, v_seat, v_color, v_brand, v_length, v_weight, v_engine, v_cylvol, v_fuel));
                    }
                    if (v_type == "Motorcycle")
                    {
                        garage.Add(new Motorcycle(v_reg, v_wheel, v_seat, v_color, v_brand, v_cylvol, v_fuel));
                    }
                    if (v_type == "Bus")
                    {
                        garage.Add(new Bus(v_reg, v_wheel, v_seat, v_color, v_brand, v_length, v_weight, v_engine, v_cylvol, v_fuel));
                    }
                    if (v_type == "Boat")
                    {
                        garage.Add(new Boat(v_reg, v_wheel, v_seat, v_color, v_brand, v_length, v_weight, v_engine, v_fuel));
                    }
                    if (v_type == "Airplane")
                    {
                        garage.Add(new Airplane(v_reg, v_wheel, v_seat, v_color, v_length, v_weight, v_engine, v_fuel));
                    }
                    if (v_type == "quit") // Extra.
                    {
                        garage.Add(new Car(v_reg, v_wheel, v_seat, v_color, v_brand, v_length, v_weight, v_engine, v_cylvol, v_fuel));
                    }
                    if (v_type == "quit") // Extra.
                    {
                        garage.Add(new Car(v_reg, v_wheel, v_seat, v_color, v_brand, v_length, v_weight, v_engine, v_cylvol, v_fuel));
                    }
                    rak++;
                    MenuUI.Inr = rak;

                    CC.ClearIn(0, Yp1 - 4, 110, 8, 0, true);
                    CC.WB($"Your vehicle was successfully parked");
                    CC.RL();
                }
                else
                {
                    CC.ClearIn(0, Yp2 - 4, 110, 3, 0, true);
                    CC.WY($"You chose to quit - no vehicle was parked...");
                    CC.RL();
                }
            }
            else
            {
                CC.ClearIn(0, Yp1 - 4, 110, 1, 0, true);
                if (Arraysize < 1) CC.WY($"There is no garage or any capacity declared yet...");
                else CC.WY($"You try to add vehicles beyond the capacity for this garage...");
                CC.RL();
            }            
        }

        public void Choice_Remove()
        {
            {
                string str = "";
                int irak = 0;

                if (rak > 0)
                {
                    foreach (var item1 in garage) // Skriver ut alla.
                    {
                        CC.ClearIn(0, Yp1 - 4, 70, 15, 0, false);
                        CC.WR(" " + (irak + 1) + " of " + rak + "\n" + item1.VehicleInfo());
                        irak++;
                        if (irak == rak) CC.WY("\nThat was the last one...");
                        CC.WG("\nQuit? or type \"rem\" to REMOVE this vehicle. Press ENTER to proceed...");
                        str = CC.RL();
                        if (str == "quit" || str == "rem") break;
                    }

                    if (str == "rem")
                    {
                        garage.Remove(irak - 1);
                        rak--;
                        MenuUI.Inr = rak;
                        CC.ClearIn(0, Yp1 - 4, 70, 15, 0, false);
                        CC.WB($"The vehicle was successfully removed...");
                        CC.RL();
                    }
                }
                else
                {
                    CC.ClearIn(0, Yp1 - 4, 70, 1, 0, false);
                    CC.WY($"The garage is empty...");
                    CC.RL();
                }
            }
        }

        public void Choice_ListAll()
        {
            string str = "";
            int irak = 0;

            if (rak > 0)
            {
                foreach (var item in garage) // Skriver ut alla.
                {
                    CC.ClearIn(0, Yp1, 45, 15, 0, false);
                    CC.WR(" " + (irak + 1) + " of " + rak + "\n" + item.VehicleInfo());
                    irak++;
                    if (irak == rak) CC.WY("\nThat was the last one...");
                    CC.WG("\nQuit? or press ENTER to proceed...");
                    str = CC.RL();
                    if (str == "quit") break;
                }
            }
            else
            {
                CC.ClearIn(0, Yp1, 45, 1, 0, false);
                CC.WY($"The garage is empty...");
                CC.RL();
            }
        }

        public void Choice_Search()
        {
            getout = "null";
            getout = Inputl.InputAlternative1();
            string[] s_alla = getout.Split("+");
            List<string> samples = new(); // Lista för uppsamling av funna sökningar.
            samples.Clear(); // Nollställer listan.

            int irak = 0;

            if (rak > 0 && getout != "")
            {
                foreach (var item in garage) // Dubbla for som söker igenom, dels fordonen i garaget och dels fyra egenskaper.
                {
                    for (int i = 0; i < s_alla.Length; i++)
                    {
                        if (item.GetType().Name.ToLower() == s_alla[i].ToLower() ||
                        item.Reg.ToLower() == s_alla[i].ToLower() ||
                        item.Wheel.ToString() == s_alla[i].ToLower() ||
                        item.Seat.ToString() == s_alla[i].ToLower() ||
                        item.Vcolor.ToLower() == s_alla[i].ToLower())
                        {
                            samples.Add(item.VehicleInfo());
                        }
                    }
                }

                foreach (var item2 in samples) // Skriver ut de funna sökningar via samples.
                {
                        CC.ClearIn(0, Yp1, 110, 15, 0, false);
                        CC.WR(" " + (irak + 1) + " of " + samples.Count + "\n" + item2);
                        irak++;
                        if (irak == samples.Count) CC.WY("\nThat was the last one...");
                        CC.WG("\nQuit? or press ENTER to proceed...");
                        getout = CC.RL();

                    if (getout == "quit") break;

                }

                if (irak == 0)
                {
                    CC.ClearIn(0, Yp1, 110, 3, 0, false);
                    CC.WY($"No items were found...");
                    CC.RL();
                }
            }
            else
            {
                if (rak < 1)
                {
                    CC.ClearIn(0, Yp1, 110, 3, 0, false);
                    CC.WY($"The garage is empty...");
                    CC.RL();
                }
            }
        }

        public void Choice_ListType()
        {
            getout = "null";
            string v_type = Inputl.InputAlternative3();
            getout = v_type;
            
            int irak = 0;
            int rak2 = 0;

            if (rak > 0 && getout != "")
            {
                foreach (var item1 in garage) // Kollar hur många typer det finns och tilldelar antalet till rak2.
                {                             // Annars vet man inte totalen.
                    if (item1.GetType().Name == v_type) rak2++;
                }

                foreach (var item2 in garage) // Repeterar och skriver ut dem.
                {
                    if (item2.GetType().Name == v_type)
                    {
                        CC.ClearIn(0, Yp1, 110, 15, 0, false);
                        CC.WR(" " + (irak + 1) + " of " + rak2 + "\n" + item2.VehicleInfo());
                        irak++;
                        if (irak == rak2) CC.WY("\nThat was the last one...");
                        CC.WG("\nQuit? or press ENTER to proceed...");
                        getout = CC.RL();
                    }                                                
                    if (getout == "quit") break;

                }

                if (irak == 0)
                {
                    CC.ClearIn(0, Yp1, 110, 3, 0, false);
                    CC.WY($"No items were found...");
                    CC.RL();
                }
            }
            else
            {
                if (rak < 1)
                {
                    CC.ClearIn(0, Yp1, 110, 3, 0, false);
                    CC.WY($"The garage is empty...");
                    CC.RL();
                }
            }
        }

        public void Choice_SearchRegNumber()
        {
            getout = "null";
            string v_reg = Inputl.InputAlternative4();
            getout = v_reg;

            int irak = 0;
            int rak2 = 0;
            int rak3 = 0;

            if (rak > 0 && getout != "")
            {
                foreach (var item1 in garage) // Kollar hur många regnummer det finns och tilldelar antalet till rak2.
                {                             // Annars vet man inte totalen.
                    if (item1.Reg.ToLower() == v_reg.ToLower()) rak2++;                    
                }

                foreach (var item2 in garage) // Repeterar och skriver ut dem.
                {
                    if (item2.Reg.ToLower() == v_reg.ToLower())
                    {
                        CC.ClearIn(0, Yp1, 110, 18, 0, false);
                        CC.WR(" " + (irak + 1) + " of " + rak2 + "\n" + item2.VehicleInfo());
                        irak++;
                        if (irak == rak2) CC.WY("\nThat was the last one...");
                        CC.WG("\nQuit? or type \"rem\" to REMOVE this vehicle. Press ENTER to proceed...");
                        getout = CC.RL();
                    }
                    rak3++;
                    if (getout == "quit" || getout == "rem") break;

                }

                if (getout == "rem")
                {
                    garage.Remove(rak3 - 1);
                    rak--;
                    MenuUI.Inr = rak;
                    CC.ClearIn(0, Yp1, 110, 18, 0, false);
                    CC.WB($"The vehicle was successfully removed...");
                    CC.RL();
                }

                if (irak == 0)
                {
                    CC.ClearIn(0, Yp1, 110, 3, 0, false);
                    CC.WY($"No items were found...");
                    CC.RL();
                }
            }
            else
            {
                if (rak < 1)
                {
                    CC.ClearIn(0, Yp1, 110, 3, 0, false);
                    CC.WY($"The garage is empty...");
                    CC.RL();
                }
            }
        }

        public void Choice_Populate1() // Seeding.
        {
            if (Arraysize == 0)
            {
                Arraysize= 20;
                Arrayname = "CityNorth";
                garage = new Garage<Vehicle>(Arraysize); // Garage ges ny kapacitet.
                rak = 0;
                garage.Add(new Car("BIL111", 4, 4, "Gray", "KIA", 3, 1100, 1, 1600, "Gasoline/Petrol"));
                rak++;
                garage.Add(new Car("RED222", 4, 4, "Red", "Volvo", 3, 1400, 1, 1800, "Diesel/Kerosene"));
                rak++;
                garage.Add(new Car("BAD333", 4, 4, "Black", "Merzedes", 3, 3550, 1, 2100, "Gasoline/Petrol"));
                rak++;
                garage.Add(new Car("BIO444", 4, 4, "Green", "SAAB", 3, 1300, 1, 1600, "Biofuel"));
                rak++;
                garage.Add(new Car("E L O N", 4, 4, "Blue", "Tesla", 3, 2200, 1, 0, "Battery/Hybride"));
                rak++;
                garage.Add(new Bus("BUS555", 6, 32, "White", "Scania", 12, 5500, 1, 3800, "Diesel/Kerosene"));
                rak++;
                garage.Add(new Motorcycle("YAM666", 2, 1, "Black", "Yamaha", 1100, "Gasoline/Petrol"));
                rak++;
                garage.Add(new Boat("DAS096", 2, 6, "Gray", "Cresent", 4, 1200, 2, "Gas/H2e"));
                rak++;
                MenuUI.Str = Arrayname;
                MenuUI.Cap = Arraysize;
                MenuUI.Inr = rak;
                CC.ClearIn(0, Yp2 - 7, 65, 1, 0, true);
                CC.WB($"Garage {Arrayname} has been seeded with {rak} vehicles");
                CC.RL();
            }
            else
            {
                CC.ClearIn(0, Yp2 - 7, 65, 1, 0, true);
                CC.WY($"The garage must be null and empty before populating...");
                CC.RL();
            }
        }

        public void Choice_Populate2() // Seeding.
        {
            if (Arraysize == 0)
            {
                Arraysize = 1;
                Arrayname = "SkyPark";
                garage = new Garage<Vehicle>(Arraysize); // Garage ges ny kapacitet.
                rak = 0;
                garage.Add(new Airplane("SE 55", 6, 12, "White", 8, 1300, 2, "Diesel/Kerosene"));
                rak++;
                MenuUI.Str = Arrayname;
                MenuUI.Cap = Arraysize;
                MenuUI.Inr = rak;
                CC.ClearIn(0, Yp2 - 7, 65, 1, 0, true);
                CC.WB($"Garage {Arrayname} has been seeded with {rak} vehicles");
                CC.RL();
            }
            else
            {
                CC.ClearIn(0, Yp2 - 7, 65, 1, 0, true);
                CC.WY($"The garage must be null and empty before populating...");
                CC.RL();
            }
        }
    }
}
