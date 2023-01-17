using APIThingOrWhateverIdk.Controllers;
using APIThingOrWhateverIdk.Models;

namespace APIThingOrWhateverIdkTest.Controllers
{
    [TestClass]
    public class DatabaseControllerTest
    {

        [TestMethod]
        public void Test_InitializeDatabaseTables()
        {
            Assert.IsTrue(DatabaseController.InitializeDatabaseTables());
        }

        [TestMethod]
        public void Test_GetPersons()
        {
            List<PersonModel> persons = DatabaseController.GetAllPersons();
            Assert.IsNotNull(persons);
        }

        [TestMethod]
        public void Test_IsTestRunning()
        {
            Assert.IsTrue(true);
        }
    }
}