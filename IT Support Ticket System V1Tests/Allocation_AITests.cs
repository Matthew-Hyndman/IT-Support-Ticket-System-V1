using Microsoft.VisualStudio.TestTools.UnitTesting;
using IT_Support_Ticket_System_V1;
using System;

namespace IT_Support_Ticket_System_V1.Tests
{
    [TestClass()]
    public class Allocation_AITests
    {

        [TestMethod()]
        public void pick_Contractor_Test1()
        {
            //tests if the ai can register new words
            Allocation_AI ai = new Allocation_AI();
            ai.server = "DESKTOP-NSHTTAL";
            ai.populate();
            try
            {
                int check = ai.pick_Contractor("The printer is not working and is out of paper.");
                Assert.AreEqual(10000, check, "Check was " + check);
                
            }
            catch (Exception e)
            {
                Assert.Fail("\n\nThere was an error: \n" + e.Message + "\n");
            }

        }

        [TestMethod()]
        public void pick_Contractor_Test2()
        {
            //tests if the ai can register reconised words
            Allocation_AI ai = new Allocation_AI();
            ai.server = "DESKTOP-NSHTTAL";
            ai.populate();
            try
            {
                int check = ai.pick_Contractor("The server is chrashing.");
                Assert.AreEqual(10001, check, "Check was " + check);

            }
            catch (Exception e)
            {
                Assert.Fail("\n\nThere was an error: \n" + e.Message + "\n");
            }

        }

    }
}