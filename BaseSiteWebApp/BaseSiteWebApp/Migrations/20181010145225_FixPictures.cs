using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseSiteWebApp.Migrations
{
    public partial class FixPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("alter table [Northwind].[dbo].[Categories] alter column [Picture] varbinary(max)");
            migrationBuilder.Sql("update [Northwind].[dbo].[Categories] set Picture = CONVERT(varbinary(max), SUBSTRING(cast(Picture as varchar(max)), 79, DATALENGTH(Picture) - 79))");
        }
    }
}
