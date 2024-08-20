using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Persistence;
public static class DatabaseHelper
{
    public static IEnumerable<Topic> GetTopics()
    {
        return new List<Topic>
        {
            new() { Id = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563"), TopicName = "Programming"},
            new() { Id = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9"), TopicName = "Web Development" },
            new() { Id = Guid.Parse("B9DD9A87-4852-434E-99DD-3103F7FBA183"), TopicName = "Mobile App Development"},
            new() { Id = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450"), TopicName = "Cloud Computing"},
            new() { Id = Guid.Parse("B8C0DB6A-716D-4004-9C89-0E7CF1BA790C"), TopicName = "DevOps Practices" },
            new() { Id = Guid.Parse("62841CBA-863B-4816-9366-E789646CA43E"), TopicName = "Cybersecurity"},
            new() { Id = Guid.Parse("096ACC69-09A4-4F36-84FF-322E9846AD54"), TopicName = "Machine Learning" },
            new() { Id = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF"), TopicName = "Artificial Intelligence"},
            new() { Id = Guid.Parse("DF8D86F9-69D9-4EDA-9FC4-AA8725C7A47B"), TopicName = "Blockchain Technology"},
            new() { Id = Guid.Parse("5393A6FE-5C8B-42AF-A453-2AF2C64F5A35"), TopicName = "Internet of Things (IoT)" }

        };
    }

    public static IEnumerable<Course> GetCourses()
    {
        return new List<Course>
        {
            new() {Id = Guid.Parse("ED9AAFB1-872D-48A9-ADE2-1A37B3A39F56"), CourseName = "C# Basics", Duration = 30, TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("4976C9F0-C558-41BF-BE86-190249FAC48D"), CourseName = "Advanced C#", Duration = 40 , TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("34F1D3B2-5E90-4873-A5C8-F9A3AE175B3B"), CourseName = "JavaScript Essentials", Duration = 25, TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("4996138F-FD12-4126-9E29-6BBF7E0330BF"), CourseName = "React Fundamentals", Duration = 35, TopicId = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9")},
            new() {Id = Guid.Parse("0BCA523A-285D-48FD-A805-7950F6BF676F"), CourseName = "TypeScript for Beginners", Duration = 20, TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("375A26A9-A1ED-42E2-95E7-E9A30824D192"), CourseName = "Python Programming", Duration = 45 , TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("102F8148-FCF8-4C1F-B513-21327622B8E1"), CourseName = "Java Programming", Duration = 50 , TopicId= Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("5661C5B1-3FC1-4B84-99F5-898C6679FDF2"), CourseName = "SQL Basics", Duration = 30 , TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("83C47415-C606-4CF5-A13F-D692E7326EDD"), CourseName = "NoSQL Databases", Duration = 40, TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("99EEDB49-B4F1-4C13-A3CE-80059AB6F559"), CourseName = "Data Structures", Duration = 35 , TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("808CA6A1-258E-4077-A443-AF979E010D73"), CourseName = "Algorithms", Duration = 40 , TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("F8475A7E-7778-4DDF-8BF7-6879DA2E2E02"), CourseName = "Web Development", Duration = 50, TopicId = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9")},
            new() {Id = Guid.Parse("E99C06C9-8605-4136-9C40-556BFD9EE2A2"), CourseName = "Mobile App Development", Duration = 45, TopicId = Guid.Parse("B9DD9A87-4852-434E-99DD-3103F7FBA183")},
            new() {Id = Guid.Parse("6BAE2C61-0DD3-44E4-8C0C-E98ADD7F3948"), CourseName = "Cloud Computing", Duration = 30, TopicId = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450")},
            new() {Id = Guid.Parse("7496625C-9FAD-4A07-87C6-98BA31C0064F"), CourseName = "DevOps Practices", Duration = 35, TopicId = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450")},
            new() {Id = Guid.Parse("BF1DCEC8-B9FE-40F0-B847-2A142FC320F3"), CourseName = "Cybersecurity Basics", Duration = 25, TopicId = Guid.Parse("62841CBA-863B-4816-9366-E789646CA43E")},
            new() {Id = Guid.Parse("1E2BAC13-26BE-4FF0-9716-EA95970A7BB7"), CourseName = "Machine Learning", Duration = 50, TopicId = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF")},
            new() {Id = Guid.Parse("AA25D7C5-5F48-40B3-BE6E-4BF6EB577F7A"), CourseName = "Artificial Intelligence", Duration = 55 , TopicId = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF")},
            new() {Id = Guid.Parse("A4207BAB-0908-4FF5-878C-0AE69DBA6457"), CourseName = "Blockchain Technology", Duration = 40, TopicId = Guid.Parse("DF8D86F9-69D9-4EDA-9FC4-AA8725C7A47B")},
            new() {Id = Guid.Parse("1AB8063B-DC0F-43A6-9109-ED1B6770C82D"), CourseName = "Internet of Things (IoT)", Duration = 35, TopicId = Guid.Parse("5393A6FE-5C8B-42AF-A453-2AF2C64F5A35")},
            new() {Id = Guid.Parse("A0A38F9A-8D3C-4F88-9092-48D0EB7764B3"), CourseName = "Kotlin for Android Development", Duration = 40, TopicId = Guid.Parse("B9DD9A87-4852-434E-99DD-3103F7FBA183")},
            new() {Id = Guid.Parse("7A5F4A5A-B46E-41A3-8E68-00B378B0E7DB"), CourseName = "Swift Programming", Duration = 35, TopicId = Guid.Parse("B9DD9A87-4852-434E-99DD-3103F7FBA183")},
            new() {Id = Guid.Parse("F6F5D3EB-DB8B-4E99-B0DF-9AFC4B7F1C48"), CourseName = "Ruby on Rails", Duration = 45, TopicId = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9")},
            new() {Id = Guid.Parse("ACEE13C3-C20C-4B4C-AD19-4A4D9868424D"), CourseName = "PHP for Web Development", Duration = 30, TopicId = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9")},
            new() {Id = Guid.Parse("88B07CB4-74EC-4D79-9E77-B0FBBC7F914C"), CourseName = "Angular Development", Duration = 40, TopicId = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9")},
            new() {Id = Guid.Parse("FC563F61-C90D-4F1A-9E5B-8C9EFDF59323"), CourseName = "Vue.js Fundamentals", Duration = 35, TopicId = Guid.Parse("B16EA527-9F9C-4C58-8385-A2F69E5C83D9")},
            new() {Id = Guid.Parse("9B7D74E9-A0A7-4D6C-A27E-7F6EED4BA1CB"), CourseName = "Golang Programming", Duration = 45, TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("30FE5C0C-7DB5-4F7C-B3A4-21711B3A36B7"), CourseName = "Rust for Systems Programming", Duration = 50, TopicId = Guid.Parse("9B0418C2-D8C8-46FB-BD0D-0094F83AD563")},
            new() {Id = Guid.Parse("3B28DFAA-03B3-4382-A1A9-3C8C8FE5682B"), CourseName = "Docker & Kubernetes", Duration = 40, TopicId = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450")},
            new() {Id = Guid.Parse("ACE2BCE9-6C9D-47F8-995D-6D8BCA51D5C2"), CourseName = "AWS Cloud Practitioner", Duration = 35, TopicId = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450")},
            new() {Id = Guid.Parse("0B873A94-4B77-4C08-B7A4-6F3B816D0E91"), CourseName = "Azure Fundamentals", Duration = 30, TopicId = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450")},
            new() {Id = Guid.Parse("16DAF9CB-A94E-4C3E-8A84-47FB0CF5917D"), CourseName = "Google Cloud Platform Essentials", Duration = 35, TopicId = Guid.Parse("7DB2ED45-A087-4E00-B804-B944F400F450")},
            new() {Id = Guid.Parse("9AC899B5-9E60-4D44-B202-8FDC85CFB77C"), CourseName = "Big Data with Hadoop", Duration = 50, TopicId = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF")},
            new() {Id = Guid.Parse("A318F207-098D-46B9-BB8D-5B96B3A04D35"), CourseName = "Data Visualization with Tableau", Duration = 25, TopicId = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF")},
            new() {Id = Guid.Parse("DB63E2D8-7FEC-4F79-BC1E-50C6728192C3"), CourseName = "Ethical Hacking", Duration = 45, TopicId = Guid.Parse("62841CBA-863B-4816-9366-E789646CA43E")},
            new() {Id = Guid.Parse("6CFF4994-907A-46AA-8FC7-3E5976C5B4A1"), CourseName = "Penetration Testing", Duration = 40, TopicId = Guid.Parse("62841CBA-863B-4816-9366-E789646CA43E")},
            new() {Id = Guid.Parse("D8496FC3-8E36-4C99-875D-53D7C2E156D2"), CourseName = "Artificial Neural Networks", Duration = 50, TopicId = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF")},
            new() {Id = Guid.Parse("7559534A-0C8B-4E2A-85F8-FFCBF3D16A1E"), CourseName = "Natural Language Processing", Duration = 55, TopicId = Guid.Parse("71FD7466-E4D4-41F6-ACE8-ED67EA8FAFCF")},
            new() {Id = Guid.Parse("902BFB23-70A0-4A55-B8C0-432CEA6F7D1C"), CourseName = "Augmented Reality Development", Duration = 45, TopicId = Guid.Parse("B9DD9A87-4852-434E-99DD-3103F7FBA183")},
            new() {Id = Guid.Parse("DEEA5689-78C2-42B4-A287-52E66394A219"), CourseName = "Virtual Reality Development", Duration = 45, TopicId = Guid.Parse("B9DD9A87-4852-434E-99DD-3103F7FBA183")}
        };
    }
}
