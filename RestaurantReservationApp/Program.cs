﻿using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Tests.RepositoryInvokers_SampleData;

using var dbContext = new RestaurantReservationDbContext();

#region Customer repository tests

    var customerRepository = new CustomerRepository(dbContext);

    var customerRepositoryTest = new CustomerRepositoryTest(customerRepository);

    await customerRepositoryTest.TestGetAllAsync();

    await customerRepositoryTest.TestGetByIdAsync();

    await customerRepositoryTest.TestAddAsync();

    await customerRepositoryTest.TestUpdateAsync();

    await customerRepositoryTest.TestDeleteAsync();

#endregion

Console.ReadKey();
Console.Clear();

#region Menu Item repository tests

    var menuItemRepository = new MenuItemRepository(dbContext);

    var menuItemRepositoryTest = new MenuItemRepositoryTest(menuItemRepository);

    await menuItemRepositoryTest.TestGetAllAsync();

    await menuItemRepositoryTest.TestGetByIdAsync();

    await menuItemRepositoryTest.TestAddAsync();

    await menuItemRepositoryTest.TestUpdateAsync();

    await menuItemRepositoryTest.TestDeleteAsync();

#endregion

Console.ReadKey();
Console.Clear();

#region OrderItem repository tests

    var orderItemRepository = new OrderItemRepository(dbContext);

    var orderItemRepositoryTest = new OrderItemRepositoryTest(orderItemRepository);

    await orderItemRepositoryTest.TestGetAllAsync();

    await orderItemRepositoryTest.TestGetByIdAsync();

    await orderItemRepositoryTest.TestAddAsync();

    await orderItemRepositoryTest.TestUpdateAsync();

    await orderItemRepositoryTest.TestDeleteAsync();

#endregion

Console.ReadKey();
Console.Clear();

#region Table repository tests

    var tableRepository = new TableRepository(dbContext);

    var tableRepositoryTest = new TableRepositoryTest(tableRepository);

    await tableRepositoryTest.TestGetAllAsync();

    await tableRepositoryTest.TestGetByIdAsync();

    await tableRepositoryTest.TestAddAsync();

    await tableRepositoryTest.TestUpdateAsync();

    await tableRepositoryTest.TestDeleteAsync();

#endregion
