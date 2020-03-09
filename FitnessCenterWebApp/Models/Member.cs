using FitnessCenterWepApp.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace FitnessCenterWebApp.Models
{
    [XmlInclude(typeof(Member))]
    [Serializable()]
    public class Member
    {
        public int Id { get; set; }
        public bool Multiclub { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public Membership Membership { get; set; }
        public int Price { get; set; }
        public int Balance { get; set; }
        public DateTime Begin { get; set; }
        public void CheckIn(Club club) 
        {
            if (this.Membership == Membership.MultiClub)
            {
                this.Points++;
            }
            HomeController.currentMember = this;
            MemberList.GetBalance();
        }
        public int Points { get; set; }

        public Member()
        {
            if (this.Membership == Membership.MultiClub)
            {
                this.Multiclub = true;
            }
        }

        public static void MemberMenu(Member member)
        {
            string input = Console.ReadLine();
            if (input == "1")
            {
                member.Balance = 0;
                member.Begin = DateTime.Today;
                Console.WriteLine($"{member.Name} has paid their bill! Press any key to continue");
                Console.ReadKey();
                //MemberDetailsView.Display();
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
                    //MemberDetailsView.Display();
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
        
    }
}