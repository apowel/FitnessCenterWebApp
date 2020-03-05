using FitnessCenter.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.Models
{
    public class SCMember : Member
    {
        public override void CheckIn(Club club)
        {
            if (HomeController.currentMember.Membership == club.Membership)
            {
                
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
