using FitnessCenterWepApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FitnessCenterWebApp.Models
{
    [XmlInclude(typeof(Member))]
    [XmlInclude(typeof(List<Member>))]
    [Serializable()]
    public class MemberList
    {
        public static List<Member> memberList = new List<Member>()
        {
            new Member() { Id = 1234, Name = "Andrew", Price = 10, Membership = Membership.GrandRapids, Begin = new DateTime(2020, 01, 01) },
            new Member() { Id = 7138, Name = "Austin", Price = 10, Membership = Membership.Djibouti, Begin = new DateTime(2019, 04, 20) },
            new Member() { Id = 3825, Name = "Tommy", Price = 20, Membership = Membership.MultiClub, Begin = new DateTime(2015, 08, 28) },
            new Member() { Id = 2678, Name = "Naruto", Price = 10, Membership = Membership.Kyoto, Begin = new DateTime(2017, 03, 17) }
        };
        public static void Signup(Member member)
        {
            // make validation to prevent duplicate Id's.
            Random id = new Random();
            member.Id = id.Next(1000, 9999);
            memberList.Add(member);
        }
        public static void Remove(Member member)
        {
            memberList.Remove(member);
        }
        public static Member GetMember(string name)
        {
            return memberList.FirstOrDefault(e => e.Name == name);
        }
        public static void GetBalance()
        {
            int monthResult = (DateTime.Today.Month - HomeController.currentMember.Begin.Month) 
                + 12 * (DateTime.Today.Year - HomeController.currentMember.Begin.Year);
            HomeController.currentMember.Balance = monthResult * HomeController.currentMember.Price;
        }
        public static void GetMember()
        {
            int id = 0;
            while (!Int32.TryParse(Console.ReadLine(), out id)
                || id < 1000|| id > 9999)
            {
                Console.Clear();
                if (id == 0)
                {
                    throw new Exception();
                }
                Console.WriteLine("That is not a valid Id number.");
                try
                {
                    //MemberListView.Display(MemberList.GetMembersOf(HomeController.currentClub.Membership));
                }
                catch (Exception)
                {
                    //MemberListView.Display(MemberList.memberList);
                }
            }
            HomeController.currentMember =  memberList.FirstOrDefault(e => e.Id == id);
        }

        //returns a list of members of a gym and includes multiclub members.
        public static List<Member> GetMembersOf(Membership m)
        {
            return memberList.Where(e => e.Membership == m || 
            e.Membership == Membership.MultiClub).ToList();
        }

        // add file.io saving implemenation here?


        //Customized ToString for testing purposes.
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < memberList.Count(); i++)
            {
                s.Append(memberList[i].Name);
                if (i < memberList.Count() - 1)
                {
                    s.Append(", ");
                }
                else
                {
                    s.Append(".");
                }
            }
            return s.ToString();
        }
    }
}