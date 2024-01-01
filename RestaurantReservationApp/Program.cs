using RestaurantReservation.Db;
using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.IServices;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Tests.RepositoryInvokers_SampleData;

using var dbContext = new RestaurantReservationDbContext();

//#region Customer repository tests

//    var customerRepository = new CustomerRepository(dbContext);

//    var customerRepositoryTest = new CustomerRepositoryTest(customerRepository);

//    await customerRepositoryTest.TestGetAllAsync();

//    await customerRepositoryTest.TestGetByIdAsync();

//    await customerRepositoryTest.TestAddAsync();

//    await customerRepositoryTest.TestUpdateAsync();

//    await customerRepositoryTest.TestDeleteAsync();

//#endregion

//Console.ReadKey();
//Console.Clear();

//#region Menu Item repository tests

//    var menuItemRepository = new MenuItemRepository(dbContext);

//    var menuItemRepositoryTest = new MenuItemRepositoryTest(menuItemRepository);

//    await menuItemRepositoryTest.TestGetAllAsync();

//    await menuItemRepositoryTest.TestGetByIdAsync();

//    await menuItemRepositoryTest.TestAddAsync();

//    await menuItemRepositoryTest.TestUpdateAsync();

//    await menuItemRepositoryTest.TestDeleteAsync();

//#endregion

//Console.ReadKey();
//Console.Clear();

//#region OrderItem repository tests

//    var orderItemRepository = new OrderItemRepository(dbContext);

//    var orderItemRepositoryTest = new OrderItemRepositoryTest(orderItemRepository);

//    await orderItemRepositoryTest.TestGetAllAsync();

//    await orderItemRepositoryTest.TestGetByIdAsync();

//    await orderItemRepositoryTest.TestAddAsync();

//    await orderItemRepositoryTest.TestUpdateAsync();

//    await orderItemRepositoryTest.TestDeleteAsync();

//#endregion

//Console.ReadKey();
//Console.Clear();

//#region Table repository tests

//var tableRepository = new TableRepository(dbContext);

//var tableRepositoryTest = new TableRepositoryTest(tableRepository);

//await tableRepositoryTest.TestGetAllAsync();

//await tableRepositoryTest.TestGetByIdAsync();

//await tableRepositoryTest.TestAddAsync();

//await tableRepositoryTest.TestUpdateAsync();

//await tableRepositoryTest.TestDeleteAsync();

//#endregion

//Console.ReadKey();
//Console.Clear();

//#region Employee repository tests

//    IRepositoryServices<Employee, string> employeeRepository = new EmployeeRepository(dbContext);
    
//    IEmployeeServices employeeServices = new EmployeeRepository(dbContext);

//    var employeeRepositoryTest = new EmployeeRepositoryTest(employeeRepository, employeeServices);

//    await employeeRepositoryTest.TestGetAllAsync();

//    await employeeRepositoryTest.TestGetByIdAsync();

//    await employeeRepositoryTest.TestAddAsync();

//    await employeeRepositoryTest.TestUpdateAsync();

//    await employeeRepositoryTest.TestDeleteAsync();

//#endregion

//Console.ReadKey();
//Console.Clear();

//#region Restaurant repository tests

//    var restaurantRepository = new RestaurantRepository(dbContext);

//    var restaurantRepositoryTest = new RestaurantRepositoryTest(restaurantRepository);

//    await restaurantRepositoryTest.TestGetAllAsync();

//    await restaurantRepositoryTest.TestGetByIdAsync();

//    await restaurantRepositoryTest.TestAddAsync();

//    await restaurantRepositoryTest.TestUpdateAsync();

//    await restaurantRepositoryTest.TestDeleteAsync();

//#endregion

Console.ReadKey();
Console.Clear();

#region Order repository tests

    IRepositoryServices<Order, string> orderRepository = new OrderRepository(dbContext);
    
    IOrderServices orderServices = new OrderRepository(dbContext);

    var orderRepositoryTest = new OrderRepositoryTest(orderRepository, orderServices);

    await orderRepositoryTest.TestGetAllAsync();

    await orderRepositoryTest.TestGetByIdAsync();

    await orderRepositoryTest.TestAddAsync();

    await orderRepositoryTest.TestUpdateAsync();

    await orderRepositoryTest.TestDeleteAsync();

#endregion