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
            new() {Id = Guid.Parse("1AB8063B-DC0F-43A6-9109-ED1B6770C82D"), CourseName = "Internet of Things (IoT)", Duration = 35, TopicId = Guid.Parse("5393A6FE-5C8B-42AF-A453-2AF2C64F5A35")}

        };
    }
}
