namespace PametnoMesto.Test;
using Scripts;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.IsTrue(true);
    }

    #region Storage & Vehicle Tests

    [TestMethod]
    public void TestDuplicateAdd()
    {
        DataHandler dataHandler = new DataHandler();
    
        int id1 = dataHandler.AddVehicle("Test1", "Pink", VehicleType.Car);
        int id2 = dataHandler.AddVehicle("Test1", "Pink", VehicleType.Car); // duplicate name
    
        // They should have different IDs
        Assert.AreNotEqual(id1, id2);

        // Optionally: check that both vehicles exist
        Assert.AreEqual(2, dataHandler.GetVehicles().Count);
    }


    [TestMethod]
    public void TestDoubleRemove()
    {
        DataHandler dataHandler = new DataHandler();
        int id = dataHandler.AddVehicle("Test1", "Pink", VehicleType.Car);

        bool t1 = dataHandler.RemoveVehicle(id);
        bool t2 = !dataHandler.RemoveVehicle(id);

        Assert.IsTrue(t1 && t2);
    }


    [TestMethod]
    public void TestNormalUse()
    {
        DataHandler dataHandler = new DataHandler();
    
        int id1 = dataHandler.AddVehicle("Test1", "Pink", VehicleType.Car);
        int id2 = dataHandler.AddVehicle("Test2", "Pink", VehicleType.Car);
        int id3 = dataHandler.AddVehicle("Test3", "Pink", VehicleType.Car);
        int id4 = dataHandler.AddVehicle("Test4", "Pink", VehicleType.Car);

        dataHandler.RemoveVehicle(id3);

        // id4 should still exist
        var vehicle4 = dataHandler.GetVehicle(id4);
        Assert.IsNotNull(vehicle4);
        Assert.AreEqual("Test4", vehicle4.Name);

        // id3 should be gone
        Assert.IsNull(dataHandler.GetVehicle(id3));
    }


    #endregion
}