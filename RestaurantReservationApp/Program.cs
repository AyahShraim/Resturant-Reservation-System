using RestaurantReservation.Db;
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

