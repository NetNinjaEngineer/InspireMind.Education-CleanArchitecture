using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InspireMind.Education.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seed20NewCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseName", "Duration", "TopicId" },
                values: new object[,]
                {
                    { new Guid("0b873a94-4b77-4c08-b7a4-6f3b816d0e91"), "Azure Fundamentals", 30, new Guid("7db2ed45-a087-4e00-b804-b944f400f450") },
                    { new Guid("16daf9cb-a94e-4c3e-8a84-47fb0cf5917d"), "Google Cloud Platform Essentials", 35, new Guid("7db2ed45-a087-4e00-b804-b944f400f450") },
                    { new Guid("30fe5c0c-7db5-4f7c-b3a4-21711b3a36b7"), "Rust for Systems Programming", 50, new Guid("9b0418c2-d8c8-46fb-bd0d-0094f83ad563") },
                    { new Guid("3b28dfaa-03b3-4382-a1a9-3c8c8fe5682b"), "Docker & Kubernetes", 40, new Guid("7db2ed45-a087-4e00-b804-b944f400f450") },
                    { new Guid("6cff4994-907a-46aa-8fc7-3e5976c5b4a1"), "Penetration Testing", 40, new Guid("62841cba-863b-4816-9366-e789646ca43e") },
                    { new Guid("7559534a-0c8b-4e2a-85f8-ffcbf3d16a1e"), "Natural Language Processing", 55, new Guid("71fd7466-e4d4-41f6-ace8-ed67ea8fafcf") },
                    { new Guid("7a5f4a5a-b46e-41a3-8e68-00b378b0e7db"), "Swift Programming", 35, new Guid("b9dd9a87-4852-434e-99dd-3103f7fba183") },
                    { new Guid("88b07cb4-74ec-4d79-9e77-b0fbbc7f914c"), "Angular Development", 40, new Guid("b16ea527-9f9c-4c58-8385-a2f69e5c83d9") },
                    { new Guid("902bfb23-70a0-4a55-b8c0-432cea6f7d1c"), "Augmented Reality Development", 45, new Guid("b9dd9a87-4852-434e-99dd-3103f7fba183") },
                    { new Guid("9ac899b5-9e60-4d44-b202-8fdc85cfb77c"), "Big Data with Hadoop", 50, new Guid("71fd7466-e4d4-41f6-ace8-ed67ea8fafcf") },
                    { new Guid("9b7d74e9-a0a7-4d6c-a27e-7f6eed4ba1cb"), "Golang Programming", 45, new Guid("9b0418c2-d8c8-46fb-bd0d-0094f83ad563") },
                    { new Guid("a0a38f9a-8d3c-4f88-9092-48d0eb7764b3"), "Kotlin for Android Development", 40, new Guid("b9dd9a87-4852-434e-99dd-3103f7fba183") },
                    { new Guid("a318f207-098d-46b9-bb8d-5b96b3a04d35"), "Data Visualization with Tableau", 25, new Guid("71fd7466-e4d4-41f6-ace8-ed67ea8fafcf") },
                    { new Guid("ace2bce9-6c9d-47f8-995d-6d8bca51d5c2"), "AWS Cloud Practitioner", 35, new Guid("7db2ed45-a087-4e00-b804-b944f400f450") },
                    { new Guid("acee13c3-c20c-4b4c-ad19-4a4d9868424d"), "PHP for Web Development", 30, new Guid("b16ea527-9f9c-4c58-8385-a2f69e5c83d9") },
                    { new Guid("d8496fc3-8e36-4c99-875d-53d7c2e156d2"), "Artificial Neural Networks", 50, new Guid("71fd7466-e4d4-41f6-ace8-ed67ea8fafcf") },
                    { new Guid("db63e2d8-7fec-4f79-bc1e-50c6728192c3"), "Ethical Hacking", 45, new Guid("62841cba-863b-4816-9366-e789646ca43e") },
                    { new Guid("deea5689-78c2-42b4-a287-52e66394a219"), "Virtual Reality Development", 45, new Guid("b9dd9a87-4852-434e-99dd-3103f7fba183") },
                    { new Guid("f6f5d3eb-db8b-4e99-b0df-9afc4b7f1c48"), "Ruby on Rails", 45, new Guid("b16ea527-9f9c-4c58-8385-a2f69e5c83d9") },
                    { new Guid("fc563f61-c90d-4f1a-9e5b-8c9efdf59323"), "Vue.js Fundamentals", 35, new Guid("b16ea527-9f9c-4c58-8385-a2f69e5c83d9") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("0b873a94-4b77-4c08-b7a4-6f3b816d0e91"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("16daf9cb-a94e-4c3e-8a84-47fb0cf5917d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("30fe5c0c-7db5-4f7c-b3a4-21711b3a36b7"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("3b28dfaa-03b3-4382-a1a9-3c8c8fe5682b"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("6cff4994-907a-46aa-8fc7-3e5976c5b4a1"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7559534a-0c8b-4e2a-85f8-ffcbf3d16a1e"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7a5f4a5a-b46e-41a3-8e68-00b378b0e7db"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("88b07cb4-74ec-4d79-9e77-b0fbbc7f914c"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("902bfb23-70a0-4a55-b8c0-432cea6f7d1c"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("9ac899b5-9e60-4d44-b202-8fdc85cfb77c"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("9b7d74e9-a0a7-4d6c-a27e-7f6eed4ba1cb"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("a0a38f9a-8d3c-4f88-9092-48d0eb7764b3"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("a318f207-098d-46b9-bb8d-5b96b3a04d35"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("ace2bce9-6c9d-47f8-995d-6d8bca51d5c2"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("acee13c3-c20c-4b4c-ad19-4a4d9868424d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("d8496fc3-8e36-4c99-875d-53d7c2e156d2"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("db63e2d8-7fec-4f79-bc1e-50c6728192c3"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("deea5689-78c2-42b4-a287-52e66394a219"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("f6f5d3eb-db8b-4e99-b0df-9afc4b7f1c48"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("fc563f61-c90d-4f1a-9e5b-8c9efdf59323"));
        }
    }
}
