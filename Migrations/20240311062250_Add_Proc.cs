using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Dapper.Migrations
{
    public partial class Add_Proc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //select single
            migrationBuilder.Sql(@"
                Create PROC usp_GetCompany
                @CompanyId int
                AS
                BEGIN
                Select * From Companies where CompanyId = @CompanyId
                END
                GO
                ");

            //select all
            migrationBuilder.Sql(@"
                Create PROC usp_GetAllCompany
                AS
                BEGIN
                Select * From Companies
                END
                GO
                ");

            //insert
            migrationBuilder.Sql(@"
                Create PROC usp_AddCompany
                @CompanyId int OUTPUT,
                @Name varchar(MAX),
                @Address varchar(MAX),
                @City varchar(MAX),
                @State varchar(MAX),
                @PostalCode varchar(MAX) 
                AS
                BEGIN
                INSERT INTO Companies (Name,Address,City,State,PostalCode)
                VALUES (@Name,@Address,@City,@State,@PostalCode);
                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
                ");

            //update
            migrationBuilder.Sql(@"
                Create PROC usp_UpdateCompany
                @CompanyId int,
                @Name varchar(MAX),
                @Address varchar(MAX),
                @City varchar(MAX),
                @State varchar(MAX),
                @PostalCode varchar(MAX) 
                AS
                BEGIN
                UPDATE Companies SET Name = @Name,Address = @Address,City = @City,State = @State,PostalCode = @PostalCode
                WHERE CompanyId = @CompanyId
                END
                GO
                ");

            //delete
            migrationBuilder.Sql(@"
                Create PROC usp_DeleteCompany
                @CompanyId int
                AS
                BEGIN
                DELETE Companies WHERE CompanyId = @CompanyId
                END
                GO
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
