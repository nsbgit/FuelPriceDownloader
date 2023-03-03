# EIA U.S. Fuel Pricing Download Background Service

This project is a simple console application that implements a scheduled, repeatable task to download U.S. fuel pricing from the EIA open API and save it to a SQL Server database.

## Technical Stack
### Required
- .Net Core 6.0 or later
- SQL Server or SQL Express to store the data
- ADO.NET or any ORM library for data access
> Note: `SQL Server & Entity Framework Core` is used in this project.

### Optional
- Any third-party libraries for scheduling, downloading, and parsing results.
> Note: `Quartz.NET` (https://www.quartz-scheduler.net) is used for scheduler.
> `Newtonsoft Json.NET`  (https://www.newtonsoft.com/json) is used for handeling Json Objects
 
## Flow Description
The background service should perform the following:
- Download weekly fuel pricing data from http://api.eia.gov/series/?api_key=ec92aacd6947350dcb894062a4ad2d08&series_id=PET.EMD_EPD2D_PTE_NUS_DPG.W.
- Parse the data and extract the first series section in format [yyyyMMdd, price].
- Do not save data to the database that is older than N days, and save it to the database in the format [yyyyMMdd, price].
- Ignore records that already exist in the database. Duplicates can be checked by yyyyMMdd (record date).

## Requirements
- Create a console application that implements the flow described above.
- Allow configuration of the service with two parameters:
    - Task execution delay: Delay between background job executions.
    - Days count: Number of days after which data is considered old and should not be saved to the database.
- These parameters can be passed through app.config parameters or json settings.

## Installation 
1. Clone the repository:
    ```
    git clone https://github.com/nsbgit/FuelPriceDownloader.git
    ```
2. Open the solution in Visual Studio
3. Build the solution
4. Configure the connection string in appsettings.json to point to your SQL Server database
5. Run `FulePriceDownloader/Scripts/DbCreationScript.sql` script to create database and table.
6. Run the application

## Usage
1. Run the console application.
2. The application will run in the background, downloading and saving fuel pricing data to the database at the scheduled intervals.
3. To stop the application, press `Ctrl+C`.

## Screenshot
![Screenshot](https://github.com/nsbgit/FuelPriceDownloader/blob/master/Screenshot%202023-03-03%20170151.png)


## Contributors
This project was created by Sukanta Sharma.

## License

MIT
