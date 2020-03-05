using FitnessCenter.Controller;
using FitnessCenter.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FitnessCenter.Models
{
    [XmlInclude(typeof(SCMember))]
    [XmlInclude(typeof(MCMember))]
    [Serializable()]
    public abstract class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Membership Membership { get; set; }
        public int Price { get; set; }
        public int Balance { get; set; }
        public DateTime Begin { get; set; }
        public abstract void CheckIn(Club club);

        public static void MemberMenu(Member member)
        {
            string input = Console.ReadLine();
            if (input == "1")
            {
                member.Balance = 0;
                member.Begin = DateTime.Today;
                Console.WriteLine($"{member.Name} has paid their bill! Press any key to continue");
                Console.ReadKey();
                MemberDetailsView.Display();
                MemberMenu(member);
                return;
            }
            else if (input == "2")
            {
                Console.WriteLine($"If you wish to delete {HomeController.currentMember.Name} type 'yes'.");
                string decision = Console.ReadLine().ToLower().Trim();
                if (decision == "yes")
                {
                    MemberList.memberList.Remove(member);
                    Console.WriteLine($"{member.Name} has been deleted from our records.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    MemberDetailsView.Display();
                    MemberMenu(member);
                    return;
                }
            }
            else if (input == "3")
            {
                return;
            }
        }
        public static void GetNewName()
        {
            string name = "";
            name = Console.ReadLine();
            while (name == "")
            {
                Console.Clear();
                Console.WriteLine("Please type your name.");
                name = Console.ReadLine();
            }
            HomeController.currentMember.Name = name;
        }
        public static void GetNewMemberType(Club club)
        {
            string memberType = Console.ReadLine();

            if (memberType == "1")
            {
                SCMember newMember = new SCMember() { Membership = club.Membership };
                HomeController.currentMember = newMember;
            }
            else if (memberType == "2")
            {
                MCMember newMember = new MCMember() { Membership = Membership.MultiClub };
                HomeController.currentMember = newMember;
            }
            else
            {
                Console.WriteLine("That is not a valid answer, please try again.");
                Console.ReadKey();
                AddMemberView.Display(club);
            }
        }
    }
}