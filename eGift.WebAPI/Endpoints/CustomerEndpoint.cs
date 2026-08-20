using eGift.WebAPI.Data;
using eGift.WebAPI.Dtos;
using eGift.WebAPI.Mappings;
using Microsoft.EntityFrameworkCore;

namespace eGift.WebAPI.Endpoints;

public static class CustomerEndpoint
{
    public static RouteGroupBuilder MapCustomerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/customer").WithTags("Customer");

        #region Default CRUD Endpoints

        // GET: api/customer
        group.MapGet("/", async (AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CustomerEndpoint");

            try
            {
                var customers = await (
                    from customer in context.Customers
                    join gender in context.Genders on customer.GenderId equals gender.Id
                    join role in context.Roles on customer.RoleId equals role.Id
                    join address in context.Addresses on customer.AddressId equals address.Id
                    join city in context.Cities on address.CityId equals city.Id
                    join state in context.States on address.StateId equals state.Id
                    join country in context.Countries on address.CountryId equals country.Id
                    where !customer.IsDeleted
                    select new
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        DateofBirth = customer.DateofBirth,
                        Age = CalculateAgeInYears(customer.DateofBirth),
                        GenderName = gender.GenderName,
                        Mobile = customer.Mobile,
                        Email = customer.Email,
                        IsActive = customer.IsActive,
                        RoleName = role.RoleName,
                        IsDefault = customer.IsDefault,
                        CreatedDate = customer.CreatedDate
                    }
                )
                .AsNoTracking()
                .ToListAsync();

                return customers is null ? Results.NotFound() : Results.Ok(customers);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                logger.LogError(
                    "Exception in CustomerEndpoint: /api/customer GET: {Message}.",
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = "An error occurred while retrieving customers.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // GET: api/customer/{id}
        group.MapGet("/{id:int}", async (int id, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CustomerEndpoint");

            try
            {
                var customer = await (
           from c in context.Customers
           join gender in context.Genders on c.GenderId equals gender.Id
           join role in context.Roles on c.RoleId equals role.Id
           join address in context.Addresses on c.AddressId equals address.Id
           join city in context.Cities on address.CityId equals city.Id
           join state in context.States on address.StateId equals state.Id
           join country in context.Countries on address.CountryId equals country.Id
           join login in context.Logins on c.Id equals login.RefId into loginGroup

           from login in loginGroup.DefaultIfEmpty()
           where c.Id == id && !c.IsDeleted
           select new
           {
               Id = c.Id,
               FirstName = c.FirstName,
               LastName = c.LastName,
               DateofBirth = c.DateofBirth,
               Age = CalculateAgeInYears(c.DateofBirth),

               GenderId = c.GenderId,
               GenderName = gender.GenderName,

               Mobile = c.Mobile,
               Email = c.Email,

               AddressId = c.AddressId,
               FullAddress = address.Street1 + ", " + city.CityName + ", " + state.StateName + ", " + country.CountryName + " - " + address.PinCode,

               IsActive = c.IsActive,
               ProfileImagePath = c.ProfileImagePath,
               ProfileImageData = c.ProfileImageData,

               RoleId = c.RoleId,
               RoleName = role.RoleName,

               IsDefault = c.IsDefault,
               CreatedDate = c.CreatedDate,
               LastLogin = login != null
                ? login.LastLoginDate : null
           }
       )
       .AsNoTracking()
       .FirstOrDefaultAsync();

                return customer is null ? Results.NotFound() : Results.Ok(customer);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                logger.LogError(
                    "Exception in CustomerEndpoint: /api/customer/{id} GET: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while retrieving the customer with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // POST: api/customer
        group.MapPost("/", async (CustomerDto dto, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CustomerEndpoint");

            try
            {
                var customer = dto.ToEntity();

                context.Customers.Add(customer);
                await context.SaveChangesAsync();

                return Results.Created(
                    $"/api/customer/{customer.Id}", customer
                );
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                logger.LogError(
                    "Exception in CustomerEndpoint: /api/customer POST: {Message}.",
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = "An error occurred while creating the customer.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // PUT: api/customer/{id}
        group.MapPut("/{id:int}", async (int id, EditCustomerDto dto, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CustomerEndpoint");

            try
            {
                var existingCustomer = await context.Customers.FindAsync(id);

                if (existingCustomer is null)
                {
                    return Results.NotFound();
                }

                existingCustomer.ToEntity(dto);

                context.Customers.Update(existingCustomer);
                await context.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                logger.LogError(
                    "Exception in CustomerEndpoint: /api/customer/{id} PUT: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while updating the customer with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        // DELETE: api/customer/{id}?loginUserId={loginUserId}&deletedDate={deletedDate}
        group.MapDelete("/{id:int}", async (int id, int loginUserId, DateTime deletedDate, AppDBContext context, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("CustomerEndpoint");

            try
            {
                var existingCustomer = await context.Customers.FindAsync(id);

                if (existingCustomer is null)
                {
                    return Results.NotFound();
                }

                existingCustomer.IsDeleted = true;
                existingCustomer.UpdatedBy = loginUserId;
                existingCustomer.UpdatedDate = deletedDate;

                context.Customers.Update(existingCustomer);
                await context.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                logger.LogError(
                    "Exception in CustomerEndpoint: /api/customer/{id} DELETE: {Message}.",
                    id,
                    ex.Message
                );

                return Results.Json(
                    new
                    {
                        Message = $"An error occurred while deleting the customer with ID {id}.",
                        error = ex.Message
                    },
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        });

        #endregion

        return group;
    }

    #region Private Methods
    // To calculate age in year from date of birth
    private static int CalculateAgeInYears(DateTime dateOfBirth)
    {
        int age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
    #endregion
}