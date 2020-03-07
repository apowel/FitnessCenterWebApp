using FitnessCenterWepApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitnessCenterWebApp.Models
{
    public class ClubList
    {
        public static List<Club> clubList  = new List<Club>()
            {
                new Club() {Id = 1, Name = "Grand Rapids", Address = "605 Leonard ST NW", Membership = Membership.GrandRapids},
                new Club() {Id = 2, Name = "Detroit", Address = "2918 W Davison St.", Membership = Membership.Detroit},
                new Club() {Id = 3, Name = "Djibouti", Address = "H4VW+WF Djibouti", Membership = Membership.Djibouti},
                new Club() {Id = 4, Name = "Kyoto", Address = "689 Nakagyo Ward", Membership = Membership.Kyoto},
            };
        public static void SetCurrentClub()
        {
            int decision = 0;
            while (!Int32.TryParse(Console.ReadLine(), out decision)
                || decision < 1 || decision > (ClubList.clubList.Count))
            {
                Console.Clear();
                Console.WriteLine("That was not a Valid input");
                //ClubListView.Display();
            }
            HomeController.currentClub = clubList.FirstOrDefault(e => e.Id == decision);
        }
        //Customized ToString for testing purposes.
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < clubList.Count(); i++)
            {
                s.Append(clubList[i].Name.ToString());
                if (i < clubList.Count() - 1)
                {
                    s.Append(", ");
                }
                else
                {
                    s.Append(".");
                }
            };
            return s.ToString();
        }
    }
}
