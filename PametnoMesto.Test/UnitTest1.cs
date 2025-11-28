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
        Vehicle vehicle = new Vehicle("Test1", "Pink");
        bool t1 = dataHandler.AddVehicle(vehicle) != -1;
        bool t2 = dataHandler.AddVehicle(vehicle) == -1;
        Assert.IsTrue(t1 && t2);
    }

    [TestMethod]
    public void TestDoubleRemove()
    {
        DataHandler dataHandler = new DataHandler();
        Vehicle vehicle = new Vehicle("Test1", "Pink");
        int index = dataHandler.AddVehicle(vehicle);
        bool t1 = dataHandler.RemoveVehicle(index);
        bool t2 = !dataHandler.RemoveVehicle(index);
        Assert.IsTrue(t1 && t2);
    }

    [TestMethod]
    public void TestNormalUse()
    {
        DataHandler dataHandler = new DataHandler();
        Vehicle vehicle1 = new Vehicle("Test1", "Pink");
        Vehicle vehicle2 = new Vehicle("Test2", "Pink");
        Vehicle vehicle3 = new Vehicle("Test3", "Pink");
        Vehicle vehicle4 = new Vehicle("Test4", "Pink");
        
        dataHandler.AddVehicle(vehicle1);
        dataHandler.AddVehicle(vehicle2);
        dataHandler.AddVehicle(vehicle3);
        dataHandler.AddVehicle(vehicle4);
        
        dataHandler.RemoveVehicle(2);
        
        Assert.IsTrue(dataHandler.GetVehicle(2) == vehicle4);
    }

    #endregion
}